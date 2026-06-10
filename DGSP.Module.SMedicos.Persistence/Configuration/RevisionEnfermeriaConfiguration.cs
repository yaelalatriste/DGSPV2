using DGSP.Module.SMedicos.Domain.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMedicos.Persistence.Database.Configuration
{
    public class RevisionEnfermeriaConfiguration
    {
        public RevisionEnfermeriaConfiguration(EntityTypeBuilder<RevisionEnfermeria> entityBuilder)
        {
            entityBuilder.HasKey(x => x.FiIdRevisionEnfermeria);
        }
    }
}
