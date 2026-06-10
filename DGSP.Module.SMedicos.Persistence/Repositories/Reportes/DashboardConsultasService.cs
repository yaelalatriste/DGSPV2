using DGSP.Module.SMedicos.Application.Interfaces.Reportes;
using DGSP.Module.SMedicos.Persistence;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Dashboards;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Reportes;
using Microsoft.EntityFrameworkCore;
using SMedicos.Service.Queries.Mapping;

namespace DGSP.Module.SMedicos.Application.Queries.Reportes
{
    public class DashboardConsultasService : IDashboardConsultasService
    {
        private readonly SiacomDbContext _context;

        public DashboardConsultasService(SiacomDbContext context)
        {
            _context = context;
        }

        //Reporte General de Consultas Medicas
        public async Task<List<ResumenTipoConsultaDto>> ConsultasMedicasAsync(FiltrosSmDto filtros)
        {
            var query = _context.ConsultasMedicas
                .AsNoTracking()
                // Filtro año y médico distinto de 0
                .Where(cm => cm.FdFchConsulta.Year == filtros.Anios && cm.FiExpMedico != 0 && filtros.Meses.Contains(cm.FdFchConsulta.Month))
                //LEFT JOIN a CTTipoConsulta (si no tienes navegación, te dejo más abajo la versión con join)
                .Join(
                    _context.CTTipoConsultas,
                    cm => cm.FiIdTipoConsulta,
                    tc => tc.FiIdTipoConsulta,
                    (cm, tc) => new
                    {
                        cm.FdFchConsulta,
                        cm.FiIdTipoConsulta,
                        TipoConsulta = tc.FcTipoConsulta
                    }
                )
                // Agrupación por tipo, año y mes
                .GroupBy(x => new
                {
                    x.FiIdTipoConsulta,
                    x.TipoConsulta,
                    Anio = x.FdFchConsulta.Year,
                    Mes = x.FdFchConsulta.Month
                })
                // Proyección al DTO
                .Select(g => new ResumenTipoConsultaDto
                {
                    IdTipoConsulta = g.Key.FiIdTipoConsulta,
                    TipoConsulta = g.Key.TipoConsulta,
                    Anio = g.Key.Anio,
                    Mes = g.Key.Mes,
                    Cantidad = g.Count()
                })
                // ORDER BY
                .OrderBy(r => r.IdTipoConsulta)
                .ThenBy(r => r.TipoConsulta)
                .ThenBy(r => r.Anio)
                .ThenBy(r => r.Mes);

            return await query.ToListAsync();
        }

        public async Task<DashboardConsultasResponseDto> ObtenerDashboardAsync(FiltrosSmDto request)
        {
            var consultasMedicas = await ConsultasMedicasAsync(request);

            var data = consultasMedicas.ToList();

            var totalConsultas = data.Sum(x => x.Cantidad);

            var tipoMasFrecuente = data
                .GroupBy(x => x.TipoConsulta)
                .Select(g => new
                {
                    TipoConsulta = g.Key,
                    Total = g.Sum(x => x.Cantidad)
                })
                .OrderByDescending(x => x.Total)
                .FirstOrDefault();

            var mesMayorDemanda = data
                .GroupBy(x => new { x.Anio, x.Mes })
                .Select(g => new
                {
                    g.Key.Anio,
                    g.Key.Mes,
                    Total = g.Sum(x => x.Cantidad)
                })
                .OrderByDescending(x => x.Total)
                .FirstOrDefault();

            var tendencia = data
                .GroupBy(x => new { x.Anio, x.Mes })
                .Select(g => new TendenciaMensualDto
                {
                    Anio = g.Key.Anio,
                    Mes = g.Key.Mes,
                    Periodo = $"{g.Key.Anio}-{g.Key.Mes:D2}",
                    TotalConsultas = g.Sum(x => x.Cantidad)
                })
                .OrderBy(x => x.Anio)
                .ThenBy(x => x.Mes)
                .ToList();

            decimal variacion = 0;
            if (tendencia.Count >= 2)
            {
                var mesAnterior = tendencia[^2].TotalConsultas;
                var mesActual = tendencia[^1].TotalConsultas;

                variacion = mesAnterior == 0
                    ? 0
                    : ((mesActual - mesAnterior) / (decimal)mesAnterior) * 100;
            }

            var comparativa = data
                .GroupBy(x => new { x.TipoConsulta, x.Anio, x.Mes })
                .Select(g => new ComparativaTipoConsultaDto
                {
                    TipoConsulta = g.Key.TipoConsulta,
                    Anio = g.Key.Anio,
                    Mes = g.Key.Mes,
                    Cantidad = g.Sum(x => x.Cantidad)
                })
                .OrderBy(x => x.TipoConsulta)
                .ThenBy(x => x.Anio)
                .ThenBy(x => x.Mes)
                .ToList();

            var distribucion = data
                .GroupBy(x => x.TipoConsulta)
                .Select(g => new DistribucionTipoConsultaDto
                {
                    TipoConsulta = g.Key,
                    Cantidad = g.Sum(x => x.Cantidad),
                    Porcentaje = totalConsultas == 0
                        ? 0
                        : Math.Round((g.Sum(x => x.Cantidad) * 100m) / totalConsultas, 2)
                })
                .OrderByDescending(x => x.Cantidad)
                .ToList();

            var detalle = data
                .OrderBy(x => x.Anio)
                .ThenBy(x => x.Mes)
                .ThenBy(x => x.TipoConsulta)
                .Select(x => new DetalleConsultaDto
                {
                    TipoConsulta = x.TipoConsulta,
                    Anio = x.Anio,
                    Mes = x.Mes,
                    Cantidad = x.Cantidad,
                    Porcentaje = totalConsultas == 0
                        ? 0
                        : Math.Round((x.Cantidad * 100m) / totalConsultas, 2)
                })
                .ToList();
            var response = new DashboardConsultasResponseDto
            {
                Kpis = new DashboardKpiDto
                {
                    TotalConsultas = totalConsultas,
                    TipoConsultaMasFrecuente = tipoMasFrecuente?.TipoConsulta ?? string.Empty,
                    MesMayorDemanda = mesMayorDemanda is null
                        ? string.Empty
                        : $"{ObtenerNombreMes(mesMayorDemanda.Mes)} {mesMayorDemanda.Anio}",
                    VariacionPorcentual = Math.Round(variacion, 2)
                },
                ComparativaTipos = comparativa,
                Distribucion = distribucion,
                TendenciaMensual = tendencia,
                Detalle = detalle
            };

            return response.MapTo<DashboardConsultasResponseDto>();
        }

        private static string ObtenerNombreMes(int mes)
        {
            return mes switch
            {
                1 => "Enero",
                2 => "Febrero",
                3 => "Marzo",
                4 => "Abril",
                5 => "Mayo",
                6 => "Junio",
                7 => "Julio",
                8 => "Agosto",
                9 => "Septiembre",
                10 => "Octubre",
                11 => "Noviembre",
                12 => "Diciembre",
                _ => string.Empty
            };
        }
    }
}
