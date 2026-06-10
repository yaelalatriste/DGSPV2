namespace DGSP.Module.Catalogos.Domain.SMedicos
{
    public class CTVariableMedica
    {
        public int Id { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Singular { get; set; } = string.Empty;
        public string Plural { get; set; } = string.Empty;
    }
}
