using DGSP.Module.SMedicos.Domain.SIACOM.DCatalogos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMedicos.Persistence.Database.Configuration
{
    public class CTTipoConsultaConfiguration
    {
        public CTTipoConsultaConfiguration(EntityTypeBuilder<CTTipoConsulta> entityBuilder)
        {
            entityBuilder.HasKey(x => x.FiIdTipoConsulta);
        }
    }
}
