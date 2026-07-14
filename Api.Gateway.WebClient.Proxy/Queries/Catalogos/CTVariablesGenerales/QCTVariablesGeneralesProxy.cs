using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesGenerales
{
    public interface IQCTVariablesGeneralesProxy
    {
        Task<List<CTVariableGeneralDto>> GetAllVariablesGeneralesAsync();
        Task<CTVariableGeneralDto> GetVariableGeneralById(int id);
        Task<CTVariableGeneralDto> GetVariableGeneralxAnioAbreviacion(int anio, string abreviacion);
    }
    public class QCTVariablesGeneralesProxy : IQCTVariablesGeneralesProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCTVariablesGeneralesProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<CTVariableGeneralDto>> GetAllVariablesGeneralesAsync() => GetAsync<List<CTVariableGeneralDto>>($"{_apiGatewayUrl}catalogos/CTVariablesGenerales/getAllVariablesGenerales");
        public Task<CTVariableGeneralDto> GetVariableGeneralById(int id) => GetAsync<CTVariableGeneralDto>($"{_apiGatewayUrl}catalogos/CTVariablesGenerales/getVariableGeneralById/{id}");
        public Task<CTVariableGeneralDto> GetVariableGeneralxAnioAbreviacion(int anio, string abreviacion) => GetAsync<CTVariableGeneralDto>($"{_apiGatewayUrl}catalogos/CTVariablesGenerales/getVariableByAnioxAbreviacion/{anio}/{abreviacion}");

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
