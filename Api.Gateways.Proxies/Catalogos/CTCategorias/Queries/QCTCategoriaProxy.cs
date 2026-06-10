using Api.Gateway.Models.Catalogos.DTOs.CTCategorias;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTCategorias.Queries
{
    public interface IQCTCategoriaProxy
    {
        Task<List<CTCategoriaDto>> GetAllCategoriasAsync();
        Task<List<CTCategoriaDto>> GetCategoriasByTipo(string tipo);
        Task<CTCategoriaDto> GetCategoriaByIdAsync(int Categoria);
    }
    public class QCTCategoriaProxy : IQCTCategoriaProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTCategoriaProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTCategoriaDto>> GetAllCategoriasAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/categorias/getAllCategorias");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTCategoriaDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<List<CTCategoriaDto>> GetCategoriasByTipo(string tipo)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/categorias/getCategoriasByTipo/{tipo}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTCategoriaDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CTCategoriaDto> GetCategoriaByIdAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/categorias/getCategoriaById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTCategoriaDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
