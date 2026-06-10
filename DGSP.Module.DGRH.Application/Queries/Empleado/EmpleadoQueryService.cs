using DGRH.Persistence.Database;
using DGSP.Shared.Contracts.DTOs.DGRH.Empleados;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.DGRH.Application.Queries.Empleado
{
    public interface IEmpleadoQueryService
    {
        Task<List<EmpleadoDto>> GetAllEmpleados();
        Task<EmpleadoDto> GetEmpleado(int exp);
        Task<List<EmpleadoDto>> GetMovimientosEmpleado(int exp);
        Task<List<EmpleadoDto>> GetEmpleadosTEAsync();
    }

    public class EmpleadoQueryService : IEmpleadoQueryService
    {
        private readonly DGRHProductionDbContext _context;

        public EmpleadoQueryService(DGRHProductionDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoDto>> GetAllEmpleados()
        {
            return await _context.Empleados.AsNoTracking().Select(cm => new EmpleadoDto
            {
                Expediente = cm.exp,
                Nombre = cm.nombre.Trim(),
                Paterno = cm.paterno.Trim(),
                Materno = cm.paterno != null ? cm.materno.Trim() : "",
                FechaNacimiento = cm.fech_nacimiento,
                FechaAlta = cm.fech_alta
            }).ToListAsync();
        }

        public async Task<EmpleadoDto> GetEmpleado(int exp)
        {
            return await _context.Empleados
                .AsNoTracking()
                .Where(e => e.exp == exp)
                .Select(cm => new EmpleadoDto { 
                    Expediente = cm.exp,
                    Nombre = cm.nombre.Trim(),
                    Paterno = cm.paterno.Trim(),
                    Materno = cm.paterno != null  ? cm.materno.Trim() : "",
                    Curp = cm.curp != null ? cm.curp.Trim() : "",
                    Rfc = cm.rfc != null ? cm.rfc.Trim() : "",
                    FechaNacimiento = cm.fech_nacimiento,
                    FechaAlta = cm.fech_alta
                })
                .FirstAsync();
        }

        public async Task<List<EmpleadoDto>> GetMovimientosEmpleado(int exp)
        {
            try
            {
                var empleado = from e in _context.Empleados.AsNoTracking()
                               join k in _context.Kardex.AsNoTracking()
                                    on e.exp equals k.exp into agK
                               from k in agK.DefaultIfEmpty()

                               join p in _context.Puestos.AsNoTracking()
                                on k.cve_puesto equals p.cve_puesto into agP
                               from p in agP.DefaultIfEmpty()

                               where k.exp.Equals(exp)
                               orderby k.csc_nomb descending
                               select new EmpleadoDto
                               {
                                   Expediente = k.exp,
                                   CscNomb = k.csc_nomb,
                                   FechaInicioNombr = k.fech_ini_nomb,
                                   FechaFinNombr = k.fech_fin_nomb,
                                   CscPuesto = k.csc_puesto,
                                   CvePuesto = k.cve_puesto,
                                   Puesto = p.nom_puesto != null ? p.nom_puesto : "",
                                   Nivel = p.nivel,
                                   Rango = k.ind_rango,
                                   Baja = k.ind_baja,
                                   FechaBaja = k.fech_baja
                               };

                return await empleado.ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                return null;
            }
        }

        public async Task<List<EmpleadoDto>> GetEmpleadosTEAsync()
        {
            var nivelesValidos = new[] { "02","03","04","05","06","07","08","09","10","11" };

            var query =
                from e in _context.Empleados
                join k in _context.Kardex on e.exp equals k.exp
                where k.csc_nomb ==
                      _context.Kardex
                          .Where(k2 => k2.exp == e.exp)
                          .Max(k2 => k2.csc_nomb)
                join p in _context.Puestos
                    on k.cve_puesto equals p.cve_puesto into puestoJoin
                from p in puestoJoin.DefaultIfEmpty()
                where p != null
                      && p.nivel != null
                      && nivelesValidos.Contains(p.nivel.Substring(0, 2))
                orderby e.exp
                select new EmpleadoDto
                {
                    Expediente = k.exp,
                    CscNomb = k.csc_nomb,
                    FechaInicioNombr = k.fech_ini_nomb,
                    FechaFinNombr = k.fech_fin_nomb,
                    CscPuesto = k.csc_puesto,
                    CvePuesto = k.cve_puesto,
                    Puesto = p.nom_puesto != null ? p.nom_puesto : "",
                    Nivel = p.nivel,
                    Rango = k.ind_rango,
                    Baja = k.ind_baja,
                    FechaBaja = k.fech_baja
                };

            // Ejecutar consulta
            var lista = await query.ToListAsync();

            return lista;
        }
    }
}
