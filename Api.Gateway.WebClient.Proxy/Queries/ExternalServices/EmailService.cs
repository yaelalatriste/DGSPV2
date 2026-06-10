using DGSP.Shared.Contracts.DTOs.ExternalServices.Email;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DGSP.Gateway.Proxy.Queries.ExternalServices
{
    public interface IEmailProxy
    {
        Task<string> GenerarTokenAsync();
        Task<EmailResponseDto> EnviarCorreoAsync(EmailRequestDto request);
    }

    public class EmailProxy : IEmailProxy
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public EmailProxy(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GenerarTokenAsync()
        {
            var urlToken = _configuration["ApiCorreos:UrlToken"]
                ?? throw new InvalidOperationException("No se configuró ApiCorreos:UrlToken");

            var usuario = _configuration["ApiCorreos:Usuario"]
                ?? throw new InvalidOperationException("No se configuró ApiCorreos:Usuario");

            var clave = _configuration["ApiCorreos:Clave"]
                ?? throw new InvalidOperationException("No se configuró ApiCorreos:Clave");

            var tokenRequest = new TokenRequestDto
            {
                Usuario = usuario,
                Clave = clave
            };

            var response = await _httpClient.PostAsJsonAsync(urlToken, tokenRequest);
            var json = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            using var doc = JsonDocument.Parse(json);

            var token = doc.RootElement.ValueKind switch
            {
                JsonValueKind.String => doc.RootElement.GetString(),
                JsonValueKind.Object when doc.RootElement.TryGetProperty("token", out var tokenProp)
                    => tokenProp.GetString(),
                _ => throw new InvalidOperationException("La respuesta del servicio de token no tiene el formato esperado.")
            };

            if (string.IsNullOrWhiteSpace(token))
                throw new InvalidOperationException("No se pudo obtener el token del servicio de correos.");

            return token;
        }

        public async Task<EmailResponseDto> EnviarCorreoAsync(EmailRequestDto request)
        {
            var urlMail = _configuration["ApiCorreos:UrlMail"]
                ?? throw new InvalidOperationException("No se configuró ApiCorreos:UrlMail");

            var token = await GenerarTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync(urlMail, request);
            var rawResponse = await response.Content.ReadAsStringAsync();

            return new EmailResponseDto
            {
                Success = response.IsSuccessStatusCode,
                Message = response.IsSuccessStatusCode
                    ? "Correo enviado correctamente."
                    : "Error al enviar el correo.",
                RawResponse = rawResponse
            };
        }
    }
}
