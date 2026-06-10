using DGSP.Module.Modulos.Domain.DSubmodulos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modulos.Persistence.Database.Configuration
{
    public class SubmodulosConfiguration
    {
        public SubmodulosConfiguration(EntityTypeBuilder<Submodulo> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
