using Api.Gateway.Proxies.DGRH.Queries.Empleado;
using Api.Gateway.Proxies.Estatus.Queries;
using Api.Gateway.Proxies.Estatus.Queries.Acuerdos;
using Api.Gateway.Proxies.Estatus.Queries.FlujoJustificantes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.WebClient.Config.DGRH
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesDGRHQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<IQEmpleadoProxy, QEmpleadoProxy>();
            
            return service;
        }
    }
}
