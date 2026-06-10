using Api.Gateway.Proxies.Modulos;
using Api.Gateway.Proxies.Permisos;
using Api.Gateway.Proxies.Usuarios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.WebClient.Config.Generales
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesGenerales(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<IPermisoProxy, PermisoProxy>();
            service.AddHttpClient<IModuloProxy, ModuloProxy>();
            service.AddHttpClient<ISubmoduloProxy, SubmoduloProxy>();
            service.AddHttpClient<IUsuarioProxy, UsuarioProxy>();
            service.AddHttpClient<ISAUsuarioProxy, SAUsuarioProxy>();

            return service;
        }
    }
}
