using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Reportes
{
    public class LogContinuidadConfiguration
    {
        public LogContinuidadConfiguration(EntityTypeBuilder<LogContinuidad> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
