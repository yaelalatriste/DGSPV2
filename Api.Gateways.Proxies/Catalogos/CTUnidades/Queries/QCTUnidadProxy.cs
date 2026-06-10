using Api.Gateway.Models.Catalogos.DTOs.CTProcesos;
using Api.Gateway.Models.Catalogos.DTOs.CTUnidades;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Catalogos.CTUnidades.Queries
{
    public interface IQCTUnidadProxy
    {
        Task<List<CTUnidadDto>> GetAllUnidades();
        Task<CTUnidadDto> GetUnidadById(int id);
    }
    public class QCTUnidadProxy : IQCTUnidadProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTUnidadProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTUnidadDto>> GetAllUnidades()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctunidades/getAllUnidades");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTUnidadDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CTUnidadDto> GetUnidadById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogoUrl}api/catalogos/ctunidades/getUnidadById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTUnidadDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
