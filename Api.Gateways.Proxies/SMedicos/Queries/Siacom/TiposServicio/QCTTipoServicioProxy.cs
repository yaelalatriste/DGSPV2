using Api.Gateway.Models.SMedicos.Queries.Siacom.Catalogos;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.SMedicos.Queries.Siacom.TiposServicio
{
    public interface IQCTTipoServicioProxy
    {
        Task<List<CTTipoServicioDto>> GetAllTiposServicios();
        Task<CTTipoServicioDto> GetTipoServicioById(int id);
    }

    public class QCTTipoServicioProxy : IQCTTipoServicioProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTTipoServicioProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTTipoServicioDto>> GetAllTiposServicios()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SMedicosUrl}api/smedicos/tiposServicio/getAllTiposServicio");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTTipoServicioDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        public async Task<CTTipoServicioDto> GetTipoServicioById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SMedicosUrl}api/smedicos/tiposServicio/getTipoServicioById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTTipoServicioDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
