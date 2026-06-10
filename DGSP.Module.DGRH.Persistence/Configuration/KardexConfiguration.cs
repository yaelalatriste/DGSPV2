using DGSP.Module.DGRH.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.DGRH.Persistence.Configuration
{
    public class KardexConfiguration
    {
        public KardexConfiguration(EntityTypeBuilder<Kardex> entityBuilder)
        {
            entityBuilder.HasKey(x => new { x.exp, x.csc_nomb});
        }
    }
}
