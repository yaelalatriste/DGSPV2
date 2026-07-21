using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.Seguros.Movimientos.Calculadora;
using DGSP.Shared.Contracts.DTOs.ExternalServices.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Commands.Seguros.Movimientos.Calculadora
{
    public interface ICCorreoCalculadoraProxy
    {
        Task<EmailRequestDto> EnviarCotizacionByCorreo([FromBody] ResultadoCotizacionSgmmCommand command);
    }
    
    public class CCorreoCalculadoraProxy : ICCorreoCalculadoraProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public CCorreoCalculadoraProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<EmailRequestDto> EnviarCotizacionByCorreo(ResultadoCotizacionSgmmCommand command) => SendJsonAsync<ResultadoCotizacionSgmmCommand, EmailRequestDto>(HttpMethod.Post, "seguros/movimientos/CorreosCalculadora/enviarCotizacionCorreo", command);

        private async Task<TResponse> SendJsonAsync<TRequest, TResponse>(HttpMethod method, string endpoint, TRequest data)
        {
            using var request = new HttpRequestMessage(method, $"{_apiGatewayUrl}{endpoint}")
            {
                Content = JsonContent.Create(data)
            };

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions);
        }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
