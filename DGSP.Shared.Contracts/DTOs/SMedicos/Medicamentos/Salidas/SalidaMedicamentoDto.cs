using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.Usuarios;

namespace DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas
{
    public class SalidaMedicamentoDto
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public Nullable<int> ConsultaId { get; set; }
        public int ConsultorioId { get; set; }
        public DateTime FechaSalida { get; set; }
        public string? Observaciones { get; set; }
        public List<SalidaMedicamentoDetalleDto> Detalles { get; set; } = new List<SalidaMedicamentoDetalleDto>();
        public UsuarioDto Usuario { get; set; } = new UsuarioDto();
        public CTConsultorioDto Consultorio { get; set; } = new CTConsultorioDto();
    }
}
