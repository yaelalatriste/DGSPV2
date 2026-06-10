using DGSP.Module.DGRH.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.DGRH.Persistence.Configuration
{
    public class EmpleadoConfiguration
    {
        public EmpleadoConfiguration(EntityTypeBuilder<Empleado> entityBuilder)
        {
            entityBuilder.HasKey(x => x.exp);
        }
    }
}
