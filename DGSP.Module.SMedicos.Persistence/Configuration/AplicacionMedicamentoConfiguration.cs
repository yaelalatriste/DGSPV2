using DGSP.Module.SMedicos.Domain.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMedicos.Persistence.Database.Configuration
{
    public class AplicacionMedicamentoConfiguration
    {
        public AplicacionMedicamentoConfiguration(EntityTypeBuilder<AplicacionMedicamento> entityBuilder)
        {
            entityBuilder.HasKey(x => x.FiIdAplicaMedicamentos);
        }
    }
}
