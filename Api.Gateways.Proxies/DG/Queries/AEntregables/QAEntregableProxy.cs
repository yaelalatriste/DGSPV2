using Api.Gateway.Models.DG.Queries.AEntregable;
using Api.Gateway.Models.Modulos.DTOs;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.DG.Queries.AEntregables
{
    public interface IQAEntregableProxy
    {
        Task<List<AEntregableDto>> GetAllEntregablesAsync();
        Task<List<AEntregableDto>> GetEntregablesByAcuerdo(int acuerdo);
        Task<AEntregableDto> GetEntregableById(int id);
    }

    public class QAEntregableProxy : IQAEntregableProxy 
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QAEntregableProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<AEntregableDto>> GetAllEntregablesAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.DGUrl}api/dg/entregablesAcuerdos");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<AEntregableDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<AEntregableDto>> GetEntregablesByAcuerdo(int acuerdo)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.DGUrl}api/dg/entregablesAcuerdos/getAEntregableByAcuerdo/{acuerdo}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<AEntregableDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<AEntregableDto> GetEntregableById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.DGUrl}api/dg/entregablesAcuerdos/getAcuerdoById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<AEntregableDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
