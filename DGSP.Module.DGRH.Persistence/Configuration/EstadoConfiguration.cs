using DGSP.Module.DGRH.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.DGRH.Persistence.Configuration
{
    public class EstadoConfiguration
    {
        public EstadoConfiguration(EntityTypeBuilder<Estado> entityBuilder)
        {
            entityBuilder.HasKey(x => x.CveEdo);
        }
    }
}
