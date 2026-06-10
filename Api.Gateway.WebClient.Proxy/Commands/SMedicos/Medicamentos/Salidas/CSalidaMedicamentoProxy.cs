using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Salidas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.Salidas
{
    public interface ICSalidaMedicamentoProxy
    {
        Task<SalidaMedicamentoDto> RegistrarSalidaMedicamentoAsync(RegistrarSalidaMedicamentoCommand command);
    }

    public class CSalidaMedicamentoProxy : ICSalidaMedicamentoProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public CSalidaMedicamentoProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<SalidaMedicamentoDto> RegistrarSalidaMedicamentoAsync([FromBody] RegistrarSalidaMedicamentoCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiGatewayUrl}smedicos/salidaMedicamento/createSalida", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<SalidaMedicamentoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
