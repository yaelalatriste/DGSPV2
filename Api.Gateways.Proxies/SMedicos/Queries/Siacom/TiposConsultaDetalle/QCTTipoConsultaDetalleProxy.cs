using Api.Gateway.Models.SMedicos.Queries.Siacom.Catalogos;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.SMedicos.Queries.Siacom.TiposConsultaDetalle
{
    public interface IQCTTipoConsultaDetalleProxy 
    {
        Task<List<CTTipoConsultaDetalleDto>> GetAllTiposConsultaDetalle();
        Task<CTTipoConsultaDetalleDto> GetTipoConsultaDetalleById(int id);
    }

    public class QCTTipoConsultaDetalleProxy : IQCTTipoConsultaDetalleProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTTipoConsultaDetalleProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTTipoConsultaDetalleDto>> GetAllTiposConsultaDetalle()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SMedicosUrl}api/smedicos/tiposConsultaDetalle/getAllTCDetalle");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTTipoConsultaDetalleDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        public async Task<CTTipoConsultaDetalleDto> GetTipoConsultaDetalleById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SMedicosUrl}api/smedicos/tiposConsultaDetalle/getTCDetalleById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTTipoConsultaDetalleDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
