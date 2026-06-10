using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposInsumos
{
    public interface IQCTTipoInsumoProxy
    {
        Task<List<CTTipoInsumoDto>> GetAllTiposInsumosAsync();
        Task<CTTipoInsumoDto> GetTipoInsumoByIdAsync(int id);
    }
    public class QCTTipoInsumoProxy : IQCTTipoInsumoProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCTTipoInsumoProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<CTTipoInsumoDto>> GetAllTiposInsumosAsync() => GetAsync<List<CTTipoInsumoDto>>($"{_apiGatewayUrl}catalogos/cttiposInsumo/getAllTipoInsumos");
        public Task<CTTipoInsumoDto> GetTipoInsumoByIdAsync(int id) => GetAsync<CTTipoInsumoDto>($"{_apiGatewayUrl}catalogos/cttiposInsumo/getTipoInsumoById/{id}");

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
