using DGSP.Domain.SMedicos.SIACOM.DCatalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Infraestructure.Data.Configuraciones.SMedicos
{
    public class CTTipoServicioConfig : IEntityTypeConfiguration<CTTipoServicio>
    {
        public void Configure(EntityTypeBuilder<CTTipoServicio> builder)
        {
            builder.HasKey(x => x.FiIdTipoServicio);
        }
    }
}
