namespace DGSP.Shared.Contracts.DTOs.Permisos
{
    public class PermisoOpcionDto
    {
        public int SubmoduloId { get; set; }
        public int OpcionId { get; set; }
        public int PermisoId { get; set; }
        public PermisoDto Permiso { get; set; } = new PermisoDto();
    }
}
