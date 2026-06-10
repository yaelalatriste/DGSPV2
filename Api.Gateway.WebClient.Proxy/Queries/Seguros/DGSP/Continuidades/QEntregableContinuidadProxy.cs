using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Seguros.DGSP.Continuidades
{
    public interface IQEntregableContinuidadProxy
    {
        Task<List<EntregableContinuidadDto>> GetEntregablesByContinuidad(int continuidadId);
        Task<string> VisualizarEntregable(int entregableId);
    }
    public class QEntregableContinuidadProxy : IQEntregableContinuidadProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QEntregableContinuidadProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }
        public Task<List<EntregableContinuidadDto>> GetEntregablesByContinuidad(int continuidadId) => GetAsync<List<EntregableContinuidadDto>>($"{_apiGatewayUrl}seguros/continuidades/entregablesContinuidad/getEntregablesByContinuidad/{continuidadId}");

        public async Task<string> VisualizarEntregable(int entregableId)
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}seguros/continuidades/entregablesContinuidad/visualizarEntregable/{entregableId}");
            request.EnsureSuccessStatusCode();

            var contents = await request.Content.ReadAsStringAsync();

            return contents;
        }

        private async Task<T> GetAsync<T>(string url)
        {
            using var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            return JsonSerializer.Deserialize<T>(stream, _jsonOptions);
        }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
