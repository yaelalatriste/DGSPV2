using Permisos.Service.Queries;

namespace DGSP.API.Config.Permisos
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddPermisosQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IPermisosQueryService, PermisosQueryService>();
            
            return service;
        }
    }
}
