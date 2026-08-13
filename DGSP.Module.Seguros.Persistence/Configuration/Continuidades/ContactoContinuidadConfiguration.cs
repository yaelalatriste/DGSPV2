using DGSP.Module.Seguros.Domain.DGSP.Siniestros.Continuidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Reportes
{
    public class ContactoContinuidadConfiguration
    {
        public ContactoContinuidadConfiguration(EntityTypeBuilder<ContactoContinuidad> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id );
        }
    }
}
