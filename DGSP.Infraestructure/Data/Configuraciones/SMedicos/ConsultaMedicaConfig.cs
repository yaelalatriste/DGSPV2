using DGSP.Domain.SMedicos.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Infraestructure.Data.Configuraciones.SMedicos
{
    public class ConsultaMedicaConfig : IEntityTypeConfiguration<ConsultaMedica>
    {
        public void Configure(EntityTypeBuilder<ConsultaMedica> builder)
        {
            builder.HasKey(x => x.FiIdConsultaMedica);
        }
    }
}
