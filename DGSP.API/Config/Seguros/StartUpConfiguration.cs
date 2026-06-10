using DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades;
using DGSP.Module.Seguros.Application.Interfaces.DGSP.Logs;
using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.SGMM;
using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Module.Seguros.Application.Services.DGSP.Logs;
using DGSP.Module.Seguros.Persistence.Repositories.DGSP.Continuidades;
using DGSP.Module.Seguros.Persistence.Repositories.DGSP.Logs;
using DGSP.Module.Seguros.Persistence.Services.CJFBDRHDF.Catalogos;
using DGSP.Module.Seguros.Persistence.Services.CJFBDRHDF.MovimientosSP;
using DGSP.Module.Seguros.Persistence.Services.DGSP.Continuidades;
using DGSP.Module.Seguros.Persistence.Services.DGSP.Logs;

namespace DGSP.API.Config.Seguros
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddSegurosQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IMovimientoSpService, MovimientoSpService>();
            service.AddTransient<IMovimientoService, MovimientoService>();
            service.AddTransient<IContinuidadService, ContinuidadService>();
            service.AddTransient<ILogContinuidadService, LogContinuidadService>();
            service.AddTransient<IOficioContinuidadService, OficioContinuidadService>();
            service.AddTransient<IEntregableContinuidadService, EntregableContinuidadService>();
            service.AddTransient<IContactoContinuidadService, ContactoContinuidadService>();
            
            service.AddTransient<IContinuidadRepository, ContinuidadRepository>();
            service.AddTransient<ILogContinuidadRepository, LogContinuidadRepository>();
            service.AddTransient<IOficioContinuidadRepository, OficioContinuidadRepository>();
            service.AddTransient<IEntregableContinuidadRepository, EntregableContinuidadRepository>();
            service.AddTransient<IContactoContinuidadRepository, ContactoContinuidadRepository>();

            return service;
        }
    }
}
