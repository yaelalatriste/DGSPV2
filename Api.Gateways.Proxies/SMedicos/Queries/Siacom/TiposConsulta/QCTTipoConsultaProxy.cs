using Api.Gateway.Models.SMedicos.Queries.Siacom.Catalogos;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.SMedicos.Queries.Siacom.TiposConsulta
{
    public interface IQCTTipoConsultaProxy
    {
        Task<List<CTTipoConsultaDto>> GetAllTiposConsultas();
        Task<CTTipoConsultaDto> GetTipoConsultaById(int id);
    }

    public class QCTTipoConsultaProxy : IQCTTipoConsultaProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTTipoConsultaProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTTipoConsultaDto>> GetAllTiposConsultas()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SMedicosUrl}api/smedicos/tiposConsulta/getAllTiposConsultas");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTTipoConsultaDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        public async Task<CTTipoConsultaDto> GetTipoConsultaById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SMedicosUrl}api/smedicos/tiposConsulta/getTipoConsultaById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTTipoConsultaDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
