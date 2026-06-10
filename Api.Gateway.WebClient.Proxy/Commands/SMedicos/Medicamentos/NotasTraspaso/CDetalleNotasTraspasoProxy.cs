using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.NotasTraspaso
{
    public interface ICDetalleNotasTraspasoProxy
    {
        Task<DetalleNotaTraspasoDto> CreateDetalleNotaTraspasoAsync(RegistrarDetalleNotaTraspasoCommand command);
        Task<DetalleNotaTraspasoDto> UpdateDetalleNotaTraspasoAsync([FromBody] ActualizarDetalleNotaTraspasoCommand command);
        Task<DetalleNotaTraspasoDto> DeleteDetalleNotaTraspasoAsync([FromBody] EliminarDetalleNotaTraspasoCommand command);
    }

    public class CDetalleNotasTraspasoProxy : ICDetalleNotasTraspasoProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public CDetalleNotasTraspasoProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<DetalleNotaTraspasoDto> CreateDetalleNotaTraspasoAsync([FromBody] RegistrarDetalleNotaTraspasoCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiGatewayUrl}smedicos/detalleNotasTraspaso/createDetalleNotaTraspaso", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DetalleNotaTraspasoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<DetalleNotaTraspasoDto> UpdateDetalleNotaTraspasoAsync([FromBody] ActualizarDetalleNotaTraspasoCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiGatewayUrl}smedicos/detalleNotasTraspaso/updateDetalleNotaTraspaso", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DetalleNotaTraspasoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<DetalleNotaTraspasoDto> DeleteDetalleNotaTraspasoAsync([FromBody] EliminarDetalleNotaTraspasoCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiGatewayUrl}smedicos/detalleNotasTraspaso/deleteDetalleNotaTraspaso", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DetalleNotaTraspasoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
    }
}
