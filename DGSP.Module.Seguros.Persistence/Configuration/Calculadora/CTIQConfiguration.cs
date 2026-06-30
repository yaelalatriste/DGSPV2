using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Calculadora;

public class CTIQConfiguration : IEntityTypeConfiguration<CTIQ>
{
    public void Configure(EntityTypeBuilder<CTIQ> builder)
    {
        builder.ToTable("CTIQ", "dbo");
        builder.HasKey(e => e.FiIdIQ).HasName("pkCTIQ");
        builder.Property(e => e.FiIdIQ).HasColumnName("fiIdIQ").ValueGeneratedNever();
        builder.Property(e => e.FcDescIQ).HasColumnName("fcDescIQ").HasMaxLength(50).IsUnicode(false);
        builder.Property(e => e.FlStatus).HasColumnName("flStatus");
    }
}
