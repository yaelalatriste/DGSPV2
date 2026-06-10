using Api.Gateway.Models.Catalogos.DTOs.CTAreas;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using DGSP.Module.Catalogos.Application.DTOs.CTCendi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTCendis.Queries
{
    public interface IQCTCendisProxy
    {
        Task<List<CTCendiDto>> GetAllCendisAsync();
        Task<CTCendiDto> GetCendisByIdAsync(int id);
    }
    public class QCTCendisProxy : IQCTCendisProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTCendisProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTCendiDto>> GetAllCendisAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/cendis/getAllCendis");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTCendiDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CTCendiDto> GetCendisByIdAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/cendis/getCendisById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTCendiDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
