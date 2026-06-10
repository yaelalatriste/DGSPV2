using Api.Gateway.Models.Estatus.DTOs;
using Api.Gateway.Models.Estatus.DTOs.FlujoJustificantes;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Estatus.Queries.FlujoJustificantes
{
    public interface IQFlujoJustificantesProxy
    {
        Task<List<FlujoJustificanteDto>> GetAllFlujosJustificantes();
        Task<List<FlujoJustificanteDto>> GetAllFlujoByModulo(int modulo, int submodulo);
        Task<List<FlujoJustificanteDto>> GetFlujoByEstatus(int modulo, int submodulo, int estatus);
    }

    public class QFlujoJustificantesProxy : IQFlujoJustificantesProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QFlujoJustificantesProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<FlujoJustificanteDto>> GetAllFlujosJustificantes()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.EstatusUrl}api/estatus/flujoJustificante/getAllFlujosJustificantes");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<FlujoJustificanteDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<FlujoJustificanteDto>> GetAllFlujoByModulo(int modulo, int submodulo)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.EstatusUrl}api/estatus/flujoJustificante/getAllFlujoByModulo/{modulo}/{submodulo}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<FlujoJustificanteDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }        
        
        public async Task<List<FlujoJustificanteDto>> GetFlujoByEstatus(int estatus, int modulo, int submodulo)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.EstatusUrl}api/estatus/flujoJustificante/getFlujoByEstatus/{estatus}/{modulo}/{submodulo}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<FlujoJustificanteDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }        
    }
}
