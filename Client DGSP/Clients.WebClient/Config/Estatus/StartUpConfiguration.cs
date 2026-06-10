using DGSP.Gateway.Proxy.Queries.Estatus.Continuidades;
using DGSP.Gateway.Proxy.Queries.Estatus.NotasTraspaso;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.WebClient.Config.Estatus
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesEstatusQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpContextAccessor();
            service.AddHttpClient<IQEstatusContinuidadProxy, QEstatusContinuidadProxy>();
            service.AddHttpClient<IQCTENotaTraspasoProxy, QCTENotaTraspasoProxy>();
            service.AddHttpClient<IQFlujoContinuidadProxy, QFlujoContinuidadProxy>();
            service.AddHttpClient<IQFlujoEntregableContinuidadProxy, QFlujoEntregableContinuidadProxy>();
            service.AddHttpClient<IQFlujoNotasProxy, QFlujoNotasProxy>();

            return service;
        }
    }
}
