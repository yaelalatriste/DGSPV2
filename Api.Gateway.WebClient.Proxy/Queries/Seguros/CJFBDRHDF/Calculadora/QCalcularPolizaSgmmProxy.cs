using Api.Gateway.WebClient.Proxy.Config;
using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora;
using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Catalogos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DGSP.Gateway.Proxy.Queries.Seguros.CJFBDRHDF.Calculadora
{
    public interface IQCalcularPolizaSgmmProxy
    {
        Task<List<CalculadoraSgmmResponseDto>> CalcularPolizaSgmmAsync(FiltroSGMMDto query);
        Task<CatalogosSgmmDto> ObtenerCatalogosSgmm(ObtenerCatalogosSgmmDto catalogo);
    }

    public class QCalcularPolizaSgmmProxy : IQCalcularPolizaSgmmProxy
    {
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public QCalcularPolizaSgmmProxy(HttpClient httpClient, ApiGatewayUrl apiGatewayUrl, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public Task<List<CalculadoraSgmmResponseDto>> CalcularPolizaSgmmAsync(FiltroSGMMDto query) => SendJsonAsync<FiltroSGMMDto, List<CalculadoraSgmmResponseDto>>(HttpMethod.Post, "seguros/CalculadoraSgmm/calcular", query);
        public Task<CatalogosSgmmDto> ObtenerCatalogosSgmm(ObtenerCatalogosSgmmDto catalogo) => GetAsync<CatalogosSgmmDto>($"{_apiGatewayUrl}seguros/CalculadoraSgmm/getAllCatalogosSgmm");

        private async Task<T> GetAsync<T>(string url)
        {
            using var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            return JsonSerializer.Deserialize<T>(stream, _jsonOptions);
        }

        private async Task<TResponse> SendJsonAsync<TRequest, TResponse>(HttpMethod method, string endpoint, TRequest data)
        {
            using var request = new HttpRequestMessage(method, $"{_apiGatewayUrl}{endpoint}")
            {
                Content = JsonContent.Create(data)
            };

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions);
        }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
