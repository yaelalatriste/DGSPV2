using Api.Gateway.Proxies.DGRH.Queries.Empleado;
using Api.Gateway.Proxies.SMedicos.Commands.Expedientes;
using Api.Gateway.Proxies.SMedicos.Queries.Expedientes;
using Api.Gateway.Proxies.SMedicos.Queries.Reportes;
using Api.Gateway.Proxies.SMedicos.Queries.Siacom.Consultorios;
using Api.Gateway.Proxies.SMedicos.Queries.Siacom.TiposConsulta;
using Api.Gateway.Proxies.SMedicos.Queries.Siacom.TiposConsultaDetalle;
using Api.Gateway.Proxies.SMedicos.Queries.Siacom.TiposServicio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.WebClient.Config.SMedicos
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesSMedicosQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<IQExpedienteSMedicosProxy, QExpedienteSMedicosProxy>();
            service.AddHttpClient<IQCTConsultorioProxy, QCTConsultorioProxy>();
            service.AddHttpClient<IQCTTipoConsultaProxy, QCTTipoConsultaProxy>();
            service.AddHttpClient<IQCTTipoServicioProxy, QCTTipoServicioProxy>();
            service.AddHttpClient<IQCTTipoConsultaDetalleProxy, QCTTipoConsultaDetalleProxy>();
            service.AddHttpClient<IQReporteConsultaProxy, QReporteConsultaProxy>();

            return service;
        }
        
        public static IServiceCollection AddProxiesSMedicosCommands(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<ICExpedienteSMedicosProxy, CExpedienteSMedicosProxy>();

            return service;
        }
    }
}
