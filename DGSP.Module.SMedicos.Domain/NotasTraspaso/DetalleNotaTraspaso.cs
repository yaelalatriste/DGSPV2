namespace DGSP.Module.SMedicos.Domain.NotasTraspaso
{
    public class DetalleNotaTraspaso
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
    }
}
