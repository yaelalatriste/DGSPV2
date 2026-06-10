using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Seguros.DGSP.Continuidades
{
    public interface IQContactoContinuidadProxy
    {
        Task<List<ContactoContinuidadDto>> GetContactoByContinuidadAsync(int estatus);
    }
    public class QContactoContinuidadProxy : IQContactoContinuidadProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QContactoContinuidadProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }
        public Task<List<ContactoContinuidadDto>> GetContactoByContinuidadAsync(int id) => GetAsync<List<ContactoContinuidadDto>>($"{_apiGatewayUrl}seguros/continuidades/contactoContinuidad/getContactoByContinuidad/{id}");

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
