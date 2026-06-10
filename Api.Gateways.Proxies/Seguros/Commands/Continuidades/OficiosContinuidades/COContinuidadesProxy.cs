using Api.Gateway.Models.Seguros.Commands.Continuidades.OficiosContinuidades;
using Api.Gateway.Models.Seguros.Queries.Continuidades.Oficios;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Seguros.Commands.Continuidades.OficiosContinuidades
{
    public interface ICOContinuidadesProxy
    {
        Task<OficioContinuidadDto> CreateOficio([FromBody] OContinuidadCreateCommand command);
        Task<OficioContinuidadDto> UpdateOficio([FromBody] OContinuidadUpdateCommand command);
        Task<List<ContinuidadOficioDto>> CreateContinuidadesByOficio([FromBody] List<COficioCreateCommand> command);
    }
    public class COContinuidadesProxy : ICOContinuidadesProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public COContinuidadesProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<OficioContinuidadDto> CreateOficio([FromBody] OContinuidadCreateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiUrls.SegurosUrl}api/seguros/continuidades/oficios/createOficio", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<OficioContinuidadDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        public async Task<OficioContinuidadDto> UpdateOficio([FromBody] OContinuidadUpdateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.SegurosUrl}api/seguros/continuidades/oficios/updateOficio", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<OficioContinuidadDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        public async Task<List<ContinuidadOficioDto>> CreateContinuidadesByOficio([FromBody] List<COficioCreateCommand> command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiUrls.SegurosUrl}api/seguros/continuidades/oficios/createContinuidadesByOficio", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<List<ContinuidadOficioDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
