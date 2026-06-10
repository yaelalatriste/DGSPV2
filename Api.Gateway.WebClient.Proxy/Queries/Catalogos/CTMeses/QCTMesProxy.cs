using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Catalogos.CTMeses
{
    public interface IQCTMesProxy
    {
        Task<List<CTMesDto>> GetAllMesesAsync();
        Task<CTMesDto> GetMesById(int id);
    }

    public class QCTMesProxy : IQCTMesProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCTMesProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<List<CTMesDto>> GetAllMesesAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}catalogos/meses");
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
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}catalogos/meses/getMesById/{id}");
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
