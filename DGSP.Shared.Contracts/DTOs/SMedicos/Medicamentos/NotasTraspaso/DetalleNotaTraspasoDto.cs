using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.Usuarios;

namespace DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso
{
    public class DetalleNotaTraspasoDto
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int NotaId { get; set; }
        public int TipoMovimientoId { get; set; }
        public int LoteId { get; set; }
        public int Almacen { get; set; }
        public int Cantidad { get; set; }
        public int Restante { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Nullable<DateTime> FechaActualizacion { get; set; }
        public Nullable<DateTime> FechaEliminacion { get; set; }

        public UsuarioDto Usuario { get; set; } = new UsuarioDto();
        public NotaTraspasoDto NotaTraspaso { get; set; } = new NotaTraspasoDto();
        public CTTipoMovimientoDto TipoMovimiento { get; set; } = new CTTipoMovimientoDto();
        public LoteDto Lote {  get; set; } = new LoteDto();
    }
}
