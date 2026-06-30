namespace DGSP.Shared.Contracts.DTOs.Catalogos.Generales
{
    public class CTVariableGeneralDto
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
