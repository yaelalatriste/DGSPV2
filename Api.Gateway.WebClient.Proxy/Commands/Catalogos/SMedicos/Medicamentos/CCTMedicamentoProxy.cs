using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Medicamentos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Commands.Catalogos.SMedicos.Medicamentos
{
    public interface ICCTMedicamentoProxy
    {
        Task<CTMedicamentoDto> RegistrarMedicamentoAsync(RegistrarCTMedicamentoCommand command);
        Task<CTMedicamentoDto> ActualizarMedicamentoAsync(ActualizarCTMedicamentoCommand command);
    }

    public class CCTMedicamentoProxy : ICCTMedicamentoProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public CCTMedicamentoProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<CTMedicamentoDto> RegistrarMedicamentoAsync([FromBody] RegistrarCTMedicamentoCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiGatewayUrl}catalogos/medicamentos/createCTMedicamento", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTMedicamentoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<CTMedicamentoDto> ActualizarMedicamentoAsync([FromBody] ActualizarCTMedicamentoCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiGatewayUrl}catalogos/medicamentos/updateCTMedicamento", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTMedicamentoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
