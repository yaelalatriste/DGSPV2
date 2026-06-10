using Api.Gateway.Models.DGRH.Empleados;
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

namespace Api.Gateway.Proxies.DGRH.Queries.Empleado
{
    public interface IQEmpleadoProxy
    {
        Task<List<EmpleadoDto>> GetAllEmpleados();
        Task<EmpleadoDto> GetEmpleadoByExpediente(int exp);
        Task<List<EmpleadoDto>> GetMovimientosEmpleado(int exp);
    }

    public class QEmpleadoProxy : IQEmpleadoProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public QEmpleadoProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
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

        public Task<EmpleadoDto> GetEmpleadoByExpediente(int exp) => GetAsync<EmpleadoDto>($"{_apiUrls.DgrhUrl}api/dgrh/empleado/getEmpleadoByExpediente/{exp}");
        public Task<List<EmpleadoDto>> GetMovimientosEmpleado(int exp) => GetAsync<List<EmpleadoDto>>($"{_apiUrls.DgrhUrl}api/dgrh/empleado/getMovimientosEmpleado/{exp}");

        public Task<List<EmpleadoDto>> GetAllEmpleados() => GetAsync<List<EmpleadoDto>>($"{_apiUrls.DgrhUrl}api/dgrh/empleado/getAllEmpleados");
    }
}
