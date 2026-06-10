using Api.Gateway.Models.Cendis.Commands.DetallesJustificantes;
using Api.Gateway.Models.Cendis.Commands.Justificantes;
using Api.Gateway.Models.Cendis.DTOs.Justificantes;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Cendis.Commands.Justificantes
{
    public interface ICJustificanteProxy
    {
        Task<JustificanteDto> CreateJustificante([FromBody] JustificanteCreateCommand command);
        Task<JustificanteDto> UpdateJustificante([FromBody] JustificanteUpdateCommand command);
        Task<JustificanteDto> DeleteJustificante([FromBody] JustificanteDeleteCommand command);
    }

    public class CJustificanteProxy : ICJustificanteProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CJustificanteProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<JustificanteDto> CreateJustificante([FromBody] JustificanteCreateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiUrls.CendisUrl}api/cendis/justificantes/createJustificante", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<JustificanteDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<JustificanteDto> UpdateJustificante([FromBody] JustificanteUpdateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.CendisUrl}api/cendis/justificantes/updateJustificante", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<JustificanteDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<JustificanteDto> DeleteJustificante([FromBody] JustificanteDeleteCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.CendisUrl}api/cendis/justificantes/deleteJustificante", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<JustificanteDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
