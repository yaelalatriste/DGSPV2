using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Seguros.DGSP.Continuidades
{
    public interface IQContinuidadProxy
    {
        Task<List<ContinuidadDto>> GetAllContinuidades();
        Task<ContinuidadDto> GetContinuidadById(int id);
        Task<List<ContinuidadDto>> GetContinuidadesByEstatus(int estatus);
    }
    public class QContinuidadProxy : IQContinuidadProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QContinuidadProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<ContinuidadDto>> GetAllContinuidades() => GetAsync<List<ContinuidadDto>>($"{_apiGatewayUrl}seguros/continuidades/getAllContinuidades");
        public Task<ContinuidadDto> GetContinuidadById(int id) => GetAsync<ContinuidadDto>($"{_apiGatewayUrl}seguros/continuidades/getContinuidadById/{id}");
        public Task<List<ContinuidadDto>> GetContinuidadesByEstatus(int estatus) => GetAsync<List<ContinuidadDto>>($"{_apiGatewayUrl}seguros/continuidades/getContinuidadesByEstatus/{estatus}");

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
