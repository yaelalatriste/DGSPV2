using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.Usuarios;

namespace DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas
{
    public class LoteDto
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int ConsultorioId { get; set; }
        public int TipoInsumoId { get; set; }
        public int TipoMovimientoId { get; set; }
        public int MedicamentoId { get; set; }
        public int FormaFarmaceuticaId { get; set; }
        public int TipoEnvaseId { get; set; }
        public int UnidadContenidoId { get; set; }
        public string Lote { get; set; } = string.Empty;
        public DateTime FechaCaducidad { get; set; }
        public int Cantidad { get; set; }
        public int CantidadEnvase { get; set; }
        public int CantidadTotal { get; set; }
        public string Concentracion { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public Nullable<DateTime> FechaActualizacion { get; set; }

        public CTTipoInsumoDto TipoInsumo { get; set; } = new CTTipoInsumoDto();
        public CTTipoMovimientoDto TipoMovimiento { get; set; } = new CTTipoMovimientoDto();
        public CTMedicamentoDto Medicamento { get; set; } = new CTMedicamentoDto();
        public CTVariableMedicaDto FormaFarmaceutica { get; set; } = new CTVariableMedicaDto();
        public CTVariableMedicaDto UnidadContenido { get; set; } = new CTVariableMedicaDto();
        public CTVariableMedicaDto TipoEnvase { get; set; } = new CTVariableMedicaDto();
        public CTConsultorioDto Consultorio { get; set; } = new CTConsultorioDto();
        public UsuarioDto Usuario { get; set; } = new UsuarioDto();
    }
}
