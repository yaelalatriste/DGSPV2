using Api.Gateway.Models.SMedicos.Queries.Siacom.Catalogos;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.SMedicos.Queries.Siacom.Consultorios
{
    public interface IQCTConsultorioProxy
    {
        Task<List<CTConsultorioDto>> GetAllConsultorios();
        Task<CTConsultorioDto> GetConsultorioById(int id);
    }
    public class QCTConsultorioProxy : IQCTConsultorioProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTConsultorioProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTConsultorioDto>> GetAllConsultorios()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SMedicosUrl}api/smedicos/consultorios/getAllConsultorios");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTConsultorioDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        public async Task<CTConsultorioDto> GetConsultorioById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SMedicosUrl}api/smedicos/consultorios/getConsultorioById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTConsultorioDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
