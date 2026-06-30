using DGSP.Gateway.Proxy.Commands.Seguros.Continuidades.ContactosContinuidades;
using DGSP.Gateway.Proxy.Commands.Seguros.Continuidades.Entregables;
using DGSP.Gateway.Proxy.Queries.DGRH.Empleados;
using DGSP.Gateway.Proxy.Queries.Seguros.CJFBDRHDF.Calculadora;
using DGSP.Gateway.Proxy.Queries.Seguros.CJFBDRHDF.Movimiento;
using DGSP.Gateway.Proxy.Queries.Seguros.DGSP.Continuidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.WebClient.Config.Seguros
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesSegurosQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpContextAccessor();
            service.AddHttpClient<IQContinuidadProxy, QContinuidadProxy>();
            service.AddHttpClient<IQEmpleadoProxy, QEmpleadoProxy>();
            service.AddHttpClient<IQContactoContinuidadProxy, QContactoContinuidadProxy>();
            service.AddHttpClient<IQOficioContinuidadProxy, QOficioContinuidadProxy>();
            service.AddHttpClient<IQEntregableContinuidadProxy, QEntregableContinuidadProxy>();
            service.AddHttpClient<IQMovimientoSpProxy, QMovimientoSpProxy>();
            service.AddHttpClient<IQCorreoContinuidadProxy, QCorreoContinuidadProxy>();
            service.AddHttpClient<IQCalcularPolizaSgmmProxy, QCalcularPolizaSgmmProxy>();

            return service;
        }

        public static IServiceCollection AddProxiesSegurosCommands(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpContextAccessor();
            service.AddHttpClient<ICContinuidadProxy, CContinuidadProxy>();
            service.AddHttpClient<ICEntregableContinuidadProxy, CEntregableContinuidadProxy>();
            service.AddHttpClient<ICContactoContinuidadProxy, CContactoContinuidadProxy>();

            return service;
        }

    }
}
