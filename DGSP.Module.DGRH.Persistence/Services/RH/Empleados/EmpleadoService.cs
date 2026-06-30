using DGSP.Module.DGRH.Application.Services.RH;
using DGSP.Shared.Contracts.DTOs.DGRH.RH.Empleados;

namespace DGSP.Module.DGRH.Persistence.Services.RH.Empleados
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoService(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        public async Task<List<EmpleadoDto>> GetAllEmpleados()
        {
            var empleados = await _empleadoRepository.GetAllEmpleados();

            return empleados.Select(cm => new EmpleadoDto
            {
                Expediente = cm.exp,
                Nombre = cm.nombre.Trim(),
                Paterno = cm.paterno.Trim(),
                Materno = cm.paterno != null ? cm.materno.Trim() : "",
                FechaNacimiento = cm.fech_nacimiento,
                FechaAlta = cm.fech_alta
            }).ToList();
        }

        public async Task<EmpleadoDto> GetEmpleado(int exp)
        {
            var empleado = await _empleadoRepository.GetEmpleado(exp);

            return new EmpleadoDto { 
                    Expediente = empleado.exp,
                    Nombre = empleado.nombre.Trim(),
                    Paterno = empleado.paterno.Trim(),
                    Materno = empleado.paterno != null  ? empleado.materno.Trim() : "",
                    Curp = empleado.curp != null ? empleado.curp.Trim() : "",
                    Rfc = empleado.rfc != null ? empleado.rfc.Trim() : "",
                    FechaNacimiento = empleado.fech_nacimiento,
                    FechaAlta = empleado.fech_alta
                };
        }

        public async Task<List<EmpleadoDto>> GetMovimientosEmpleado(int exp)
        {
            try
            {
                var movimientos = await _empleadoRepository.GetMovimientosEmpleado(exp);

                return movimientos.Select(k => new EmpleadoDto {
                    Expediente = k.Expediente,
                    CscNomb = k.CscNomb,
                    FechaInicioNombr = k.FechaInicioNombr,
                    FechaFinNombr = k.FechaFinNombr,
                    CscPuesto = k.CscPuesto,
                    CvePuesto = k.CvePuesto,
                    Puesto = k.Puesto != null ? k.Puesto : "",
                    Nivel = k.Nivel,
                    Rango = k.Rango,
                    Baja = k.Baja,
                    FechaBaja = k.FechaBaja
                }).ToList();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                return null;
            }
        }

        public async Task<List<EmpleadoDto>> GetEmpleadosTEAsync()
        {
            // Ejecutar consulta
            var lista = await _empleadoRepository.GetEmpleadosTEAsync();

            return lista.Select(k => new EmpleadoDto
            {
                Expediente = k.Expediente,
                CscNomb = k.CscNomb,
                FechaInicioNombr = k.FechaInicioNombr,
                FechaFinNombr = k.FechaFinNombr,
                CscPuesto = k.CscPuesto,
                CvePuesto = k.CvePuesto,
                Puesto = k.Puesto != null ? k.Puesto : "",
                Nivel = k.Nivel,
                Rango = k.Rango,
                Baja = k.Baja,
                FechaBaja = k.FechaBaja
            }).ToList();
        }
    }
}
