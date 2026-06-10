using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Estatus.NotasTraspaso;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Estatus.NotasTraspaso
{
    public interface IQFlujoNotasProxy
    {
        Task<List<FlujoNotaTraspasoDto>> GetAllFlujosNotasTraspaso();
        Task<List<FlujoNotaTraspasoDto>> GetEstatusConsecutivoNota(int estatus);
    }

    public class QFlujoNotasProxy : IQFlujoNotasProxy
    {

        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QFlujoNotasProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<FlujoNotaTraspasoDto>> GetAllFlujosNotasTraspaso() => GetAsync<List<FlujoNotaTraspasoDto>>($"{_apiGatewayUrl}estatus/flujoNotasTraspaso/getAllFlujosNotas");
        public Task<List<FlujoNotaTraspasoDto>> GetEstatusConsecutivoNota(int estatus) => GetAsync<List<FlujoNotaTraspasoDto>>($"{_apiGatewayUrl}estatus/flujoNotasTraspaso/getEstatusConsecutivoNota/{estatus}");

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
