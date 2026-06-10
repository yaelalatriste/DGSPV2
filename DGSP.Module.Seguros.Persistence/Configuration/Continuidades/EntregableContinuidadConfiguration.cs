using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
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
