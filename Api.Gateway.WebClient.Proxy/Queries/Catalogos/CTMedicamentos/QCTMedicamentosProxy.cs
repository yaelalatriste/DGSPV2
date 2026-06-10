using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Catalogos.CTMedicamentos
{
    public interface IQCTMedicamentosProxy
    {
        Task<List<CTMedicamentoDto>> GetAllMedicamentosAsync();
        Task<List<CTMedicamentoDto>> GetMedicamentosByAnioAsync(int anio);
        Task<CTMedicamentoDto> GetMedicamentoByIdAsync(int id);
    }
    public class QCTMedicamentosProxy : IQCTMedicamentosProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCTMedicamentosProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<CTMedicamentoDto>> GetAllMedicamentosAsync() => GetAsync<List<CTMedicamentoDto>>($"{_apiGatewayUrl}catalogos/medicamentos/getAllMedicamentos");
        public Task<List<CTMedicamentoDto>> GetMedicamentosByAnioAsync(int anio) => GetAsync<List<CTMedicamentoDto>>($"{_apiGatewayUrl}catalogos/medicamentos/getMedicamentosByAnio/{anio}");
        public Task<CTMedicamentoDto> GetMedicamentoByIdAsync(int id) => GetAsync<CTMedicamentoDto>($"{_apiGatewayUrl}catalogos/medicamentos/getMedicamentoById/{id}");

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
