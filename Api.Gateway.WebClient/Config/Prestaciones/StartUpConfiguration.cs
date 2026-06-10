using Api.Gateway.Proxies.Prestaciones.Commands.Expedientes;
using Api.Gateway.Proxies.Prestaciones.Queries.Expedientes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.WebClient.Config.Prestaciones
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesPrestacionesQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<IQExpedientePrestacionesProxy, QExpedientePrestacionesProxy>();

            return service;
        }
        
        public static IServiceCollection AddProxiesPrestacionesCommands(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<ICExpedientePrestacionesProxy, CExpedientePrestacionesProxy>();

            return service;
        }
    }
}
