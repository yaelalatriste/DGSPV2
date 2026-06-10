using DGSP.Shared.Contracts.DTOs.Usuarios;

namespace DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos
{
    public class CTMedicamentoDto
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int TipoInsumoId { get; set; }
        public string Formula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Presentacion { get; set; } = string.Empty;
        public int TipoEnvaseId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Nullable<DateTime> FechaActualizacion { get; set; }

        public UsuarioDto Usuario { get; set; } = new UsuarioDto();
        public CTTipoInsumoDto TipoInsumo { get; set; } = new CTTipoInsumoDto();
        public CTVariableMedicaDto TipoEnvase { get; set; } = new CTVariableMedicaDto();
    }
}
