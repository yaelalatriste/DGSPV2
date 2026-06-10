using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using MediatR;

namespace DGSP.Shared.Contracts.Commands.Seguros.Continuidades.OficiosContinuidades
{
    public class RegistrarOficioContinuidadCommand : IRequest<OficioContinuidadDto>
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
    }
}
