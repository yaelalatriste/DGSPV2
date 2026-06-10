using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Salidas
{
    public interface IQSalidaMedicamentoDetalleProxy
    {
        Task<List<SalidaMedicamentoDetalleDto>> GetDetallesBySalidaAsync(int salida);
        Task<SalidaMedicamentoDetalleDto> GetDetallesById(int id);
    }

    public class QSalidaMedicamentoDetalleProxy : IQSalidaMedicamentoDetalleProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QSalidaMedicamentoDetalleProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<SalidaMedicamentoDetalleDto>> GetDetallesBySalidaAsync(int salida) => GetAsync<List<SalidaMedicamentoDetalleDto>>($"{_apiGatewayUrl}smedicos/salidaMedicamentoDetalle/getDetallesBySalida/{salida}");
        public Task<SalidaMedicamentoDetalleDto> GetDetallesById(int id) => GetAsync<SalidaMedicamentoDetalleDto>($"{_apiGatewayUrl}smedicos/salidaMedicamentoDetalle/getDetalleById/{id}");

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
