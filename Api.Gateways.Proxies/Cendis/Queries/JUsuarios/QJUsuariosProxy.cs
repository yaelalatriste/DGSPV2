using Api.Gateway.Models.Cendis.DTOs.JUsuarios;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Cendis.Queries.JUsuarios
{
    public interface IQJUsuariosProxy
    {
        Task<List<JUsuarioDto>> GetAllJUsuarios();
        Task<JUsuarioDto> GetJUsuarioById(int id);

        Task<JUsuarioDto> GetJUsuarioByExpediente(int expediente);
    }

    public class QJUsuariosProxy : IQJUsuariosProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QJUsuariosProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<JUsuarioDto>> GetAllJUsuarios()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CendisUrl}api/cendis/jusuarios/getAllJUsuarios");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<JUsuarioDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<JUsuarioDto> GetJUsuarioById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CendisUrl}api/cendis/jusuarios/getJUsuarioById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<JUsuarioDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<JUsuarioDto> GetJUsuarioByExpediente(int expediente)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CendisUrl}api/cendis/jusuarios/getJUsuarioByExpediente/{expediente}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<JUsuarioDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
