using DGSP.Shared.Contracts.DTOs.Permisos;

namespace DGSP.Shared.Contracts.DTOs.Modulos
{
    public class OpcionDto
    {
        public int Id { get; set; }
        public int SubmoduloId { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
        public string Icono { get; set; } = string.Empty;
        public int Orden { get; set; } 
        public List<PermisoOpcionDto> Permisos {get;set;} = new List<PermisoOpcionDto>();
    }
}
