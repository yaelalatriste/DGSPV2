using DGSP.Module.Modulos.Domain.DModulos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modulos.Persistence.Database.Configuration
{
    public class ModulosConfiguration
    {
        public ModulosConfiguration(EntityTypeBuilder<Modulo> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
