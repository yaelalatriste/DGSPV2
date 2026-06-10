using DGSP.Module.Catalogos.Domain.Generales;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogos.Persistence.Database.Configuration
{
    public class CTAreaConfiguration
    {
        public CTAreaConfiguration(EntityTypeBuilder<CTArea> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
