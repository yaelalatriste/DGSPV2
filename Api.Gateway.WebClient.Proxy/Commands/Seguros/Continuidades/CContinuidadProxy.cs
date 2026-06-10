using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.Continuidad;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Commands.Seguros.Continuidades.ContactosContinuidades
{
    public interface ICContinuidadProxy
    {
        Task<ContinuidadDto> RegistrarContinuidadAsync([FromBody] RegistrarContinuidadCommand command);
        Task<ContinuidadDto> ActualizarContinuidadAsync([FromBody] ActualizarContinuidadCommand command);
        Task<ContinuidadDto> EstatusContinuidadAsync([FromBody] EstatusContinuidadCommand command);
    }
    
    public class CContinuidadProxy : ICContinuidadProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public CContinuidadProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<ContinuidadDto> RegistrarContinuidadAsync(RegistrarContinuidadCommand command) => SendJsonAsync<RegistrarContinuidadCommand, ContinuidadDto>(HttpMethod.Post, "seguros/continuidades/continuidad/createContinuidad", command);
        public Task<ContinuidadDto> ActualizarContinuidadAsync(ActualizarContinuidadCommand command) => SendJsonAsync<ActualizarContinuidadCommand, ContinuidadDto>(HttpMethod.Put,"seguros/continuidades/continuidad/updateContinuidad",command);
        public Task<ContinuidadDto> EstatusContinuidadAsync(EstatusContinuidadCommand command) => SendJsonAsync<EstatusContinuidadCommand, ContinuidadDto>(HttpMethod.Put, "seguros/continuidades/continuidad/estatusContinuidad", command);

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
