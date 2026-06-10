using DGSP.Module.SMedicos.Domain.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMedicos.Persistence.Database.Configuration
{
    public class TomaEstudiosConfiguration
    {
        public TomaEstudiosConfiguration(EntityTypeBuilder<TomaEstudio> entityBuilder)
        {
            entityBuilder.HasKey(x => x.FiIdTomaEstudios);
        }
    }
}
