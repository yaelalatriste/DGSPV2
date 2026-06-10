using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Api.Gateway.Proxies.Config;
using System.Net.Http.Headers;
using Api.Gateway.Models.Cendis.Commands.RegistroVisitantes;
using Api.Gateway.Models.Cendis.DTOs.RegistroVisitantes;

namespace Api.Gateway.Proxies.Cendis.Commands.RegistroVisitantes
{
    public interface ICRVisitantesCendiProxy
    {
        Task<RegistroVisitantesDto> CreateRegistro([FromBody] RegistroVisitantesCreateCommand command);
        Task<RegistroVisitantesDto> UpdateRegistro([FromBody] RegistroVisitantesUpdateCommand command);
    }

    public class CRVisitantesCendiProxy : ICRVisitantesCendiProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CRVisitantesCendiProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<RegistroVisitantesDto> CreateRegistro([FromBody] RegistroVisitantesCreateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiUrls.CendisUrl}api/cendis/registroVisitantes/createRegistro", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<RegistroVisitantesDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        public async Task<RegistroVisitantesDto> UpdateRegistro([FromBody] RegistroVisitantesUpdateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.CendisUrl}api/cendis/registroVisitantes/updateRegistro", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<RegistroVisitantesDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
