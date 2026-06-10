using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Reportes
{
    public class OficioContinuidadConfiguration
    {
        public OficioContinuidadConfiguration(EntityTypeBuilder<OficioContinuidad> entityBuilder)
        {
            entityBuilder.HasKey(x => new { x.ContinuidadId, x.AnioMovimiento,x.TipoMovimiento, x.RegistroMovimiento, x.Oficio, x.ObservacionMovimiento, x.Validado, x.FechaAplicacionMovimientoSP, x.FechaAltaMovimiento });
        }
    }
}
