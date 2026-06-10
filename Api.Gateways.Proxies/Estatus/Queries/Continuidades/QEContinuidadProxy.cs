using Api.Gateway.Models.Estatus.DTOs;
using Api.Gateway.Models.Estatus.DTOs.Acuerdos;
using Api.Gateway.Models.Estatus.DTOs.Continuidades;
using Api.Gateway.Models.Estatus.DTOs.FlujoJustificantes;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Estatus.Queries.Continuidades
{
    public interface IQEContinuidadesProxy
    {
        Task<List<EContinuidadDto>> GetAllEstatus();
        Task<EContinuidadDto> GetEstatusById(int id);
    }

    public class QEContinuidadesProxy : IQEContinuidadesProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QEContinuidadesProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<EContinuidadDto>> GetAllEstatus()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.EstatusUrl}api/estatus/continuidades/getAllEstatus");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<EContinuidadDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<EContinuidadDto> GetEstatusById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.EstatusUrl}api/estatus/continuidades/getEstatusById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<EContinuidadDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
