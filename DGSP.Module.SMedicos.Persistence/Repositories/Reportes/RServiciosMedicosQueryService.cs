using DGSP.Module.SMedicos.Application.Interfaces.Reportes;
using DGSP.Module.SMedicos.Persistence;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Reportes;
using Microsoft.EntityFrameworkCore;

namespace SMedicos.Services.Queries.Queries.Reportes
{
    public class RServiciosMedicosQueryService : IRServiciosMedicosQueryService
    {
        private readonly SiacomDbContext _context;

        public RServiciosMedicosQueryService(SiacomDbContext context)
        {
            _context = context;
        }

        public async Task<List<int>> GetAniosOfConsultas()
        {
            return await _context.ConsultasMedicas
                .AsNoTracking()
                .Select(cm => cm.FdFchConsulta.Year)
                .Distinct()
                .OrderBy(y => y)
                .ToListAsync();
        }

        //Reporte General de Consultas Medicas
        public async Task<List<ResumenTipoConsultaDto>> GetReporteGeneralTiposConsultasAsync(FiltrosSmDto filtros)
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
    }
}
