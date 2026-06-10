using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.Usuarios;

namespace DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas
{
    public class SalidaMedicamentoDetalleDto
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int SalidaId { get; set; }
        public int ConsultorioDestinoId{ get; set; }
        public int LoteId { get; set; }
        public int TipoInsumoId { get; set; }
        public int TipoMovimientoId { get; set; }
        public int FormaFarmaceuticaId { get; set; }
        public int TipoEnvaseId { get; set; }
        public int Cantidad { get; set; }
        public int CantidadEnvase { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }


        public UsuarioDto Usuario { get; set; } = new UsuarioDto();
        public SalidaMedicamentoDto Salida { get; set; } = new SalidaMedicamentoDto();
        public LoteDto Lote { get; set; } = new LoteDto();
        public CTConsultorioDto Consultorio { get; set; } = new CTConsultorioDto();
        public CTTipoInsumoDto TipoInsumo { get; set; } = new CTTipoInsumoDto();
        public CTTipoMovimientoDto TipoMovimiento { get; set; } = new CTTipoMovimientoDto();
        public CTVariableMedicaDto FormaFarmaceutica { get; set; } = new CTVariableMedicaDto();
        public CTVariableMedicaDto TipoEnvase { get; set; } = new CTVariableMedicaDto();
    }
}
