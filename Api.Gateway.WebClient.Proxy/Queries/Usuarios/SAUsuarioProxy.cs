using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Usuarios;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Usuarios
{
    public interface ISAUsuarioProxy
    {
        Task<UserDto> GetUsuarioByExpediente(int expediente);
    }
    public class SAUsuarioProxy : ISAUsuarioProxy
    {
        private readonly string _usuariosUrl;
        private readonly HttpClient _httpClient;

        public SAUsuarioProxy(HttpClient httpClient, UsuariosUrl usuariosUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _usuariosUrl = usuariosUrl.Value;
        }
        
        public async Task<UserDto> GetUsuarioByExpediente(int expediente)
        {
            var request = await _httpClient.GetAsync($"{_usuariosUrl}sausuarios/getUsuarioByExpediente/{expediente}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<UserDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
