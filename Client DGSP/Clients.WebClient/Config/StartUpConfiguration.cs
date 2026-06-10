using DGSP.Gateway.Proxy.Queries.ExternalServices;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.WebClient.Config
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddAppsettingBinding(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpContextAccessor();
            return service;
        }

        public static IServiceCollection AddProxiesServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpContextAccessor();
            service.AddHttpClient<IEmailProxy, EmailProxy>();
            service.AddHttpClient<IUsuarioProxy, UsuarioProxy>();
            service.AddHttpClient<IPermisoProxy, PermisoProxy>();
            service.AddHttpClient<IModuloProxy, ModuloProxy>();

            return service;
        }
    }
}
