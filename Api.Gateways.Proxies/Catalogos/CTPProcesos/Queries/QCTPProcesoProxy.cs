using Api.Gateway.Models.Catalogos.DTOs.CTProcesos;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTPProcesos.Queries
{
    public interface IQCTPProcesoProxy
    {
        Task<List<CTPProcesoDto>> GetPProcesosAsync();
        Task<List<CTPProcesoDto>> GetPProcesosByArea(int idArea);
    }
    public class QCTPProcesoProxy : IQCTPProcesoProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTPProcesoProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTPProcesoDto>> GetPProcesosAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctprocesos");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTPProcesoDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<CTPProcesoDto>> GetPProcesosByArea(int idArea)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctprocesos/getPProcesosByArea/{idArea}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTPProcesoDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
