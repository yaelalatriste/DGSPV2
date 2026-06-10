using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.SMedicos.Siacom.Catalogos
{
    public interface IQCTTipoConsultaProxy
    {
        Task<List<CTTipoConsultaDto>> GetAllTiposConsultas();
        Task<CTTipoConsultaDto> GetTipoConsultaById(int id);
    }
    public class QCTTipoConsultaProxy : IQCTTipoConsultaProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCTTipoConsultaProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<List<CTTipoConsultaDto>> GetAllTiposConsultas()
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}smedicos/tiposConsultas/getAllTiposConsultas");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTTipoConsultaDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        public async Task<CTTipoConsultaDto> GetTipoConsultaById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}smedicos/tiposConsulta/getTipoConsultaById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTTipoConsultaDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
