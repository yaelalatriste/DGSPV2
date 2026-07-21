using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.OficiosContinuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Commands.Seguros.Siniestros.Oficios
{
    public interface ICCOficioContinuidadProxy
    {
        Task<OficioContinuidadDto> RegistrarOficioContinuidadAsync([FromBody] RegistrarOficioContinuidadCommand command);
    }
    
    public class COficioContinuidadProxy : ICCOficioContinuidadProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public COficioContinuidadProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<OficioContinuidadDto> RegistrarOficioContinuidadAsync(RegistrarOficioContinuidadCommand command) => SendJsonAsync<RegistrarOficioContinuidadCommand, OficioContinuidadDto>(HttpMethod.Post,"seguros/continuidades/createOficio",command);

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
