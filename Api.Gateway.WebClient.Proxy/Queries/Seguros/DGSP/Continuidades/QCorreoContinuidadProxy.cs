using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.ExternalServices.Email;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Seguros.DGSP.Continuidades
{
    public interface IQCorreoContinuidadProxy
    {
        Task<EmailRequestDto> EnviarCorreoPoliza(int continuidadId);
        Task<EmailRequestDto> EnviarCorreoReferencia(int continuidadId);
        Task<EmailRequestDto> EnviarCorreoMovimientos();
    }
    public class QCorreoContinuidadProxy : IQCorreoContinuidadProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCorreoContinuidadProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<EmailRequestDto> EnviarCorreoPoliza(int continuidadId) => GetAsync<EmailRequestDto>($"{_apiGatewayUrl}seguros/correosContinuidad/enviarCorreoPoliza/{continuidadId}");
        public Task<EmailRequestDto> EnviarCorreoReferencia(int continuidadId) => GetAsync<EmailRequestDto>($"{_apiGatewayUrl}seguros/correosContinuidad/enviarCorreoReferencia/{continuidadId}");
        public Task<EmailRequestDto> EnviarCorreoMovimientos() => GetAsync<EmailRequestDto>($"{_apiGatewayUrl}seguros/correosContinuidad/enviarCorreoMovimientos");

        private async Task<T> GetAsync<T>(string url)
        {
            using var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            return JsonSerializer.Deserialize<T>(stream, _jsonOptions);
        }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
