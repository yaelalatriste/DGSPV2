using Api.Gateway.Models.Planeacion.Queries.ActividadesMensuales;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Planeacion.Queries.ActividadesMensuales
{
    public interface IQAMensualProxy
    {
        Task<List<ActividadMensualDto>> GetAllActividadesAsync();
        Task<List<ActividadMensualDto>> GetRegistrosByActividades(int actividad);
        Task<List<ActividadMensualDto>> GetActividadesByArea(int area);
        Task<string> VisualizarEntregable(int anio, string proceso, string actividad, string mes, string entregable, string archivo);
    }
    public class QAMensualProxy : IQAMensualProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QAMensualProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<ActividadMensualDto>> GetAllActividadesAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/actividadesMensuales");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<ActividadMensualDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<ActividadMensualDto>> GetRegistrosByActividades(int actividad)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/actividadesMensuales/getActividadesByProceso/{actividad}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<ActividadMensualDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<List<ActividadMensualDto>> GetActividadesByArea(int area)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/actividadesMensuales/getActividadesByArea/{area}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<ActividadMensualDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<string> VisualizarEntregable(int anio, string proceso, string actividad, string mes, string entregable, string archivo)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/actividadesMensuales/visualizarEntregable/{anio}/{proceso}/{actividad}/{mes}/{entregable}/{archivo}");
            request.EnsureSuccessStatusCode();

            var contents = await request.Content.ReadAsStringAsync();

            return contents;

        }
    }
}
