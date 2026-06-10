using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Modulos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Modulos
{
    public interface IModuloProxy
    {
        Task<List<ModuloDto>> GetAllModulosAsync();
        Task<ModuloDto> GetModuloByIdAsync(int modulo);
        Task<SubmoduloDto> GetSubmoduloByIdAsync(int submodulo);
        Task<OpcionDto> GetOpcionById(int opcion);
        Task<List<SubmoduloDto>> GetSubmodulosByModulo(int modulo);
    }

    public class ModuloProxy : IModuloProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public ModuloProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<ModuloDto>> GetAllModulosAsync() => GetAsync<List<ModuloDto>>($"{_apiGatewayUrl}modulos");
        public Task<ModuloDto> GetModuloByIdAsync(int modulo) => GetAsync<ModuloDto>($"{_apiGatewayUrl}modulos/{modulo}");
        public Task<OpcionDto> GetOpcionById(int opcion) => GetAsync<OpcionDto>($"{_apiGatewayUrl}opciones/getOpcionById/{opcion}");
        public Task<SubmoduloDto> GetSubmoduloByIdAsync(int submodulo) => GetAsync<SubmoduloDto>($"{_apiGatewayUrl}modulos/getSubmoduloById/{submodulo}");
        public Task<List<SubmoduloDto>> GetSubmodulosByModulo(int modulo) => GetAsync<List<SubmoduloDto>>($"{_apiGatewayUrl}submodulos/getSubmodulosByModulo/{modulo}");

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
