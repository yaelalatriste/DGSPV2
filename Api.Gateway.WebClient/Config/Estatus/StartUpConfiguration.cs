using Api.Gateway.Proxies.Estatus.Queries;
using Api.Gateway.Proxies.Estatus.Queries.Acuerdos;
using Api.Gateway.Proxies.Estatus.Queries.Continuidades;
using Api.Gateway.Proxies.Estatus.Queries.FlujoJustificantes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.WebClient.Config.Estatus
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesEstatusQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<IQCTEstatusCendiProxy, QCTEstatusCendiProxy>();
            service.AddHttpClient<IQFlujoJustificantesProxy, QFlujoJustificantesProxy>();
            service.AddHttpClient<IQEContinuidadesProxy, QEContinuidadesProxy>();
            service.AddHttpClient<IQEAcuerdoProxy, QEAcuerdoProxy>();
            
            return service;
        }
    }
}
