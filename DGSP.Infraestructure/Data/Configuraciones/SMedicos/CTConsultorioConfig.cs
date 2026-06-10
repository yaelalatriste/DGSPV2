using DGSP.Domain.Catalogos.Consultorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Infraestructure.Data.Configuraciones.SMedicos
{
    public class CTConsultorioConfig : IEntityTypeConfiguration<CTConsultorio>
    {
        public void Configure(EntityTypeBuilder<CTConsultorio> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
