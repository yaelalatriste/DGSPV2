using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Logs;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.NotasTraspaso
{
    public interface ICNotasTraspasoProxy
    {
        Task<NotaTraspasoDto> AddNotaTraspasoAsync(RegistrarNotaTraspasoCommand command);
        Task<NotaTraspasoDto> CreateLogNotaTraspaso(RegistrarLogNotaTraspasoCommand command);
        Task<NotaTraspasoDto> ActualizarNotaTraspasoAsync(ActualizarNotaTraspasoCommand command);
        Task<NotaTraspasoDto> ConcluirNotaTraspasoAsync(ConcluirNotaTraspasoCommand command);
    }

    public class CNotasTraspasoProxy : ICNotasTraspasoProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public CNotasTraspasoProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<NotaTraspasoDto> AddNotaTraspasoAsync([FromBody] RegistrarNotaTraspasoCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiGatewayUrl}smedicos/notasTraspaso/createNota", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<NotaTraspasoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<NotaTraspasoDto> CreateLogNotaTraspaso([FromBody] RegistrarLogNotaTraspasoCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PostAsync($"{_apiGatewayUrl}smedicos/logsNotasTraspaso/createLog", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<NotaTraspasoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<NotaTraspasoDto> ActualizarNotaTraspasoAsync([FromBody] ActualizarNotaTraspasoCommand command)
        {
            var content = new StringContent(
                 JsonSerializer.Serialize(command),
                 Encoding.UTF8,
                 "application/json"
             );

            var request = await _httpClient.PutAsync($"{_apiGatewayUrl}smedicos/notasTraspaso/updateNota", content);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<NotaTraspasoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
        
        public async Task<NotaTraspasoDto> ConcluirNotaTraspasoAsync([FromForm] ConcluirNotaTraspasoCommand command)
        {
            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(command.Id.ToString()), "Id");
            formContent.Add(new StringContent(command.UsuarioId.ToString()), "UsuarioId");
            formContent.Add(new StringContent(command.EstatusId.ToString()), "EstatusId");
            if (command.Entregable != null)
            {
                var fileStreamContentPDF = new StreamContent(command.Entregable.OpenReadStream());
                fileStreamContentPDF.Headers.ContentType = MediaTypeHeaderValue.Parse(command.Entregable.ContentType);
                formContent.Add(fileStreamContentPDF, name: "Entregable", command.Entregable.FileName);
            }

            var request = await _httpClient.PutAsync($"{_apiGatewayUrl}smedicos/notasTraspaso/concluirNota", formContent);
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<NotaTraspasoDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
