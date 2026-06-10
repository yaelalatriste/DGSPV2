using Api.Gateway.Models.Catalogos.DTOs.CTAreas;
using Api.Gateway.Models.Catalogos.DTOs.CTIncidencias;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTIncidencias
{
    public interface IQCTIncidenciaProxy
    {
        Task<List<CTIncidenciaDto>> GetAllCTIncidenciasAsync();
        Task<List<CTIncidenciaDto>> GetCTIncidenciasByTipo(string tipo);
        Task<CTIncidenciaDto> GetCTIncidenciaById(int area);
    }

    public class QCTIncidenciaProxy : IQCTIncidenciaProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTIncidenciaProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTIncidenciaDto>> GetAllCTIncidenciasAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/incidencias/getAllIncidencias");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTIncidenciaDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<CTIncidenciaDto>> GetCTIncidenciasByTipo(string tipo)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/incidencias/getIncidenciasByTipo/{tipo}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTIncidenciaDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CTIncidenciaDto> GetCTIncidenciaById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/incidencias/getIncidenciaById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTIncidenciaDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
