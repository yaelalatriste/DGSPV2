using DGSP.Shared.Contracts.DTOs.Modulos;

namespace DGSP.Shared.Contracts.DTOs.Permisos
{
    public class PermisoUsuarioDto
    {
        public string UsuarioId { get; set; } = string.Empty;
        public int ModuloId { get; set; }
        public int SubmoduloId { get; set; }
        public int PermisoId { get; set; }
        public int OpcionId { get; set; }

        public ModuloDto Modulo { get; set; } = new ModuloDto();
        public SubmoduloDto Submodulo { get; set; } = new SubmoduloDto();
        public OpcionDto Opciones { get; set; } = new OpcionDto();
        public PermisoDto Permiso { get; set; } = new PermisoDto();
    }
}
