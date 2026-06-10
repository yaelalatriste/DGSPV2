using DGSP.Module.SMedicos.Domain.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMedicos.Persistence.Database.Configuration
{
    public class SignosVitalesConfiguration
    {
        public SignosVitalesConfiguration(EntityTypeBuilder<SignosVitales> entityBuilder)
        {
            entityBuilder.HasKey(x => x.FiIdSignosVitales);
        }
    }
}
