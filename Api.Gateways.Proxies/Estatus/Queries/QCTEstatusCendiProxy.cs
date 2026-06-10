using Api.Gateway.Models.Catalogos.DTOs.CTAreas;
using Api.Gateway.Models.Cendis.DTOs.Justificantes;
using Api.Gateway.Models.Estatus.DTOs.CTECendis;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Estatus.Queries
{
    public interface IQCTEstatusCendiProxy
    {
        Task<List<CTECendisDto>> GetAllEstatus();
        Task<List<CTECendisDto>> GetEstatusCendis(int modulo, int submodulo);
        Task<CTECendisDto> GetEstatusById(int id);
    }

    public class QCTEstatusCendiProxy : IQCTEstatusCendiProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QCTEstatusCendiProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<CTECendisDto>> GetAllEstatus()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.EstatusUrl}api/estatus/cendis/getAllEstatus");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTECendisDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CTECendisDto> GetEstatusById(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.EstatusUrl}api/estatus/cendis/getEstatusById/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CTECendisDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<List<CTECendisDto>> GetEstatusCendis(int modulo, int submodulo)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.EstatusUrl}api/estatus/cendis/getEstatusCendis/{modulo}/{submodulo}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<CTECendisDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
