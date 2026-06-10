namespace DGSP.Module.SMedicos.Domain
{
    public class TipoInsumo
    {
        public int TipoInsumoId { get; set; }
        public string Nombre { get; set; } = "";
        public bool Estatus { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
