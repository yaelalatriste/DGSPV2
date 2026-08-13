using DGSP.Module.DGRH.Application.Interfaces.Seguros.Movimientos;
using DGSP.Module.DGRH.Application.Services.Seguros.Movimientos;
using DGSP.Module.DGRH.Persistence.Repositories.Seguros.MovimientosSP;
using DGSP.Module.DGRH.Persistence.Services.RH.Empleados;
using DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.Calculadora;
using DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.SGMM;
using DGSP.Module.Seguros.Application.Interfaces.DGSP.Movimientos.Calculadora;
using DGSP.Module.Seguros.Application.Interfaces.DGSP.Siniestros.Continuidades;
using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.Calculadora;
using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.SGMM;
using DGSP.Module.Seguros.Application.Services.DGSP.Movimientos.Calculadora;
using DGSP.Module.Seguros.Application.Services.DGSP.Siniestros.Continuidades;
using DGSP.Module.Seguros.Persistence.Repositories.CJFBDRHDF.Calculadora;
using DGSP.Module.Seguros.Persistence.Repositories.CJFBDRHDF.SGMM;
using DGSP.Module.Seguros.Persistence.Repositories.DGSP.Movimientos.Calculadora;
using DGSP.Module.Seguros.Persistence.Repositories.DGSP.Siniestros.Continuidades;
using DGSP.Module.Seguros.Persistence.Services.CJFBDRHDF.Calculadora;
using DGSP.Module.Seguros.Persistence.Services.CJFBDRHDF.Catalogos;
using DGSP.Module.Seguros.Persistence.Services.DGSP.Movimientos.Calculadora;
using DGSP.Module.Seguros.Persistence.Services.DGSP.Siniestros.Continuidades;
using SISSGMM.Infrastructure.Repositories;

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
            service.AddTransient<ICatalogosSgmmService, CatalogosSgmmService>();
            service.AddTransient<ICalcularPolizaSgmmService, CalcularPolizaSgmmService>();
            service.AddTransient<ICalendarioNominaService, CalendarioNominaService>();
            
            service.AddTransient<ICalcularPolizaSGMMRepository, CalcularPolizaSGMMRepository>();
            service.AddTransient<IServidorPublicoOpMMSRepository, ServidorPublicoOpMMSRepository>();
            service.AddTransient<ICatalogosSgmmRepository, CatalogosSgmmRepository>();
            service.AddTransient<IMovimientoSpRepository, MovimientoSpRepository>();
            service.AddTransient<IMovimientoRepository, MovimientoRepository>();
            service.AddTransient<IContinuidadRepository, ContinuidadRepository>();
            service.AddTransient<ILogContinuidadRepository, LogContinuidadRepository>();
            service.AddTransient<IOficioContinuidadRepository, OficioContinuidadRepository>();
            service.AddTransient<IEntregableContinuidadRepository, EntregableContinuidadRepository>();
            service.AddTransient<IContactoContinuidadRepository, ContactoContinuidadRepository>();
            service.AddTransient<ICalendarioNominaRepository, CalendarioNominaRepository>();

            return service;
        }
    }
}
