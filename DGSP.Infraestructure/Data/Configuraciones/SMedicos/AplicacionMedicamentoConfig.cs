using DGSP.Domain.SMedicos.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Infraestructure.Data.Configuraciones.SMedicos
{
    public class AplicacionMedicamentoConfig : IEntityTypeConfiguration<AplicacionMedicamento>
    {
        public void Configure(EntityTypeBuilder<AplicacionMedicamento> builder)
        {
            builder.HasKey(x => x.FiIdAplicaMedicamentos);
        }
    }
}
