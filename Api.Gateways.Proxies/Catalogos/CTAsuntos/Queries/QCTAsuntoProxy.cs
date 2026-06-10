using Api.Gateway.Models.Catalogos.DTOs.CTAsuntos;
using Api.Gateway.Models.Catalogos.DTOs.CTExpedientes;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTAsuntos.Queries
{
    public interface IQCTAsuntoProxy
    {
        Task<List<CTAsuntoDto>> GetAllAsuntosAsync();
        Task<List<CTAsuntoDto>> GetAsuntosByArea(int area);
        Task<CTAsuntoDto> GetAsuntoById(int id);
    }
    public class QCTAsuntoProxy : IQCTAsuntoProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTAsuntoProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTAsuntoDto>> GetAllAsuntosAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctasuntos/getAllAsuntosAsync");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTAsuntoDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<CTAsuntoDto>> GetAsuntosByArea(int area)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctasuntos/getAsuntosByArea/{area}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTAsuntoDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<CTAsuntoDto> GetAsuntoById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctasuntos/getAsuntoById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTAsuntoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
