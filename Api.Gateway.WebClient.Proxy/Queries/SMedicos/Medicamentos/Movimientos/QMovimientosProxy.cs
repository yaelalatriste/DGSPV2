using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Movimientos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Movimientos
{
    public interface IQMovimientosProxy
    {
        Task<List<MovimientoInventarioDto>> GetAllMovimientosInventariosAsync();
        Task<List<MovimientoInventarioDto>> GetMovimientosInventariosByLoteAsync(int loteId);
    }

    public class QMovimientosProxy : IQMovimientosProxy 
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QMovimientosProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<MovimientoInventarioDto>> GetAllMovimientosInventariosAsync() => GetAsync<List<MovimientoInventarioDto>>($"{_apiGatewayUrl}smedicos/movimientos/getAllMovimientos");
        public Task<List<MovimientoInventarioDto>> GetMovimientosInventariosByLoteAsync(int loteId) => GetAsync<List<MovimientoInventarioDto>>($"{_apiGatewayUrl}smedicos/movimientos/getMovimientosByLote/{loteId}");

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
