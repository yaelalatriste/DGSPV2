using Api.Gateway.Models.Catalogos.DTOs.CTAreas;
using Api.Gateway.Models.Cendis.DTOs.CendisUsuarios;
using Api.Gateway.Models.Cendis.DTOs.Justificantes;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Cendis.Queries.CendisUsuarios
{
    public interface IQCendisUsuariosProxy
    {
        Task<List<CendisUsuarioDto>> GetAllCendisUsuarios();
        Task<List<CendisUsuarioDto>> GetCendisByUsuario(string usuario);
        Task<List<CendisUsuarioDto>> GetUsuariosByCendi(int cendi);
    }

    public class QCendisUsuariosProxy : IQCendisUsuariosProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCendisUsuariosProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CendisUsuarioDto>> GetAllCendisUsuarios()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CendisUrl}api/cendis/cendisUsuarios/getAllCendisUsuarios");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CendisUsuarioDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<CendisUsuarioDto>> GetCendisByUsuario(string usuario)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CendisUrl}api/cendis/cendisUsuarios/getCendisByUsuario/{usuario}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CendisUsuarioDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<CendisUsuarioDto>> GetUsuariosByCendi(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CendisUrl}api/cendis/cendisUsuarios/getUsuariosByCendi/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CendisUsuarioDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
