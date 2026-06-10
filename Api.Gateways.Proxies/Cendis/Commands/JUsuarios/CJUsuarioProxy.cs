using Api.Gateway.Models.Cendis.Commands.JUsuarios;
using Api.Gateway.Models.Cendis.DTOs.JUsuarios;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Cendis.Commands.JUsuarios
{
    public interface ICJUsuarioProxy
    {
        Task<JUsuarioDto> CreateJUsuario([FromBody] JUsuarioCreateCommand command);
        Task<JUsuarioDto> UpdateJUsuario([FromBody] JUsuarioUpdateCommand command);
    }

    public class CJUsuarioProxy : ICJUsuarioProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CJUsuarioProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<JUsuarioDto> CreateJUsuario([FromBody] JUsuarioCreateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiUrls.CendisUrl}api/cendis/jusuarios/createJUsuario", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<JUsuarioDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<JUsuarioDto> UpdateJUsuario([FromBody] JUsuarioUpdateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.CendisUrl}api/cendis/jusuarios/updateJUsuario", content);
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
