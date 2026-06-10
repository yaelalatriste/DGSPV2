using DGSP.Module.Permisos.Domain.DPermisos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Permisos.Persistence.Database.Configuration
{
    public class PermisosUsuarioConfiguration
    {
        public PermisosUsuarioConfiguration(EntityTypeBuilder<PermisoUsuario> entityBuilder)
        {
            entityBuilder.HasKey(x => new { x.UsuarioId, x.ModuloId, x.SubmoduloId, x.PermisoId, x.OpcionId });
        }
    }
}
