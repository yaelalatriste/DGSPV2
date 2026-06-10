using Api.Gateway.Models.Reportes;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.SMedicos.Queries.Reportes
{
    public interface IQReporteConsultaProxy
    {
        Task<List<int>> GetAniosOfConsultas();
        Task<List<RConsultaDto>> GetAllConsultas(FiltrosSmDto filtros);
        Task<List<RConsultaDto>> GetConsultasMedicas(FiltrosSmDto filtros);
        Task<List<RConsultaDto>> GetConsultasOdontologicas(FiltrosSmDto filtros);
        Task<List<RConsultaDto>> GetRevisionEnfermeria(FiltrosSmDto filtros);
    }

    public class QReporteConsultaProxy : IQReporteConsultaProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public QReporteConsultaProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        // 🔹 Helper para GET
        private async Task<T> GetAsync<T>(string url)
        {
            using var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions);
        }

        // 🔹 Helper para POST
        private async Task<T> PostAsync<T>(string url, object data)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            using var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions);
        }

        // 🔹 Endpoints específicos
        public Task<List<int>> GetAniosOfConsultas() => GetAsync<List<int>>($"{_apiUrls.SMedicosUrl}api/smedicos/reportes/getAniosOfConsultas");

        public Task<List<RConsultaDto>> GetAllConsultas(FiltrosSmDto filtros) => PostAsync<List<RConsultaDto>>($"{_apiUrls.SMedicosUrl}api/smedicos/reportes/getAllConsultas", filtros);

        public Task<List<RConsultaDto>> GetConsultasMedicas(FiltrosSmDto filtros) => PostAsync<List<RConsultaDto>>($"{_apiUrls.SMedicosUrl}api/smedicos/reportes/getConsultasMedicas", filtros);

        public Task<List<RConsultaDto>> GetConsultasOdontologicas(FiltrosSmDto filtros) => PostAsync<List<RConsultaDto>>($"{_apiUrls.SMedicosUrl}api/smedicos/reportes/getConsultasOdontologicas", filtros);

        public Task<List<RConsultaDto>> GetRevisionEnfermeria(FiltrosSmDto filtros) => PostAsync<List<RConsultaDto>>($"{_apiUrls.SMedicosUrl}api/smedicos/reportes/getRevisionEnfermeria", filtros);
    }
}
