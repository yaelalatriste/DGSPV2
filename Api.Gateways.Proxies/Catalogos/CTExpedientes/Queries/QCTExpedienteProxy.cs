using Api.Gateway.Models.Catalogos.DTOs.CTExpedientes;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTExpedientes.Queries
{
    public interface IQCTExpedienteProxy
    {
        Task<List<CTExpedienteDto>> GetAllExpedientesAsync();
        Task<List<CTExpedienteDto>> GetExpedientesByAsunto(int area);
        Task<CTExpedienteDto> GetExpedienteById(int id);
    }
    public class QCTExpedienteProxy : IQCTExpedienteProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTExpedienteProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTExpedienteDto>> GetAllExpedientesAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctexpedientes/getAllExpedientesAsync");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTExpedienteDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<CTExpedienteDto>> GetExpedientesByAsunto(int asunto)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctexpedientes/getExpedientesByAsunto/{asunto}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTExpedienteDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<CTExpedienteDto> GetExpedienteById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctexpedientes/getExpedienteById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTExpedienteDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
