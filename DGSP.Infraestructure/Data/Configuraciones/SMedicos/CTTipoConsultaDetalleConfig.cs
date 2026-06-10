using DGSP.Domain.SMedicos.SIACOM.DCatalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Infraestructure.Data.Configuraciones.SMedicos
{
    public class CTTipoConsultaDetalleConfig : IEntityTypeConfiguration<CTTipoConsultaDetalle>
    {
        public void Configure(EntityTypeBuilder<CTTipoConsultaDetalle> builder)
        {
            builder.HasKey(x => x.FiIdTipoConsultaDetalle);
        }
    }
}
