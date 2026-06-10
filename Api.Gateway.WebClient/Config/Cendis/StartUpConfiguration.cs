using Api.Gateway.Proxies.Cendis.Commands.DJustificantes;
using Api.Gateway.Proxies.Cendis.Commands.Expedientes;
using Api.Gateway.Proxies.Cendis.Commands.Justificantes;
using Api.Gateway.Proxies.Cendis.Commands.JUsuarios;
using Api.Gateway.Proxies.Cendis.Commands.RegistroVisitantes;
using Api.Gateway.Proxies.Cendis.Queries.CendisUsuarios;
using Api.Gateway.Proxies.Cendis.Queries.Expedientes;
using Api.Gateway.Proxies.Cendis.Queries.Justificantes;
using Api.Gateway.Proxies.Cendis.Queries.JUsuarios;
using Api.Gateway.Proxies.Cendis.Queries.Logs;
using Api.Gateway.Proxies.Cendis.Queries.RegistroVisitantes;
using Api.Gateway.Proxies.Seguros.Commands.Expedientes;
using Api.Gateway.Proxies.Seguros.Queries.Expedientes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.WebClient.Config.Cendis
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesCendisQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<IQCendisUsuariosProxy, QCendisUsuariosProxy>();
            service.AddHttpClient<IQJUsuariosProxy, QJUsuariosProxy>();
            service.AddHttpClient<IQLJustificanteProxy, QLJustificanteProxy>();
            service.AddHttpClient<IQExpedienteCendiProxy, QExpedienteCendiProxy>();
            service.AddHttpClient<IQRVisitantesCendisProxy, QRVisitantesCendisProxy>();

            return service;
        }

        public static IServiceCollection AddProxiesCendisCommands(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<ICJustificanteProxy, CJustificanteProxy>();
            service.AddHttpClient<ICDJustificanteProxy, CDJustificanteProxy>();
            service.AddHttpClient<ICJUsuarioProxy, CJUsuarioProxy>();
            service.AddHttpClient<ICExpedienteCendiProxy, CExpedienteCendiProxy>();
            service.AddHttpClient<ICRVisitantesCendiProxy, CRVisitantesCendiProxy>();

            return service;
        }
    }
}
