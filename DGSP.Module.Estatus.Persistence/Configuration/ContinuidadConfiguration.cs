using DGSP.Module.Estatus.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Estatus.Persistence.Configuration
{
    public class ContinuidadConfiguration
    {
        public ContinuidadConfiguration(EntityTypeBuilder<EstatusContinuidad> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
        }
    }
}