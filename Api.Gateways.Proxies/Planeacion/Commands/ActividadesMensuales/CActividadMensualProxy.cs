using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Gateway.Models.Planeacion.Commands.ActividadesMensuales;
using System.Net.Http.Headers;
using Api.Gateway.Models.Planeacion.Queries.ActividadesMensuales;
using System.Text.Json;
using System.Text;

namespace Api.Gateway.Proxies.Planeacion.Commands.ActividadesMensuales
{
    public interface ICActividadMensualProxy
    {
        Task<ActividadMensualDto> CreateActividad([FromForm] ActividadMensualCreateCommand actividad);
        Task<ActividadMensualDto> UpdateActividad([FromForm] ActividadMensualUpdateCommand actividad);
        Task<ActividadMensualDto> DeleteActividad([FromBody] ActividadMensualDeleteCommand actividad);
    }

    public class CActividadMensualProxy : ICActividadMensualProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CActividadMensualProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<ActividadMensualDto> CreateActividad([FromBody] ActividadMensualCreateCommand actividad)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(actividad),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/actividadesMensuales/createActividad", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ActividadMensualDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<ActividadMensualDto> UpdateActividad([FromBody] ActividadMensualUpdateCommand actividad)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(actividad),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/actividadesMensuales/updateActividad", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ActividadMensualDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<ActividadMensualDto> DeleteActividad([FromBody] ActividadMensualDeleteCommand actividad)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(actividad),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/actividadesMensuales/deleteActividad", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ActividadMensualDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
