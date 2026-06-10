using DGSP.Module.Estatus.Domain.NotasTraspaso;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Estatus.Persistence.Configuration
{
    public class FlujoNotaTraspasoConfiguration
    {
        public FlujoNotaTraspasoConfiguration(EntityTypeBuilder<FlujoNotaTraspaso> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => new { x.EstatusId, x.ESucesivoId, x.Boton });
        }
    }
}
