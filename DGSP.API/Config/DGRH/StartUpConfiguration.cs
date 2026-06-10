using DGSP.Module.Catalogos.Application.Interfaces.Generales;
using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Module.Catalogos.Persistence.Repositories.Generales;
using DGSP.Module.Catalogos.Persistence.Repositories.SMedicos;
using DGSP.Module.Catalogos.Persistence.Services.Generales;
using DGSP.Module.Catalogos.Persistence.Services.SMedicos;
using DGSP.Module.DGRH.Application.Queries.Empleado;

namespace DGSP.API.Config.DGRH
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddDGRHQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IEmpleadoQueryService, EmpleadoQueryService>();            

            return service;
        }
    }
}
