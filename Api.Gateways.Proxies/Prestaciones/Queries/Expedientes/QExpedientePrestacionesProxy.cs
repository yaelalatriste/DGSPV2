using Api.Gateway.Models.Expedientes.DTOs;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Prestaciones.Queries.Expedientes
{
    public interface IQExpedientePrestacionesProxy
    {
        Task<List<ExpedienteDto>> GetAllExpedientes();
        Task<List<ExpedienteDto>> GetExpedientesByAnio(int anio);
        Task<List<ExpedienteDto>> GetExpedientesByCategoria(int categoria);
        Task<List<ExpedienteDto>> GetExpedientesByAnioCategoria(int anio, int categoria);
        Task<ExpedienteDto> GetExpedienteById(int id);
    }
    public class QExpedientePrestacionesProxy : IQExpedientePrestacionesProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QExpedientePrestacionesProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<ExpedienteDto>> GetAllExpedientes()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PrestacionesUrl}api/prestaciones/expedientes/getAllExpedientes");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<ExpedienteDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<List<ExpedienteDto>> GetExpedientesByAnio(int anio)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PrestacionesUrl}api/prestaciones/expedientes/getExpedientesByAnio/{anio}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<ExpedienteDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<ExpedienteDto>> GetExpedientesByCategoria(int categoria)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PrestacionesUrl}api/prestaciones/expedientes/getExpedientesByCategoria/{categoria}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<ExpedienteDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<ExpedienteDto>> GetExpedientesByAnioCategoria(int anio, int categoria)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PrestacionesUrl}api/prestaciones/expedientes/getExpedientesByAnioCategoria/{anio}/{categoria}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<ExpedienteDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<ExpedienteDto> GetExpedienteById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PrestacionesUrl}api/prestaciones/expedientes/getExpedienteById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ExpedienteDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
