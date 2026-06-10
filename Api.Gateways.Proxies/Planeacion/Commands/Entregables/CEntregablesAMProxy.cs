using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Gateway.Models.Planeacion.Commands.ActividadesMensuales;
using System.Net.Http.Headers;
using Api.Gateway.Models.Planeacion.Queries.ActividadesMensuales;
using System.Text.Json;
using System.Text;
using Planeacion.Service.EventHandler.Commands.EntregablesAM;
using Api.Gateway.Models.Planeacion.Queries.Entregables;

namespace Api.Gateway.Proxies.Planeacion.Commands.Entregables
{
    public interface ICEntregablesAMProxy
    {
        Task<EntregableAMDto> CreateActividad([FromForm] EntregableAMCreateCommand actividad);
        Task<EntregableAMDto> UpdateActividad([FromForm] EntregableAMUpdateCommand actividad);
        Task<EntregableAMDto> DeleteActividad([FromBody] EntregableAMDeleteCommand actividad);
    }

    public class CEntregablesAMProxy : ICEntregablesAMProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CEntregablesAMProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<EntregableAMDto> CreateActividad([FromForm] EntregableAMCreateCommand actividad)
        {
            var formContent = new MultipartFormDataContent();
            if (actividad.Archivo != null)
            {
                formContent.Add(new StringContent(actividad.UsuarioId.ToString()), "UsuarioId");
                formContent.Add(new StringContent(actividad.AMensualId.ToString()), "AMensualId");
                formContent.Add(new StringContent(actividad.EntregableId.ToString()), "EntregableId");
                formContent.Add(new StringContent(actividad.Entregable.ToString()), "Entregable");
                formContent.Add(new StringContent(actividad.Proceso.ToString()), "Proceso");
                formContent.Add(new StringContent(actividad.Actividad.ToString()), "Actividad");
                formContent.Add(new StringContent(actividad.Mes.ToString()), "Mes");
                var fileStreamContentPDF = new StreamContent(actividad.Archivo.OpenReadStream());
                fileStreamContentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(actividad.Archivo.ContentType);
                formContent.Add(fileStreamContentPDF, name: "Archivo", actividad.Archivo.FileName);
            }

            var request = await _httpClient.PostAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/entregablesAM/createEntregableAM", formContent);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<EntregableAMDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<EntregableAMDto> UpdateActividad([FromForm] EntregableAMUpdateCommand actividad)
        {
            var formContent = new MultipartFormDataContent();

            formContent.Add(new StringContent(actividad.Id.ToString()), "Id");
            formContent.Add(new StringContent(actividad.UsuarioId.ToString()), "UsuarioId");
            formContent.Add(new StringContent(actividad.AMensualId.ToString()), "AMensualId");
            formContent.Add(new StringContent(actividad.EntregableId.ToString()), "EntregableId");
            formContent.Add(new StringContent(actividad.Entregable.ToString()), "Entregable");
            formContent.Add(new StringContent(actividad.Proceso.ToString()), "Proceso");
            formContent.Add(new StringContent(actividad.Actividad.ToString()), "Actividad");
            formContent.Add(new StringContent(actividad.Mes.ToString()), "Mes");

            if (actividad.Archivo != null)
            {
                var fileStreamContentPDF = new StreamContent(actividad.Archivo.OpenReadStream());
                fileStreamContentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(actividad.Archivo.ContentType);
                formContent.Add(fileStreamContentPDF, name: "Archivo", actividad.Archivo.FileName);
            }

            var request = await _httpClient.PutAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/entregablesAM/updateEntregableAM", formContent);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<EntregableAMDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<EntregableAMDto> DeleteActividad([FromBody] EntregableAMDeleteCommand actividad)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(actividad),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiUrls.PlaneacionUrl}api/planeacion/entregablesAM/deleteEntregableAM", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<EntregableAMDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
