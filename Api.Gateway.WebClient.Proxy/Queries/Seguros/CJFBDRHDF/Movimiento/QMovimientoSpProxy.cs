using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.OficiosContinuidades;
using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Seguros.CJFBDRHDF.Movimiento
{
    public interface IQMovimientoSpProxy
    {
        Task<int> GetMovimientoSpByContinuidad(int id);
        Task<MovimientoDto> GetMovimientoById(int id);
    }
    public class QMovimientoSpProxy : IQMovimientoSpProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QMovimientoSpProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<int> GetMovimientoSpByContinuidad(int continuidadId) => GetAsync<int>($"{_apiGatewayUrl}seguros/continuidades/movimientoSp/getMovimientoSpByContinuidad/{continuidadId}");
        public Task<MovimientoDto> GetMovimientoById(int id) => GetAsync<MovimientoDto>($"{_apiGatewayUrl}seguros/continuidades/movimientoSp/getMovimientoById/{id}");

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
