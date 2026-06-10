using DGSP.Domain.SMedicos.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Infraestructure.Data.Configuraciones.SMedicos
{
    public class RevisionEnfermeriaConfig : IEntityTypeConfiguration<RevisionEnfermeria>
    {
        public void Configure(EntityTypeBuilder<RevisionEnfermeria> builder)
        {
            builder.HasKey(x => x.FiIdRevisionEnfermeria);
        }
    }
}
