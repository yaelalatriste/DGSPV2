using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Estatus.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Estatus.NotasTraspaso
{
    public interface IQCTENotaTraspasoProxy
    {
        public Task<List<ENotaTraspasoDto>> GetAllEstatusAsync();
        public Task<ENotaTraspasoDto> GetEstatusByIdAsync(int id);
    }

    public class QCTENotaTraspasoProxy : IQCTENotaTraspasoProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCTENotaTraspasoProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<ENotaTraspasoDto>> GetAllEstatusAsync() => GetAsync<List<ENotaTraspasoDto>>($"{_apiGatewayUrl}estatus/enotasTraspaso/getAllEstatus");
        public Task<ENotaTraspasoDto> GetEstatusByIdAsync(int id) => GetAsync<ENotaTraspasoDto>($"{_apiGatewayUrl}estatus/enotasTraspaso/getEstatusById/{id}");

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
