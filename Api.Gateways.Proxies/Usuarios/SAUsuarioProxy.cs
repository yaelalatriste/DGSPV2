using Api.Gateway.Models.Usuarios;
using Api.Gateway.Models.Usuarios.DTOs;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Usuarios
{
    public interface ISAUsuarioProxy
    {
        Task<UserDto> GetUsuarioByExpediente(int expediente);
    }

    public class SAUsuarioProxy : ISAUsuarioProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public SAUsuarioProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }
        
        public async Task<UserDto> GetUsuarioByExpediente(int expediente)
        {
            try
            {
                var request = await _httpClient.GetAsync($"{_apiUrls.UsuariosUrl}api/sausuarios/getUsuarioByExpediente/{expediente}");
                request.EnsureSuccessStatusCode();

                return JsonSerializer.Deserialize<UserDto>(
                    await request.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );
            }
            catch (HttpRequestException e)
            {
                string ms = e.Message;
                return new UserDto();
            }
        }
    }
}
