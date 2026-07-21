using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.MediosContacto;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Commands.Seguros.Siniestros.ContactosContinuidades
{
    public interface ICContactoContinuidadProxy
    {
        Task<ContactoContinuidadDto> RegistrarContactoContinuidadAsync([FromBody] RegistrarContactoContinuidadCommand command);
        Task<ContactoContinuidadDto> ActualizarContactoContinuidadAsync([FromBody] ActualizarContactoContinuidadCommand command);
    }
    
    public class CContactoContinuidadProxy : ICContactoContinuidadProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public CContactoContinuidadProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<ContactoContinuidadDto> RegistrarContactoContinuidadAsync(RegistrarContactoContinuidadCommand command) => SendJsonAsync<RegistrarContactoContinuidadCommand, ContactoContinuidadDto>(HttpMethod.Post,"seguros/continuidades/contactoContinuidad/createContacto",command);
        public Task<ContactoContinuidadDto> ActualizarContactoContinuidadAsync(ActualizarContactoContinuidadCommand command) => SendJsonAsync<ActualizarContactoContinuidadCommand, ContactoContinuidadDto>(HttpMethod.Put,"seguros/continuidades/contactoContinuidad/updateContacto",command);

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
