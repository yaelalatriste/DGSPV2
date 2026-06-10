using DGSP.Domain.SMedicos.SIACOM.DCatalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Infraestructure.Data.Configuraciones.SMedicos
{
    public class CTTipoConsultaConfig : IEntityTypeConfiguration<CTTipoConsulta>
    {
        public void Configure(EntityTypeBuilder<CTTipoConsulta> builder)
        {
            builder.HasKey(x => x.FiIdTipoConsulta);
        }
    }
}
