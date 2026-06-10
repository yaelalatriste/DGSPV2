using Api.Gateway.Models.Expedientes.Commands;
using Api.Gateway.Models.Expedientes.DTOs;
using Api.Gateway.Models.Seguros.Commands.Continuidades.Continuidad;
using Api.Gateway.Models.Seguros.Queries.Continuidades.Continuidad;
using Api.Gateway.Models.Seguros.Queries.Continuidades.Entregables;
using Api.Gateway.Models.Seguros.Queries.Continuidades.MediosContacto;
using Api.Gateway.Models.Seguros.Queries.Continuidades.Oficios;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Seguros.Queries.Continuidades
{
    public interface IQContinuidadProxy
    {
        Task<List<VContinuidadDto>> GetAllContinuidades();
        Task<ContinuidadDto> ActualizarContinuidad([FromBody] ContinuidadUpdateCommand command);
        Task<List<VContinuidadDto>> VGetContinuidadesByEstatus(int estatus);
        Task<List<ContinuidadDto>> GetContinuidadesByEstatus(int estatus);
        Task<ContinuidadDto> GetContinuidadById(int id);
        Task<ContinuidadDto> GetContinuidad(int exp);
        Task<List<CorreoContinuidadDto>> GetCorreosByContinuidad(int id);
        Task<List<CEntregableDto>> GetEntregablesByContinuidad(int id);
        Task<string> VisualizarEntregable(int expediente, string tipo, string archivo);
    }
    
    public class QContinuidadProxy : IQContinuidadProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QContinuidadProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public Task<List<VContinuidadDto>> GetAllContinuidades() => GetAsync<List<VContinuidadDto>>($"{_apiUrls.SegurosUrl}api/seguros/continuidades/getAllContinuidades");
        public Task<List<VContinuidadDto>> VGetContinuidadesByEstatus(int estatus) => GetAsync<List<VContinuidadDto>>($"{_apiUrls.SegurosUrl}api/seguros/continuidades/vgetContinuidadesByEstatus/{estatus}");
        public Task<List<ContinuidadDto>> GetContinuidadesByEstatus(int estatus) => GetAsync<List<ContinuidadDto>>($"{_apiUrls.SegurosUrl}api/seguros/continuidades/getContinuidadesByEstatus/{estatus}");
        public Task<ContinuidadDto> GetContinuidad(int exp) => GetAsync<ContinuidadDto>($"{_apiUrls.SegurosUrl}api/seguros/continuidades/getContinuidad/{exp}");
        public Task<ContinuidadDto> GetContinuidadById(int id) => GetAsync<ContinuidadDto>($"{_apiUrls.SegurosUrl}api/seguros/continuidades/getContinuidadById/{id}");
        public Task<List<CorreoContinuidadDto>> GetCorreosByContinuidad(int id) => GetAsync<List<CorreoContinuidadDto>>($"{_apiUrls.SegurosUrl}api/seguros/continuidades/getCorreosByContinuidad/{id}");
        public Task<List<CEntregableDto>> GetEntregablesByContinuidad(int id) => GetAsync<List<CEntregableDto>>($"{_apiUrls.SegurosUrl}api/seguros/continuidades/getEntregablesByContinuidad/{id}");

        public async Task<string> VisualizarEntregable(int expediente, string tipo, string archivo)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.SegurosUrl}api/seguros/continuidades/visualizarEntregable/{expediente}/{tipo}/{archivo}");
            request.EnsureSuccessStatusCode();

            var contents = await request.Content.ReadAsStringAsync();

            return contents;
        }

        public async Task<ContinuidadDto> ActualizarContinuidad([FromBody] ContinuidadUpdateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.SegurosUrl}api/seguros/continuidades/actualizarContinuidad", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ContinuidadDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

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
