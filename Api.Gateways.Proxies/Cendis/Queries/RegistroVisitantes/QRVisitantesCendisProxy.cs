using Api.Gateway.Models.Catalogos.DTOs.CTAreas;
using Api.Gateway.Models.Cendis.DTOs.Justificantes;
using Api.Gateway.Models.Cendis.DTOs.RegistroVisitantes;
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

namespace Api.Gateway.Proxies.Cendis.Queries.RegistroVisitantes
{
    public interface IQRVisitantesCendisProxy
    {
        Task<List<RegistroVisitantesDto>> GetAllRVisitantes();
        Task<List<RegistroVisitantesDto>> GetRVisitantsByDay(string dia);
    }

    public class QRVisitantesCendisProxy : IQRVisitantesCendisProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QRVisitantesCendisProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<RegistroVisitantesDto>> GetAllRVisitantes()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CendisUrl}api/cendis/registroVisitantes/getAllRVisitantes");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<RegistroVisitantesDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<List<RegistroVisitantesDto>> GetRVisitantsByDay(string dia)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CendisUrl}api/cendis/registroVisitantes/getRegistrosByDia/{dia}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<RegistroVisitantesDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
