using Api.Gateway.Models.Catalogos.DTOs.CTAreas;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTAreas.Queries
{
    public interface IQCTAreaProxy
    {
        Task<List<CTAreaDto>> GetAllAreasAsync();
        Task<CTAreaDto> GetAreaByIdAsync(int area);
    }
    public class QCTAreaProxy : IQCTAreaProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTAreaProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTAreaDto>> GetAllAreasAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/areas");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTAreaDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CTAreaDto> GetAreaByIdAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/areas/getAreaById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTAreaDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
