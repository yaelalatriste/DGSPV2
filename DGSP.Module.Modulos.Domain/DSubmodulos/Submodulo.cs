namespace DGSP.Module.Modulos.Domain.DSubmodulos
{
    public class Submodulo
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }
        public int ModuloId { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
        public string Icono { get; set; } = string.Empty;
        public int Orden { get; set; }
    }
}
