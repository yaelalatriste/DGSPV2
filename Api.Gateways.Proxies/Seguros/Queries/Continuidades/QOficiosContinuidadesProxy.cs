using Api.Gateway.Models.Seguros.Queries.Continuidades.Oficios;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Seguros.Queries.Continuidades
{
    public interface IQOficiosContinuidadesProxy
    {
        Task<List<VOficioDto>> GetOficios();
        Task<OficioContinuidadDto> GetOficioById(int id);
        Task<List<ContinuidadOficioDto>> GetContinuidadesByOficio(int oficio);
    }
    public class QOficiosContinuidadesProxy : IQOficiosContinuidadesProxy
    {

        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QOficiosContinuidadesProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }
        public Task<List<VOficioDto>> GetOficios() => GetAsync<List<VOficioDto>>($"{_apiUrls.SegurosUrl}api/seguros/continuidades/oficios/getOficiosContinuidades");
        public Task<OficioContinuidadDto> GetOficioById(int id) => GetAsync<OficioContinuidadDto>($"{_apiUrls.SegurosUrl}api/seguros/continuidades/oficios/getOficioById/{id}");
        public Task<List<ContinuidadOficioDto>> GetContinuidadesByOficio(int oficio) => GetAsync<List<ContinuidadOficioDto>>($"{_apiUrls.SegurosUrl}api/seguros/continuidades/oficios/getContinuidadesByOficio/{oficio}");

        private async Task<T> GetAsync<T>(string url)
        {
            using var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions);
        }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

    }
}
