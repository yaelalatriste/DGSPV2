using Api.Gateway.Models.Catalogos.DTOs.CTActividades;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTActividades.Queries
{
    public interface IQCTActividadProxy
    {
        Task<List<CTActividadDto>> GetAllActividadesAsync();
        Task<List<CTActividadDto>> GetActividadesByProceso(int proceso);
        Task<CTActividadDto> GetActividadById(int proceso);
    }

    public class QCTActividadProxy : IQCTActividadProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTActividadProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTActividadDto>> GetAllActividadesAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/actividades");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTActividadDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<CTActividadDto>> GetActividadesByProceso(int proceso)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/actividades/getActividadesByProceso/{proceso}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTActividadDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CTActividadDto> GetActividadById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/actividades/getActividadById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTActividadDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
