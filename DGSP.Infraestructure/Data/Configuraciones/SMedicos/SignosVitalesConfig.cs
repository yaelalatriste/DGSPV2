using DGSP.Domain.SMedicos.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Infraestructure.Data.Configuraciones.SMedicos
{
    public class SignosVitalesConfig : IEntityTypeConfiguration<SignosVitales>
    {
        public void Configure(EntityTypeBuilder<SignosVitales> builder)
        {
            builder.HasKey(x => x.FiIdSignosVitales);
        }
    }
}
