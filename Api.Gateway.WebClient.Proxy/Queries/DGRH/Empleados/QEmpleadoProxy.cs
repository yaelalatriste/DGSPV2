using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.DGRH.RH.Empleados;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.DGRH.Empleados
{
    public interface IQEmpleadoProxy
    {
        Task<EmpleadoDto> GetEmpleadoByExpediente(int exp);
        Task<List<EmpleadoDto>> GetMovimientosEmpleado(int exp);
    }
    public class QEmpleadoProxy : IQEmpleadoProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QEmpleadoProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }
       
        public Task<EmpleadoDto> GetEmpleadoByExpediente(int exp) => GetAsync<EmpleadoDto>($"{_apiGatewayUrl}dgrh/empleado/getEmpleadoByExpediente/{exp}");
        public Task<List<EmpleadoDto>> GetMovimientosEmpleado(int exp) => GetAsync<List<EmpleadoDto>>($"{_apiGatewayUrl}dgrh/empleado/getMovimientosEmpleado/{exp}");

        private async Task<T> GetAsync<T>(string url)
        {
            using var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions);
        }

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

    }
}

