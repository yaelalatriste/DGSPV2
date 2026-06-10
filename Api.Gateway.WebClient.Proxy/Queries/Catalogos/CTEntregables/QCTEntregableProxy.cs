using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Catalogos.CTEntregables
{
    public interface IQCTEntregableProxy
    {
        Task<List<CTEntregableDto>> GetAllEntregablesAsync();
        Task<CTEntregableDto> GetEntregableByIdAsync(int id);
    }

    public class QCTEntregableProxy : IQCTEntregableProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCTEntregableProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<List<CTEntregableDto>> GetAllEntregablesAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}catalogos/ctentregables");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTEntregableDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CTEntregableDto> GetEntregableByIdAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}catalogos/ctentregables/getEntregableById/{id}");
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
