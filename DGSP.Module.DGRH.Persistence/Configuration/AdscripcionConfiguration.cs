using DGSP.Module.DGRH.Domain.RH.DAdscripciones;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.DGRH.Persistence.Configuration
{
    public class AdscripcionConfiguration
    {
        public AdscripcionConfiguration(EntityTypeBuilder<Adscripcion> entityBuilder)
        {
            entityBuilder.HasKey(x => x.cve_adscripcion);
        }
    }
}
