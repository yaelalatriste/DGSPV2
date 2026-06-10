using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Api.Gateway.Proxies.Config;
using Api.Gateway.Models.Expedientes.Commands;
using Api.Gateway.Models.Expedientes.DTOs;
using System.Net.Http.Headers;

namespace Api.Gateway.Proxies.SMedicos.Commands.Expedientes
{
    public interface ICExpedienteSMedicosProxy
    {
        Task<ExpedienteDto> CreateExpediente([FromForm] ExpedienteCreateCommand command);
        Task<ExpedienteDto> UpdateExpediente([FromForm] ExpedienteUpdateCommand command);
        Task<ExpedienteDto> DeleteExpediente([FromBody] ExpedienteDeleteCommand command);
    }

    public class CExpedienteSMedicosProxy : ICExpedienteSMedicosProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CExpedienteSMedicosProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<ExpedienteDto> CreateExpediente([FromForm] ExpedienteCreateCommand command)
        {
            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(command.UsuarioId.ToString()), "UsuarioId");
            formContent.Add(new StringContent(command.CategoriaId.ToString()), "CategoriaId");
            formContent.Add(new StringContent(command.EntregableId.ToString()), "EntregableId");
            formContent.Add(new StringContent(command.Anio.ToString()), "Anio");
            formContent.Add(new StringContent(command.Observaciones.ToString()), "Observaciones");
            if (command.Archivo != null)
            {
                formContent.Add(new StringContent(command.Area.ToString()), "Area");
                formContent.Add(new StringContent(command.Categoria.ToString()), "Categoria");
                formContent.Add(new StringContent(command.Entregable.ToString()), "Entregable");
                var fileStreamContentPDF = new StreamContent(command.Archivo.OpenReadStream());
                fileStreamContentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(command.Archivo.ContentType);
                formContent.Add(fileStreamContentPDF, name: "Archivo", command.Archivo.FileName);
            }

            var request = await _httpClient.PostAsync($"{_apiUrls.SMedicosUrl}api/smedicos/expedientes/createExpediente", formContent);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ExpedienteDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<ExpedienteDto> UpdateExpediente([FromForm] ExpedienteUpdateCommand command)
        {
            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(command.Id.ToString()), "Id");
            formContent.Add(new StringContent(command.UsuarioId.ToString()), "UsuarioId");
            formContent.Add(new StringContent(command.CategoriaId.ToString()), "CategoriaId");
            formContent.Add(new StringContent(command.EntregableId.ToString()), "EntregableId");
            formContent.Add(new StringContent(command.Anio.ToString()), "Anio");
            formContent.Add(new StringContent(command.Observaciones.ToString()), "Observaciones");
            if (command.Archivo != null)
            {
                formContent.Add(new StringContent(command.Area.ToString()), "Area");
                formContent.Add(new StringContent(command.Categoria.ToString()), "Categoria");
                formContent.Add(new StringContent(command.Entregable.ToString()), "Entregable");
                var fileStreamContentPDF = new StreamContent(command.Archivo.OpenReadStream());
                fileStreamContentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(command.Archivo.ContentType);
                formContent.Add(fileStreamContentPDF, name: "Archivo", command.Archivo.FileName);
            }

            var request = await _httpClient.PutAsync($"{_apiUrls.SMedicosUrl}api/smedicos/expedientes/updateExpediente", formContent);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ExpedienteDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<ExpedienteDto> DeleteExpediente([FromBody] ExpedienteDeleteCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.SMedicosUrl}api/smedicos/expedientes/deleteExpediente", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ExpedienteDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
