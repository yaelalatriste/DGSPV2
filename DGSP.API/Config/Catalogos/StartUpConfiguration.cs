using DGSP.Module.Catalogos.Application.Interfaces.Generales;
using DGSP.Module.Catalogos.Application.Interfaces.SMedicos;
using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Module.Catalogos.Persistence.Repositories.Generales;
using DGSP.Module.Catalogos.Persistence.Repositories.SMedicos;
using DGSP.Module.Catalogos.Persistence.Services.Generales;
using DGSP.Module.Catalogos.Persistence.Services.SMedicos;

namespace DGSP.API.Config.Catalogos
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddCatalogosQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<ICTMesService, CTMesService>();            
            service.AddTransient<ICTAreaService, CTAreaService>();            
            service.AddTransient<ICTConsultorioService, CTConsultorioService>();            
            service.AddTransient<ICTMedicamentoService, CTMedicamentoService>();            
            service.AddTransient<ICTTipoInsumoService, CTTipoInsumoService>();            
            service.AddTransient<ICTEntregableService, CTEntregableService>();            
            service.AddTransient<ICTTipoMovimientoService, CTTipoMovimientoService>();            
            service.AddTransient<ICTUnidadService, CTUnidadService>();            
            service.AddTransient<ICTVariableMedicaService, CTVariableMedicaService>();
            service.AddTransient<ICTVariableGeneralService, CTVariableGeneralService>();

            service.AddTransient<ICTVariableGeneralRepository, CTVariableGeneralRepository>();
            service.AddTransient<ICTMesRepository, CTMesRepository>();
            service.AddTransient<ICTAreaRepository, CTAreaRepository>();
            service.AddTransient<ICTConsultorioRepository, CTConsultorioRepository>();
            service.AddTransient<ICTEntregableRepository, CTEntregableRepository>();
            service.AddTransient<ICTMedicamentoRepository, CTMedicamentoRepository>();
            service.AddTransient<ICTTipoInsumoRepository, CTTipoInsumoRepository>();
            service.AddTransient<ICTTipoMovimientoRepository, CTTipoMovimientoRepository>();
            service.AddTransient<ICTUnidadRepository, CTUnidadRepository>();
            service.AddTransient<ICTVariableMedicaRepository, CTVariableMedicaRepository>();

            return service;
        }
    }
}
