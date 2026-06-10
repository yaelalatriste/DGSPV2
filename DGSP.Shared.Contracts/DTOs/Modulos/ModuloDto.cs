namespace DGSP.Shared.Contracts.DTOs.Modulos
{
    public class ModuloDto
    {
        public int Id { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
        public string Icono { get; set; } = string.Empty;
        public List<SubmoduloDto> Submodulos { get; set; } = new List<SubmoduloDto>();
    }
}
