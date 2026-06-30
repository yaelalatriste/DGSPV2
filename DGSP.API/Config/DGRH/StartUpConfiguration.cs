using DGSP.Module.DGRH.Application.Services.RH;
using DGSP.Module.DGRH.Persistence.Repositories.RH;
using DGSP.Module.DGRH.Persistence.Services.RH.Empleados;

namespace DGSP.API.Config.DGRH
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddDGRHQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IEmpleadoRepository, EmpleadoRepository>();            
            service.AddTransient<IEmpleadoService, EmpleadoService>();            

            return service;
        }
    }
}
