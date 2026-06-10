using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Consultorios;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Commands.Catalogos.SMedicos.Consultorios
{
    public interface ICCTConsultorioProxy
    {
        Task<CTConsultorioDto> RegistrarConsultorioAsync(RegistrarCTConsultorioCommand command);
        Task<CTConsultorioDto> ActualizarConsultorioAsync(ActualizarCTConsultorioCommand command);
    }

    public class CCTConsultorioProxy : ICCTConsultorioProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public CCTConsultorioProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<CTConsultorioDto> RegistrarConsultorioAsync([FromBody] RegistrarCTConsultorioCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiGatewayUrl}catalogos/Consultorios/createCTConsultorio", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTConsultorioDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<CTConsultorioDto> ActualizarConsultorioAsync([FromBody] ActualizarCTConsultorioCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiGatewayUrl}catalogos/Consultorios/updateCTConsultorio", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTConsultorioDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
