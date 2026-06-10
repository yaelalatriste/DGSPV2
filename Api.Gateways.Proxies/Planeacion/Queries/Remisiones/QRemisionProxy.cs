using Api.Gateway.Models.Planeacion.Queries.ActividadesMensuales;
using Api.Gateway.Models.Planeacion.Queries.Almacen;
using Api.Gateway.Models.Planeacion.Queries.Remisiones;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Planeacion.Queries.Remisiones
{
    public interface IQRemisionProxy
    {
        Task<List<RemisionDto>> GetAllRemisiones();
        Task<RemisionDto> GetRemisionById(int id);
    }
    public class QRemisionProxy : IQRemisionProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QRemisionProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<RemisionDto>> GetAllRemisiones()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/remisiones/getAllRemisiones");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<RemisionDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<RemisionDto> GetRemisionById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/remisiones/getRemisionById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<RemisionDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
