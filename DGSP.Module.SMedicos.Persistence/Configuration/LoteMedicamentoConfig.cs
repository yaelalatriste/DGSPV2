using DGSP.Module.SMedicos.Domain.Inventarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.SMedicos.Persistence.Configuration
{
    public class LoteMedicamentoConfig : IEntityTypeConfiguration<LoteMedicamento>
    {
        public void Configure(EntityTypeBuilder<LoteMedicamento> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
