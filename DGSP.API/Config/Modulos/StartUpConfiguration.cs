using DGSP.Module.Modulos.Application.Queries;

namespace DGSP.API.Config.Modulos
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddModulosQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<ISubmodulosQueryService, SubmodulosQueryService>();
            service.AddTransient<IModulosQueryService, ModulosQueryService>();
            service.AddTransient<IOpcionesQueryService, OpcionesQueryService>();
            
            return service;
        }
    }
}
