using DGSP.Module.DGRH.Domain.RH.DKardex;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.DGRH.Persistence.Configuration
{
    public class KardexConfiguration
    {
        public KardexConfiguration(EntityTypeBuilder<KardexModel> entityBuilder)
        {
            entityBuilder.HasKey(x => new { x.exp, x.csc_nomb});
        }
    }
}
