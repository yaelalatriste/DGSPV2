using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Logs;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Dashboards;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Reportes;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.NotasTraspaso
{
    public interface IQNotasTraspasoProxy
    {
        Task<List<NotaTraspasoDto>> GetAllNotasTraspasoAsync();
        Task<NotaTraspasoDto> GetNotaTraspasoByIdAsync(int id);
        Task<List<LogNotaTraspasoDto>> GetLogsNotaTraspasoByIdAsync(int id);
        Task<string> VisualizarEntregable(int id);
    }
    public class QNotasTraspasoProxy : IQNotasTraspasoProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QNotasTraspasoProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<NotaTraspasoDto>> GetAllNotasTraspasoAsync() => GetAsync<List<NotaTraspasoDto>>($"{_apiGatewayUrl}smedicos/notasTraspaso/getAllNotasTraspaso");
        public Task<NotaTraspasoDto> GetNotaTraspasoByIdAsync(int id) => GetAsync<NotaTraspasoDto>($"{_apiGatewayUrl}smedicos/notasTraspaso/getNotaTraspasoById/{id}");
        //public Task<string> VisualizarEntregable(int notaId) => GetAsync<string>($"{_apiGatewayUrl}smedicos/notasTraspaso/visualizarEntregable/{notaId}");
        public Task<List<LogNotaTraspasoDto>> GetLogsNotaTraspasoByIdAsync(int id) => GetAsync<List<LogNotaTraspasoDto>>($"{_apiGatewayUrl}smedicos/logsNotasTraspaso/getLogsByNotaTraspaso/{id}");

        public async Task<string> VisualizarEntregable(int notaId)
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}smedicos/notasTraspaso/visualizarEntregable/{notaId}");
            request.EnsureSuccessStatusCode();

            var contents = await request.Content.ReadAsStringAsync();

            return contents;
        }

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
