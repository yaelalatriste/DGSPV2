using DGSP.Module.SMedicos.Domain.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMedicos.Persistence.Database.Configuration
{
    public class ConsultaMedicaConfiguration
    {
        public ConsultaMedicaConfiguration(EntityTypeBuilder<ConsultaMedica> entityBuilder)
        {
            entityBuilder.HasKey(x => x.FiIdConsultaMedica);
        }
    }
}
