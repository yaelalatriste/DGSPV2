using DGSP.Module.Estatus.Domain.NotasTraspaso;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Estatus.Persistence.Configuration
{
    public class ENotaTraspasoConfiguration
    {
        public ENotaTraspasoConfiguration(EntityTypeBuilder<ENotaTraspaso> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
        }
    }
}