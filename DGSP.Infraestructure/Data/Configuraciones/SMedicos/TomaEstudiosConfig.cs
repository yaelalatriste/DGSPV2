using DGSP.Domain.SMedicos.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Infraestructure.Data.Configuraciones.SMedicos
{
    public class TomaEstudiosConfig : IEntityTypeConfiguration<TomaEstudio>
    {
        public void Configure(EntityTypeBuilder<TomaEstudio> builder)
        {
            builder.HasKey(x => x.FiIdTomaEstudios);
        }
    }
}
