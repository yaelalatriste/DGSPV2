using DGSP.Module.Estatus.Application.Interfaces.Continuidades;
using DGSP.Module.Estatus.Application.Interfaces.NotasTraspaso;
using DGSP.Module.Estatus.Application.Services.Continuidades;
using DGSP.Module.Estatus.Application.Services.NotasTraspaso;
using DGSP.Module.Estatus.Persistence.Repositories.Continuidades;
using DGSP.Module.Estatus.Persistence.Repositories.NotasTraspaso;
using DGSP.Module.Estatus.Persistence.Services.Continuidades;
using DGSP.Module.Estatus.Persistence.Services.NotasTraspaso;

namespace DGSP.API.Config.Estatus
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddEstatusQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IEstatusContinuidadesRepository, EstatusContinuidadesRepository>();
            service.AddTransient<IFlujoContinuidadRepository, FlujoContinuidadRepository>();
            service.AddTransient<IEstatusNotasTraspasoRepository, EstatusNotasTraspasoRepository>();
            service.AddTransient<IFlujoNotaTraspasoRepository, FlujoNotaTraspasoRepository>();
            service.AddTransient<IFlujoEntregableContinuidadRepository, FlujoEntregableContinuidadRepository>();
            
            service.AddTransient<IEstatusContinuidadesService, EstatusContinuidadesService>();
            service.AddTransient<IFlujoContinuidadService, FlujoContinuidadService>();
            service.AddTransient<IFlujoEntregableContinuidadService, FlujoEntregableContinuidadService>();

            service.AddTransient<IEstatusNotasTraspasoService, EstatusNotasTraspasoService>();
            service.AddTransient<IFlujoNotaTraspasoService, FlujoNotaTraspasoService>();

            return service;
        }
    }
}
