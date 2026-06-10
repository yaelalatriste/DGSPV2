namespace DGSP.Module.Catalogos.Domain.Cendis
{
    public class CTCendi
    {
        public int Id { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
