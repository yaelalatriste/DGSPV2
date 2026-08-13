using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Movimientos.Calculadora;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Seguros.DGSP.Movimientos.Calculadora
{
    public interface IQCalendarioNominaProxy
    {
        Task<List<CalendarioNominaDto>> GetAllCalendarioAsync();
        Task<CalendarioNominaDto> GetQuincenaById(int id);
        Task<List<CalendarioNominaDto>> GetQuincenasByPeriodoAsync(string fechaInicial, string fechaFinal);
    }
    public class QCalendarioNominaProxy : IQCalendarioNominaProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCalendarioNominaProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<CalendarioNominaDto>> GetAllCalendarioAsync() => GetAsync<List<CalendarioNominaDto>>($"{_apiGatewayUrl}seguros/movimientos/CalendarioNomina/getAllCalendario");
        public Task<CalendarioNominaDto> GetQuincenaById(int id) => GetAsync<CalendarioNominaDto>($"{_apiGatewayUrl}seguros/movimientos/CalendarioNomina/getCalendarioById/{id}");
        public Task<List<CalendarioNominaDto>> GetQuincenasByPeriodoAsync(string fechaInicial, string fechaFinal) => GetAsync<List<CalendarioNominaDto>>($"{_apiGatewayUrl}seguros/movimientos/CalendarioNomina/getQuincenasByPeriodo/{fechaInicial}/{fechaFinal}");

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
