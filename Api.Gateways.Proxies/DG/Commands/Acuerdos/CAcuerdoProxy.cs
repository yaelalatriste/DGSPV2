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
using Api.Gateway.Models.DG.Commands.Acuerdos;
using Api.Gateway.Models.DG.Queries.Acuerdos;

namespace Api.Gateway.Proxies.DG.Commands.Acuerdos
{
    public interface ICAcuerdoProxy
    {
        Task<AcuerdoDto> CreateAcuerdo(AcuerdoCreateCommand command);
        Task<AcuerdoDto> UpdateAcuerdo(AcuerdoUpdateCommand command);
        Task<AcuerdoDto> DeleteAcuerdo(AcuerdoDeleteCommand command);
    }

    public class CAcuerdoProxy : ICAcuerdoProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CAcuerdoProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<AcuerdoDto> CreateAcuerdo([FromBody] AcuerdoCreateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiUrls.DGUrl}api/dg/acuerdos/createAcuerdo", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<AcuerdoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<AcuerdoDto> UpdateAcuerdo([FromBody] AcuerdoUpdateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.DGUrl}api/dg/acuerdos/updateAcuerdo", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<AcuerdoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<AcuerdoDto> DeleteAcuerdo([FromBody] AcuerdoDeleteCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.DGUrl}api/dg/acuerdos/deleteAcuerdo", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<AcuerdoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
