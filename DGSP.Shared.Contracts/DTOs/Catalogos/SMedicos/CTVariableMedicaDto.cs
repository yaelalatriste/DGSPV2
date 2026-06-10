namespace DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos
{
    public class CTVariableMedicaDto
    {
        public int Id { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Singular { get; set; } = string.Empty;
        public string Plural { get; set; } = string.Empty;
    }
}
