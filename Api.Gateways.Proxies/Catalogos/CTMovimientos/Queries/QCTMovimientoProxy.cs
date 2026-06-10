using Api.Gateway.Models.Catalogos.DTOs.CTMeses;
using Api.Gateway.Models.Catalogos.DTOs.CTMovimientos;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTMovimientos.Queries
{
    public interface IQCTMovimientoProxy
    {
        Task<List<CTMovimientoDto>> GetAllMovimientos();
        Task<CTMovimientoDto> GetMovimientoById(int id);
    }
    public class QCTMovimientoProxy : IQCTMovimientoProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTMovimientoProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTMovimientoDto>> GetAllMovimientos()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/movimientos/getAllMovimientos");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTMovimientoDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CTMovimientoDto> GetMovimientoById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/movimmientos/getMovimientoById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTMovimientoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
