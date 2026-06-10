using Api.Gateway.Models.Expedientes.DTOs;
using Api.Gateway.Models.Seguros.Queries.Reportes;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Seguros.Queries.Reportes
{
    public interface IRJubiladosProxy
    {
        Task<List<RJubiladoDto>> GetReporteFemenino();
        Task<List<RJubiladoDto>> GetReporteMasculino();
    }

    public class RJubiladosProxy : IRJubiladosProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public RJubiladosProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<RJubiladoDto>> GetReporteFemenino()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SegurosUrl}api/seguros/reportes/jubilados/getRJubiladosFemenino");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<RJubiladoDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<RJubiladoDto>> GetReporteMasculino()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SegurosUrl}api/seguros/reportes/jubilados/getRJubiladosMasculino");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<RJubiladoDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
