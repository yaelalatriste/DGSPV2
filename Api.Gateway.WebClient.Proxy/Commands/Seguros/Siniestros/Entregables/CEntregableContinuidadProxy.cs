using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.Seguros.Siniestros.Continuidades.Entregables;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Commands.Seguros.Siniestros.Entregables
{
    public interface ICEntregableContinuidadProxy
    {
        Task<EntregableContinuidadDto> RegistrarEntregableContinuidadAsync(RegistrarEntregableContinuidadCommand command);
        Task<EntregableContinuidadDto> ActualizarEntregableContinuidadAsync(ActualizarEntregableContinuidadCommand command);
        Task<EntregableContinuidadDto> EliminarEntregableContinuidadAsync(EliminarEntregableContinuidadCommand command);

    }

    public class CEntregableContinuidadProxy : ICEntregableContinuidadProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public CEntregableContinuidadProxy(
            HttpClient httpClient,
            ApiGatewayUrl apiGatewayUrl,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<EntregableContinuidadDto> RegistrarEntregableContinuidadAsync(RegistrarEntregableContinuidadCommand command) =>
             SendMultipartAsync<EntregableContinuidadDto>(
                 HttpMethod.Post,
                 "seguros/continuidades/entregablesContinuidad/createEntregable",
                 BuildRegistrarFormData(command));

        public Task<EntregableContinuidadDto> ActualizarEntregableContinuidadAsync(ActualizarEntregableContinuidadCommand command) =>
            SendMultipartAsync<EntregableContinuidadDto>(
                HttpMethod.Put,
                "seguros/continuidades/entregablesContinuidad/updateEntregableContinuidad",
                BuildActualizarFormData(command));

        public Task<EntregableContinuidadDto> EliminarEntregableContinuidadAsync(EliminarEntregableContinuidadCommand command) =>
            SendMultipartAsync<EntregableContinuidadDto>(
                HttpMethod.Put,
                "seguros/continuidades/entregablesContinuidad/deleteEntregable",
                BuildEliminarFormData(command));

        private async Task<TResponse> SendMultipartAsync<TResponse>(HttpMethod method, string endpoint, MultipartFormDataContent formContent)
        {
            using var request = new HttpRequestMessage(method, $"{_apiGatewayUrl}{endpoint}")
            {
                Content = formContent
            };

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(json, _jsonOptions)!;
        }


        private static MultipartFormDataContent BuildRegistrarFormData(RegistrarEntregableContinuidadCommand command)
        {
            var formContent = new MultipartFormDataContent();

            AddString(formContent, nameof(command.Id), command.Id.ToString());
            AddString(formContent, nameof(command.UsuarioId), command.UsuarioId);
            AddString(formContent, nameof(command.ContinuidadId), command.ContinuidadId.ToString());
            AddString(formContent, nameof(command.EntregableId), command.EntregableId.ToString());
            AddString(formContent, nameof(command.Expediente), command.Expediente.ToString());
            AddString(formContent, nameof(command.TipoEntregable), command.TipoEntregable);
            AddString(formContent, nameof(command.FechaEnvioSP), command.FechaEnvioSP.ToString("o"));
            AddString(formContent, nameof(command.FechaLimitePago), command.FechaLimitePago.ToString("o"));
            AddString(formContent, nameof(command.Importe), command.Importe.ToString(CultureInfo.InvariantCulture));
            AddString(formContent, nameof(command.Observaciones), command.Observaciones);

            if (command.Entregable != null)
            {
                var fileContent = new StreamContent(command.Entregable.OpenReadStream());

                if (!string.IsNullOrWhiteSpace(command.Entregable.ContentType))
                {
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(command.Entregable.ContentType);
                }

                formContent.Add(fileContent, nameof(command.Entregable), command.Entregable.FileName);
            }

            return formContent;
        }

        private static MultipartFormDataContent BuildActualizarFormData(ActualizarEntregableContinuidadCommand command)
        {
            var formContent = new MultipartFormDataContent();

            AddString(formContent, nameof(command.Id), command.Id.ToString());
            AddString(formContent, nameof(command.UsuarioId), command.UsuarioId);
            AddString(formContent, nameof(command.ContinuidadId), command.ContinuidadId.ToString());
            AddString(formContent, nameof(command.EntregableId), command.EntregableId.ToString());
            AddString(formContent, nameof(command.Expediente), command.Expediente.ToString());
            AddString(formContent, nameof(command.TipoEntregable), command.TipoEntregable);
            AddString(formContent, nameof(command.FechaEnvioSP), command.FechaEnvioSP.ToString("o"));
            AddString(formContent, nameof(command.FechaLimitePago), command.FechaLimitePago.ToString("o"));
            AddString(formContent, nameof(command.Importe), command.Importe.ToString(CultureInfo.InvariantCulture));
            AddString(formContent, nameof(command.Observaciones), command.Observaciones.ToString());

            if (command.Entregable != null)
            {
                var fileContent = new StreamContent(command.Entregable.OpenReadStream());

                if (!string.IsNullOrWhiteSpace(command.Entregable.ContentType))
                {
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(command.Entregable.ContentType);
                }

                formContent.Add(fileContent, nameof(command.Entregable), command.Entregable.FileName);
            }

            return formContent;
        }

        private static MultipartFormDataContent BuildEliminarFormData(EliminarEntregableContinuidadCommand command)
        {
            var formContent = new MultipartFormDataContent();

            AddString(formContent, nameof(command.Id), command.Id.ToString());
            AddString(formContent, nameof(command.UsuarioId), command.UsuarioId);
            AddString(formContent, nameof(command.ContinuidadId), command.ContinuidadId.ToString());
            AddString(formContent, nameof(command.Expediente), command.Expediente.ToString());
            AddString(formContent, nameof(command.TipoEntregable), command.TipoEntregable);
            AddString(formContent, nameof(command.Observaciones), command.Observaciones);

            return formContent;
        }

        private static void AddString(MultipartFormDataContent content, string name, string value)
        {
            content.Add(new StringContent(value ?? string.Empty), name);
        }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}