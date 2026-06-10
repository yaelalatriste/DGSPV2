namespace DGSP.Shared.Contracts.DTOs.Modulos
{
    public class SubmoduloDto
    {
        public int Id { get; set; }
        public Nullable<int> AreaId { get; set; }
        public int ModuloId { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
        public string Icono { get; set; } = string.Empty;
        public int Orden { get; set; } 

        public List<OpcionDto> Opciones { get; set; } = new List<OpcionDto>();
    }
}
