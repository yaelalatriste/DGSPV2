using Api.Gateway.Models.Catalogos.DTOs.CTMeses;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTMeses.Queries
{
    public interface IQCTMesProxy
    {
        Task<List<CTMesDto>> GetAllMesesAsync();
        Task<CTMesDto> GetMesById(int id);
    }
    public class QCTMesProxy : IQCTMesProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTMesProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTMesDto>> GetAllMesesAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/meses");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTMesDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CTMesDto> GetMesById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/meses/getMesById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTMesDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
