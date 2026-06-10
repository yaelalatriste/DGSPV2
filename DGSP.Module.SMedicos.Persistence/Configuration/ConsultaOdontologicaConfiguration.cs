using DGSP.Module.SMedicos.Domain.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMedicos.Persistence.Database.Configuration
{
    public class ConsultaOdontologicaConfiguration
    {
        public ConsultaOdontologicaConfiguration(EntityTypeBuilder<ConsultaOdontologica> entityBuilder)
        {
            entityBuilder.HasKey(x => x.FiIdConsultaOdontologica);
        }
    }
}
