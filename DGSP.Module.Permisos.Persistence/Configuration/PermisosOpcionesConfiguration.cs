using DGSP.Module.Permisos.Domain.DPermisos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Permisos.Persistence.Database.Configuration
{
    public class PermisosOpcionesConfiguration
    {
        public PermisosOpcionesConfiguration(EntityTypeBuilder<PermisoOpcion> entityBuilder)
        {
            entityBuilder.HasKey(x => new { x.SubmoduloId, x.PermisoId, x.OpcionId });
        }
    }
}
