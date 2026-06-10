using Api.Gateway.Models.Expedientes.Commands;
using Api.Gateway.Models.Expedientes.DTOs;
using Api.Gateway.Models.Seguros.Commands.Continuidades.CEntregables;
using Api.Gateway.Models.Seguros.Commands.Continuidades.Continuidad;
using Api.Gateway.Models.Seguros.Commands.Continuidades.MediosContacto;
using Api.Gateway.Models.Seguros.Commands.Continuidades.OficiosContinuidades;
using Api.Gateway.Models.Seguros.Queries.Continuidades.Continuidad;
using Api.Gateway.Models.Seguros.Queries.Continuidades.Entregables;
using Api.Gateway.Models.Seguros.Queries.Continuidades.MediosContacto;
using Api.Gateway.Models.Seguros.Queries.Continuidades.Oficios;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Seguros.Commands.Continuidades
{
    public interface ICContinuidadesProxy
    {
        Task<ContinuidadDto> CreateSContinuidades([FromBody] ContinuidadCreateCommand command);
        Task<ContinuidadDto> UpdateSContinuidades([FromBody] ContinuidadUpdateCommand command);
        Task<CorreoContinuidadDto> CreateCorreo([FromBody] CorreoCreateCommand command);
        Task<CorreoContinuidadDto> UpdateCorreo([FromBody] CorreoUpdateCommand command);
        Task<CEntregableDto> CreateEntregable([FromForm] CEntregableCreateCommand command);
        Task<CEntregableDto> UpdateEntregable([FromForm] CEntregableUpdateCommand command);
    }

    public  class CContinuidadesProxy : ICContinuidadesProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CContinuidadesProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<ContinuidadDto> CreateSContinuidades([FromBody] ContinuidadCreateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiUrls.SegurosUrl}api/seguros/scontinuidades/createContinuidad", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ContinuidadDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<ContinuidadDto> UpdateSContinuidades([FromBody] ContinuidadUpdateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.SegurosUrl}api/seguros/scontinuidades/updateContinuidad", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ContinuidadDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CorreoContinuidadDto> CreateCorreo([FromBody] CorreoCreateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiUrls.SegurosUrl}api/seguros/continuidades/createCorreo", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CorreoContinuidadDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<CorreoContinuidadDto> UpdateCorreo([FromBody] CorreoUpdateCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.SegurosUrl}api/seguros/continuidades/updateCorreo", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CorreoContinuidadDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CEntregableDto> CreateEntregable([FromForm] CEntregableCreateCommand command)
        {
            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(command.UsuarioId.ToString()), "UsuarioId");
            formContent.Add(new StringContent(command.ContinuidadId.ToString()), "ContinuidadId");
            formContent.Add(new StringContent(command.EstatusId.ToString()), "EstatusId");
            formContent.Add(new StringContent(command.Expediente.ToString()), "Expediente");
            formContent.Add(new StringContent(command.Importe.ToString()), "Importe");
            formContent.Add(new StringContent(command.FechaEnvioSP.ToString()), "FechaEnvioSP");
            formContent.Add(new StringContent(command.Pagado.ToString()), "Pagado");
            formContent.Add(new StringContent(command.Tipo.ToString()), "Tipo");
            if (command.Archivo != null)
            {
                var fileStreamContentPDF = new StreamContent(command.Archivo.OpenReadStream());
                fileStreamContentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(command.Archivo.ContentType);
                formContent.Add(fileStreamContentPDF, name: "Archivo", command.Archivo.FileName);
            }

            var request = await _httpClient.PostAsync($"{_apiUrls.SegurosUrl}api/seguros/continuidades/createEntregable", formContent);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CEntregableDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<CEntregableDto> UpdateEntregable([FromForm] CEntregableUpdateCommand command)
        {
            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(command.UsuarioId.ToString()), "UsuarioId");
            formContent.Add(new StringContent(command.ContinuidadId.ToString()), "ContinuidadId");
            formContent.Add(new StringContent(command.EstatusId.ToString()), "EstatusId");
            formContent.Add(new StringContent(command.Expediente.ToString()), "Expediente");
            formContent.Add(new StringContent(command.Importe.ToString()), "Importe");
            formContent.Add(new StringContent(command.FechaEnvioSP.ToString()), "FechaEnvioSP");
            formContent.Add(new StringContent(command.Pagado.ToString()), "Pagado");
            formContent.Add(new StringContent(command.Tipo.ToString()), "Tipo");
            if (command.Archivo != null)
            {
                var fileStreamContentPDF = new StreamContent(command.Archivo.OpenReadStream());
                fileStreamContentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(command.Archivo.ContentType);
                formContent.Add(fileStreamContentPDF, name: "Archivo", command.Archivo.FileName);
            }

            var request = await _httpClient.PutAsync($"{_apiUrls.SegurosUrl}api/seguros/continuidades/updateEntregable", formContent);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<CEntregableDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
