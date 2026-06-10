using Api.Gateway.Proxies.DG.Commands.Acuerdos;
using Api.Gateway.Proxies.DG.Commands.AEntregables;
using Api.Gateway.Proxies.DG.Queries.Acuerdos;
using Api.Gateway.Proxies.DG.Queries.AEntregables;
using Api.Gateway.Proxies.DG.Queries.Logs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.WebClient.Config.DG
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesDGQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<IQAcuerdoProxy, QAcuerdoProxy>();
            service.AddHttpClient<IQAEntregableProxy, QAEntregableProxy>();
            service.AddHttpClient<IQLogAcuerdoProxy, QLogAcuerdoProxy>();
            service.AddHttpClient<IQLogAEntregableProxy, QLogAEntregableProxy>();
            
            return service;
        }

        public static IServiceCollection AddProxiesDGCommands(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<ICAcuerdoProxy, CAcuerdoProxy>();
            service.AddHttpClient<ICAEntregableProxy, CAEntregableProxy>();

            return service;
        }
    }
}
