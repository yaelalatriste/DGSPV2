using Api.Gateway.Models.Catalogos.DTOs.CTArticulos;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTArticulos.Queries
{
    public interface IQCTArticuloProxy
    {
        Task<List<CTArticuloDto>> GetAllArticulosAsync();
        Task<List<CTArticuloDto>> GetArticulosByCategoriaAsync(int categoria);
    }

    public class QCTArticuloProxy : IQCTArticuloProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTArticuloProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTArticuloDto>> GetAllArticulosAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/articulos/getArticulos");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTArticuloDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<CTArticuloDto>> GetArticulosByCategoriaAsync(int categoria)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/articulos/getArticulosByCategoria/{categoria}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTArticuloDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
