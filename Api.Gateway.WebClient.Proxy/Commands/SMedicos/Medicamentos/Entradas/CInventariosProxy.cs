using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.Entradas
{
    public interface ICInventariosProxy
    {
        Task<LoteDto> RegistrarLoteAsync(RegistrarLoteMedicamentoCommand command);
    }

    public class CInventariosProxy : ICInventariosProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public CInventariosProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<LoteDto> RegistrarLoteAsync([FromBody] RegistrarLoteMedicamentoCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiGatewayUrl}smedicos/inventarios/createLote", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<LoteDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
