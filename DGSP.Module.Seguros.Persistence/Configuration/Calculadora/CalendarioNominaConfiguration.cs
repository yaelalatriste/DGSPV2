using DGSP.Module.Seguros.Domain.DGSP.Movimientos.Calculadora;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Reportes
{
    public class CalendarioNominaConfiguration
    {
        public CalendarioNominaConfiguration(EntityTypeBuilder<CalendarioNomina> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
