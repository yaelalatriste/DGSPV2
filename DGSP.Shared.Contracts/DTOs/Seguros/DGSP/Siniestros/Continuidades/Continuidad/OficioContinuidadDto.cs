using DGSP.Shared.Contracts.DTOs.DGRH.RH.Empleados;
using DGSP.Shared.Contracts.DTOs.DGRH.Seguros;

namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Continuidad
{
    public class OficioContinuidadDto
    {
        public int ContinuidadId { get; set; }
        public int AnioMovimiento { get; set; }
        public int TipoMovimiento { get; set; }
        public int Expediente { get; set; }
        public int RegistroMovimiento { get; set; }
        public int Oficio { get; set; }
        public string ObservacionMovimiento { get; set; } = string.Empty;
        public bool Validado { get; set; }
        public DateTime FechaAplicacionMovimientoSP { get; set; }
        public DateTime FechaAltaMovimiento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public EmpleadoDto ServidorPublico { get; set; } = new EmpleadoDto();
        public EmpleadoDto UsuarioRegistro { get; set; } = new EmpleadoDto();
        public MovimientoDto Movimiento { get; set; } = new MovimientoDto();
    }
}
