using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesMedicas
{
    public interface IQCTVariablesMedicasProxy
    {
        Task<List<CTVariableMedicaDto>> GetAllVariablesAsync();
        Task<List<CTVariableMedicaDto>> GetVariablesByCategoriaAsync(string categoria);
        Task<CTVariableMedicaDto> GEtVariableByIdAsync(int id);
    }
    public class QCTVariablesMedicasProxy : IQCTVariablesMedicasProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCTVariablesMedicasProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<CTVariableMedicaDto>> GetAllVariablesAsync() => GetAsync<List<CTVariableMedicaDto>>($"{_apiGatewayUrl}catalogos/ctvariablesMedicas/getAllVariablesMedicas");
        public Task<List<CTVariableMedicaDto>> GetVariablesByCategoriaAsync(string categoria) => GetAsync<List<CTVariableMedicaDto>>($"{_apiGatewayUrl}catalogos/ctvariablesMedicas/getVariablesByCategoria/{categoria}");
        public Task<CTVariableMedicaDto> GEtVariableByIdAsync(int id) => GetAsync<CTVariableMedicaDto>($"{_apiGatewayUrl}catalogos/ctvariablesMedicas/getVariableById/{id}");

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
