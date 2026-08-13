using DGSP.Module.Seguros.Domain.DGSP.Siniestros.Continuidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Reportes
{
    public class EntregableContinuidadConfiguration
    {
        public EntregableContinuidadConfiguration(EntityTypeBuilder<EntregableContinuidad> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
