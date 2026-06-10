using DGSP.Module.SMedicos.Domain.SIACOM.DCatalogos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMedicos.Persistence.Database.Configuration
{
    public class CTConsultorioConfiguration
    {
        public CTConsultorioConfiguration(EntityTypeBuilder<CTConsultorio> entityBuilder)
        {
            entityBuilder.HasKey(x => x.FiIdConsultorio);
        }
    }
}
