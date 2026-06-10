using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.SMedicos.Siacom.Catalogos
{
    public interface IQCTConsultorioProxy
    {
        Task<List<CTConsultorioSiacomDto>> GetAllConsultorios();
        Task<CTConsultorioSiacomDto> GetConsultorioById(int id);
    }
    public class QCTConsultorioProxy : IQCTConsultorioProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCTConsultorioProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<List<CTConsultorioSiacomDto>> GetAllConsultorios()
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}smedicos/consultorios/getAllConsultorios");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTConsultorioSiacomDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        public async Task<CTConsultorioSiacomDto> GetConsultorioById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}smedicos/consultorios/getConsultorioById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTConsultorioSiacomDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
