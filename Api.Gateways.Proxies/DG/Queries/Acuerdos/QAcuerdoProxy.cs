using Api.Gateway.Models.DG.Queries.Acuerdos;
using Api.Gateway.Models.Planeacion.Queries.ActividadesMensuales;
using Api.Gateway.Models.Planeacion.Queries.Almacen;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.DG.Queries.Acuerdos
{
    public interface IQAcuerdoProxy
    {
        Task<List<AcuerdoDto>> GetAllAcuerdosAsync();
        Task<AcuerdoDto> GetAcuerdoById(int id);
    }
    public class QAcuerdoProxy : IQAcuerdoProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QAcuerdoProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<AcuerdoDto>> GetAllAcuerdosAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.DGUrl}api/dg/acuerdos");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<AcuerdoDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<AcuerdoDto> GetAcuerdoById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.DGUrl}api/dg/acuerdos/getAcuerdoById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<AcuerdoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
