using DGSP.Module.DGRH.Domain.RH.DPuestos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.DGRH.Persistence.Configuration
{
    public class PuestoConfiguration
    {
        public PuestoConfiguration(EntityTypeBuilder<Puesto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.cve_puesto);
        }
    }
}
