using DGSP.Module.SMedicos.Domain.SIACOM.DCatalogos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMedicos.Persistence.Database.Configuration
{
    public class CTTipoServicioConfiguration
    {
        public CTTipoServicioConfiguration(EntityTypeBuilder<CTTipoServicio> entityBuilder)
        {
            entityBuilder.HasKey(x => x.FiIdTipoServicio);
        }
    }
}
