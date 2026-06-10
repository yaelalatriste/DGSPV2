using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Reportes
{
    public class ContinuidadConfiguration
    {
        public ContinuidadConfiguration(EntityTypeBuilder<Continuidad> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.EstatusId).HasDefaultValue(1);

        }
    }
}
