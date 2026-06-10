using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Estatus.Continuidades
{
    public interface IQEstatusContinuidadProxy
    {
        Task<List<EstatusContinuidadDto>> GetAllEstatus();
        Task<EstatusContinuidadDto> GetEstatusById(int id);
    }

    public class QEstatusContinuidadProxy : IQEstatusContinuidadProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QEstatusContinuidadProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<List<EstatusContinuidadDto>> GetAllEstatus()
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}estatus/estatusContinuidades/getAllEstatus");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<EstatusContinuidadDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<EstatusContinuidadDto> GetEstatusById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}estatus/EstatusContinuidades/getEstatusById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<EstatusContinuidadDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
