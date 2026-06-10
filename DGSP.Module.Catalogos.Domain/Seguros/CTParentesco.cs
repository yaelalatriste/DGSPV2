namespace DGSP.Module.Catalogos.Domain.Seguros
{
    public class CTParentesco
    {
        public int Id { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
