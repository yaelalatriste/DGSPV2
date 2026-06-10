using DGSP.Module.DGRH.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.DGRH.Persistence.Configuration
{
    public class CiudadConfiguration
    {
        public CiudadConfiguration(EntityTypeBuilder<Ciudad> entityBuilder)
        {
            entityBuilder.HasKey(x => x.cve_cd);
        }
    }
}
