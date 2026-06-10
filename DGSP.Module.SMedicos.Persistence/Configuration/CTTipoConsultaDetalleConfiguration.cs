using DGSP.Module.SMedicos.Domain.SIACOM.DCatalogos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMedicos.Persistence.Database.Configuration
{
    public class CTTipoConsultaDetalleConfiguration
    {
        public CTTipoConsultaDetalleConfiguration(EntityTypeBuilder<CTTipoConsultaDetalle> entityBuilder)
        {
            entityBuilder.HasKey(x => x.FiIdTipoConsultaDetalle);
        }
    }
}
