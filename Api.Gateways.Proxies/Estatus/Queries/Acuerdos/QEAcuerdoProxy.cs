using Api.Gateway.Models.Estatus.DTOs;
using Api.Gateway.Models.Estatus.DTOs.Acuerdos;
using Api.Gateway.Models.Estatus.DTOs.FlujoJustificantes;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Estatus.Queries.Acuerdos
{
    public interface IQEAcuerdoProxy
    {
        Task<List<EAcuerdoDto>> GetAllEstatus();
        Task<EAcuerdoDto> GetEstatusById(int id);
    }

    public class QEAcuerdoProxy : IQEAcuerdoProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QEAcuerdoProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<EAcuerdoDto>> GetAllEstatus()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.EstatusUrl}api/estatus/acuerdos/getAllEstatus");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<EAcuerdoDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<EAcuerdoDto> GetEstatusById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.EstatusUrl}api/estatus/acuerdos/getEstatusById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<EAcuerdoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
