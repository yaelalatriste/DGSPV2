using Api.Gateway.Models.Catalogos.DTOs.CTAreas;
using Api.Gateway.Models.Cendis.DTOs.Justificantes;
using Api.Gateway.Models.Cendis.DTOs.RegistroVisitantes;
using Api.Gateway.Proxies.Config;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies.Cendis.Queries.Justificantes
{
    public interface IQJustificanteCendisProxy
    {
        Task<List<JustificanteDto>> GetAllJustificantes();
        Task<List<JustificanteDto>> GetJustificantesByCendi(int centi);
        Task<JustificanteDto> GetJustificanteById(int id);
        Task<List<DetalleJustificanteDto>> GetDetalleJustificante(int id);
    }

    public class QJustificanteCendisProxy : IQJustificanteCendisProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public QJustificanteCendisProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public Task<List<JustificanteDto>> GetAllJustificantes()
        {
            throw new NotImplementedException();
        }

        public Task<List<DetalleJustificanteDto>> GetDetalleJustificante(int id)
        {
            throw new NotImplementedException();
        }

        public Task<JustificanteDto> GetJustificanteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<JustificanteDto>> GetJustificantesByCendi(int centi)
        {
            throw new NotImplementedException();
        }
    }
}
