namespace DGSP.Shared.Contracts.DTOs.Catalogos.Generales
{
    public class CTEntregableDto
    {
        public int Id { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get;set; }
        public Nullable<DateTime> FechaActualizacion { get;set; }
        public Nullable<DateTime> FechaEliminacion { get;set; }

    }
}
