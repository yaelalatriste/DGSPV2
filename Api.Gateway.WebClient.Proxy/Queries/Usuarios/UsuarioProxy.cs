using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Usuarios;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Usuarios
{
    public interface IUsuarioProxy
    {
        Task<List<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto> GetUsuarioById(string usuario);
        Task<List<UsuarioDto>> GetUsuariosByServicio(string usuario, int servicio);
        Task<UserDto> GetUsuarioByExpediente(int expediente);
    }
    public class UsuarioProxy : IUsuarioProxy
    {
        private readonly string _usuariosUrl;
        private readonly HttpClient _httpClient;

        public UsuarioProxy(HttpClient httpClient, UsuariosUrl usuariosUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _usuariosUrl = usuariosUrl.Value;
        }
        public async Task<List<UsuarioDto>> GetAllAsync()
        {
            var request = await _httpClient.GetAsync($"{_usuariosUrl}usuarios/getAllUsuarios");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<UsuarioDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<UsuarioDto> GetUsuarioById(string usuario)
        {
            var request = await _httpClient.GetAsync($"{_usuariosUrl}usuarios/getUsuarioById/{usuario}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<UsuarioDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<UserDto> GetUsuarioByExpediente(int expediente)
        {
            var request = await _httpClient.GetAsync($"{_usuariosUrl}usuarios/getUsuarioByExpediente/{expediente}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<UserDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<List<UsuarioDto>> GetUsuariosByServicio(string usuario, int servicio)
        {
            var request = await _httpClient.GetAsync($"{_usuariosUrl}usuarios/getUsuariosByServicio/{usuario}/{servicio}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<UsuarioDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
