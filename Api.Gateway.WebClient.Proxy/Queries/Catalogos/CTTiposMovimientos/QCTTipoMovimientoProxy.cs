using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposMovimientos
{
    public interface IQCTTipoMovimientoProxy
    {
        Task<List<CTTipoMovimientoDto>> GetAllTiposMovimientosAsync();
        Task<CTTipoMovimientoDto> GetTipoMovimientoByIdAsync(int id);
        Task<List<CTTipoMovimientoDto>> GetMovimientosEntradaAsync();
        Task<List<CTTipoMovimientoDto>> GetMovimientosSalidaAsync();
    }
    public class QCTTipoMovimientoProxy : IQCTTipoMovimientoProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCTTipoMovimientoProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<CTTipoMovimientoDto>> GetAllTiposMovimientosAsync() => GetAsync<List<CTTipoMovimientoDto>>($"{_apiGatewayUrl}catalogos/cttiposMovimiento/getAllTiposMovimientos");
        public Task<List<CTTipoMovimientoDto>> GetMovimientosEntradaAsync() => GetAsync<List<CTTipoMovimientoDto>>($"{_apiGatewayUrl}catalogos/cttiposMovimiento/getTiposMovimientosEntrada");
        public Task<List<CTTipoMovimientoDto>> GetMovimientosSalidaAsync() => GetAsync<List<CTTipoMovimientoDto>>($"{_apiGatewayUrl}catalogos/cttiposMovimiento/getTiposMovimientosSalida");
        public Task<CTTipoMovimientoDto> GetTipoMovimientoByIdAsync(int id) => GetAsync<CTTipoMovimientoDto>($"{_apiGatewayUrl}catalogos/cttiposMovimiento/getTipoMovimientoById/{id}");

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
