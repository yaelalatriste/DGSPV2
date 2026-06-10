namespace DGSP.Module.Catalogos.Domain.SMedicos
{
    public class CTMedicamento
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
    }
}
