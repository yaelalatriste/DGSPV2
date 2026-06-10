using DGSP.Module.Modulos.Domain.DOpciones;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modulos.Persistence.Database.Configuration
{
    public class OpcionConfiguration
    {
        public OpcionConfiguration(EntityTypeBuilder<Opcion> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
