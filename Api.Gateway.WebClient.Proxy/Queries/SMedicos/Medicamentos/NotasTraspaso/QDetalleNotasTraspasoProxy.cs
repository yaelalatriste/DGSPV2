using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.NotasTraspaso
{
    public interface IQDetalleNotasTraspasoProxy
    {
        Task<List<DetalleNotaTraspasoDto>> GetDetallesNotaTraspasoByNotaAsync(int nota);
        Task<DetalleNotaTraspasoDto> GetDetalleNotaTraspasoByIdAsync(int id);
    }
    public class QDetalleNotasTraspasoProxy : IQDetalleNotasTraspasoProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QDetalleNotasTraspasoProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<DetalleNotaTraspasoDto>> GetDetallesNotaTraspasoByNotaAsync(int nota) => GetAsync<List<DetalleNotaTraspasoDto>>($"{_apiGatewayUrl}smedicos/detalleNotasTraspaso/getDetallesNotaTraspasoByNota/{nota}");
        public Task<DetalleNotaTraspasoDto> GetDetalleNotaTraspasoByIdAsync(int id) => GetAsync<DetalleNotaTraspasoDto>($"{_apiGatewayUrl}smedicos/detalleNotasTraspaso/getDetalleNotaTraspasoById/{id}");

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
