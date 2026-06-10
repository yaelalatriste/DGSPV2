using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Entradas;
using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Logs;
using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Movimientos;
using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.NotasTraspaso;
using DGSP.Module.SMedicos.Application.Interfaces.Medicamentos.Salidas;
using DGSP.Module.SMedicos.Application.Interfaces.Reportes;
using DGSP.Module.SMedicos.Application.Interfaces.Siacom.Consultorios;
using DGSP.Module.SMedicos.Application.Interfaces.Siacom.TipoConsulta;
using DGSP.Module.SMedicos.Application.Interfaces.Siacom.TipoConsultaDetalle;
using DGSP.Module.SMedicos.Application.Interfaces.Siacom.TipoServicios;
using DGSP.Module.SMedicos.Application.Queries.Reportes;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.Entradas;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.Logs;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.Movimientos;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.NotasTraspaso;
using DGSP.Module.SMedicos.Application.Services.Medicamentos.Salidas;
using DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.Entradas;
using DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.Logs;
using DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.Movimientos;
using DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.NotasTraspaso;
using DGSP.Module.SMedicos.Persistence.Repositories.Medicamentos.Salidas;
using DGSP.Module.SMedicos.Persistence.Services.Medicamentos.Entradas;
using DGSP.Module.SMedicos.Persistence.Services.Medicamentos.Logs;
using DGSP.Module.SMedicos.Persistence.Services.Medicamentos.Movimientos;
using DGSP.Module.SMedicos.Persistence.Services.Medicamentos.NotasTraspaso;
using DGSP.Module.SMedicos.Persistence.Services.Medicamentos.Salidas;
using SMedicos.Services.Queries.Queries.Reportes;
using SMedicos.Services.Queries.Queries.Siacom.Consultorios;
using SMedicos.Services.Queries.Queries.Siacom.TipoConsultaDetalle;
using SMedicos.Services.Queries.Queries.Siacom.TiposConsulta;
using SMedicos.Services.Queries.Queries.Siacom.TiposServicios;

namespace DGSP.API.Config.SMedicos
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddSMedicosQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<ICTConsultorioQueryService, CTConsultorioQueryService>();
            service.AddScoped<ITipoConsultaQueryService, TipoConsultaQueryService>();
            service.AddScoped<ITCDetalleQueryService, TCDetalleQueryService>();
            service.AddScoped<ITipoServicioQueryService, TipoServicioQueryService>();
            service.AddScoped<IRServiciosMedicosQueryService, RServiciosMedicosQueryService>();
            service.AddScoped<IDashboardConsultasService, DashboardConsultasService>();
            service.AddScoped<INotaTraspasoQueryService, NotaTraspasoQueryService>();
            service.AddScoped<IInventarioAppService, InventarioService>();
            service.AddScoped<ISalidaMedicamentoService, SalidaMedicamentoService>();
            service.AddScoped<INotaTraspasoQueryService, NotaTraspasoQueryService>();
            service.AddScoped<ISalidaMedicamentoService, SalidaMedicamentoService>();
            service.AddScoped<ISalidaMedicamentoDetalleService, SalidaMedicamentoDetalleService>();
            service.AddScoped<IDetalleNotaTraspasoQueryService, DetalleNotaTraspasoQueryService>();
            service.AddScoped<ILogNotaTraspasoQueryService, LogNotaTraspasoQueryService>();
            service.AddScoped<IMovimientoInventarioService, MovimientoInventarioService>();

            service.AddScoped<ILoteMedicamentoRepository, LoteMedicamentoRepository>();
            service.AddScoped<IMovimientoInventarioRepository, MovimientoInventarioRepository>();
            service.AddScoped<ISalidaMedicamentoRepository, SalidaMedicamentoRepository>();
            service.AddScoped<ISalidaMedicamentoDetalleRepository, SalidaMedicamentoDetalleRepository>();
            service.AddScoped<INotaTraspasoRepository, NotaTraspasoRepository>();
            service.AddScoped<IDetalleNotaTraspasoRepository, DetalleNotaTraspasoRepository>();
            service.AddScoped<ILogNotaTraspasoRepository, LogNotaTraspasoRepository>();
            service.AddScoped<IMovimientoInventarioRepository, MovimientoInventarioRepository>();

            return service;
        }
    }
}
