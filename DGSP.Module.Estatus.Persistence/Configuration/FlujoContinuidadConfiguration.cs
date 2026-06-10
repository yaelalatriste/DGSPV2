using DGSP.Module.Estatus.Domain.Continuidade;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Estatus.Persistence.Configuration
{
    public class FlujoContinuidadConfiguration
    {
        public FlujoContinuidadConfiguration(EntityTypeBuilder<FlujoContinuidad> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => new { x.EstatusId, x.ESucesivoId, x.PermisoId, x.Boton });
        }
    }
}
