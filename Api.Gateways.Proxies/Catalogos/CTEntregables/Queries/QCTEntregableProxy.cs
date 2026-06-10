using Api.Gateway.Models.Catalogos.DTOs.CTEntregables;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTEntregables.Queries
{
    public interface IQCTEntregableProxy
    {
        Task<List<CTEntregableDto>> GetAllEntregablesAsync();
        Task<CTEntregableDto> GetEntregableById(int id);
    }
    public class QCTEntregableProxy : IQCTEntregableProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTEntregableProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTEntregableDto>> GetAllEntregablesAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctentregables");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTEntregableDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CTEntregableDto> GetEntregableById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctentregables/getEntregableById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTEntregableDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
