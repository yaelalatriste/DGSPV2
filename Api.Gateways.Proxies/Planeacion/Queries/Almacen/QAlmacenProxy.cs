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

namespace Api.Gateway.Proxies.Planeacion.Queries.Almacen
{
    public interface IQAlmacenProxy
    {
        Task<List<AlmacenDto>> GetAllAlmacenAsync();
    }
    public class QAlmacenProxy : IQAlmacenProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QAlmacenProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<AlmacenDto>> GetAllAlmacenAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/almacen/getAllAlmacen");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<AlmacenDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
