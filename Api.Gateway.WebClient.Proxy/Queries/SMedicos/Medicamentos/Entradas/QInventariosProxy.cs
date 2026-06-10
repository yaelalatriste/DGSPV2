using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Movimientos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Entradas
{
    public interface IQInventariosProxy
    {
        Task<List<LoteDto>> GetLotesAsync();
        Task<LoteDto> GetLoteByIdAsync(int id);
        Task<LoteDto> GetDatosByLoteAsync(string lote);
        Task<List<LoteDto>> GetMedicamentosPorLote(string lote, int consultorio);
        Task<LoteDto> GetDatosByLoteConsultorioAsync(string lote, int consultorio);
        Task<LoteDto> GetDatosByLoteConsultorioMedicamentoAsync(string lote, int consultorio, int medicamento);
    }

    public class QInventariosProxy : IQInventariosProxy 
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QInventariosProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<LoteDto>> GetLotesAsync() => GetAsync<List<LoteDto>>($"{_apiGatewayUrl}smedicos/inventarios/lotes");
        public Task<LoteDto> GetLoteByIdAsync(int id) => GetAsync<LoteDto>($"{_apiGatewayUrl}smedicos/inventarios/getLoteById/{id}");
        public Task<LoteDto> GetDatosByLoteAsync(string lote) => GetAsync<LoteDto>($"{_apiGatewayUrl}smedicos/inventarios/getDatosByLote/{lote}");
        public Task<List<LoteDto>> GetMedicamentosPorLote(string lote, int consultorio) => GetAsync<List<LoteDto>>($"{_apiGatewayUrl}smedicos/inventarios/getMedicamentosByLoteConsultorio/{lote}/{consultorio}");
        public Task<LoteDto> GetDatosByLoteConsultorioAsync(string lote, int consultorio) => GetAsync<LoteDto>($"{_apiGatewayUrl}smedicos/inventarios/getDatosByLoteConsultorio/{lote}/{consultorio}");
        public Task<LoteDto> GetDatosByLoteConsultorioMedicamentoAsync(string lote, int consultorio, int medicamento) => GetAsync<LoteDto>($"{_apiGatewayUrl}smedicos/inventarios/getDatosByLoteConsultorioMedicamento/{lote}/{consultorio}/{medicamento}");

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
