using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;
using DGSP.Shared.Contracts.DTOs.Estatus.NotasTraspaso;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Estatus.Continuidades
{
    public interface IQFlujoContinuidadProxy
    {
        Task<List<FlujoContinuidadDto>> GetAllFlujosNotasTraspaso();
        Task<List<FlujoContinuidadDto>> GetEstatusConsecutivoNota(int estatus);
    }

    public class QFlujoContinuidadProxy : IQFlujoContinuidadProxy
    {

        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QFlujoContinuidadProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<FlujoContinuidadDto>> GetAllFlujosNotasTraspaso() => GetAsync<List<FlujoContinuidadDto>>($"{_apiGatewayUrl}estatus/flujoContinuidad/getAllFlujosContinuidades");
        public Task<List<FlujoContinuidadDto>> GetEstatusConsecutivoNota(int estatus) => GetAsync<List<FlujoContinuidadDto>>($"{_apiGatewayUrl}estatus/flujoContinuidad/getEstatusConsecutivoContinuidad/{estatus}");

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
