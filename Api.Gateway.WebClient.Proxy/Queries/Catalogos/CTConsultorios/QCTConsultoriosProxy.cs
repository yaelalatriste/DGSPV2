using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Catalogos.CTConsultorios
{
    public interface IQCTConsultoriosProxy
    {
        Task<List<CTConsultorioDto>> GetAllConsultoriosAsync();
        Task<CTConsultorioDto> GetConsultorioByIdAsync(int id);
    }
    public class QCTConsultoriosProxy : IQCTConsultoriosProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCTConsultoriosProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<CTConsultorioDto>> GetAllConsultoriosAsync() => GetAsync<List<CTConsultorioDto>>($"{_apiGatewayUrl}catalogos/consultorios/getAllConsultorios");
        public Task<CTConsultorioDto> GetConsultorioByIdAsync(int id) => GetAsync<CTConsultorioDto>($"{_apiGatewayUrl}catalogos/consultorios/getConsultorioById/{id}");

        private async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error calling GET {url}. Status: {response.StatusCode}, Details: {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
