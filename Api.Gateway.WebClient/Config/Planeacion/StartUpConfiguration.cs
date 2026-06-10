using Api.Gateway.Proxies.Planeacion.Commands.ActividadesMensuales;
using Api.Gateway.Proxies.Planeacion.Commands.Entregables;
using Api.Gateway.Proxies.Planeacion.Commands.Expedientes;
using Api.Gateway.Proxies.Planeacion.Queries.ActividadesMensuales;
using Api.Gateway.Proxies.Planeacion.Queries.Almacen;
using Api.Gateway.Proxies.Planeacion.Queries.Entregables;
using Api.Gateway.Proxies.Planeacion.Queries.Expedientes;
using Api.Gateway.Proxies.Planeacion.Queries.Remisiones;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.WebClient.Config.Planeacion
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesPlaneacionQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<IQAMensualProxy, QAMensualProxy>();
            service.AddHttpClient<IQAlmacenProxy, QAlmacenProxy>();
            service.AddHttpClient<IQRemisionProxy, QRemisionProxy>();
            service.AddHttpClient<IQEntregablesAMProxy, QEntregablesAMProxy>();
            service.AddHttpClient<IQExpedientePlaneacionProxy, QExpedientePlaneacionProxy>();

            return service;
        }
        
        public static IServiceCollection AddProxiesPlaneacionCommands(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<ICActividadMensualProxy, CActividadMensualProxy>();
            service.AddHttpClient<ICEntregablesAMProxy, CEntregablesAMProxy>();
            service.AddHttpClient<ICExpedientePlaneacionProxy, CExpedientePlaneacionProxy>();

            return service;
        }
    }
}
