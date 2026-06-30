using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Calculadora;

public class CTSumaAsegConfiguration : IEntityTypeConfiguration<CTSumaAseg>
{
    public void Configure(EntityTypeBuilder<CTSumaAseg> builder)
    {
        builder.ToTable("CTSumaAseg", "dbo");
        builder.HasKey(e => e.FiIdRegSA).HasName("PK_CTSumaAseg");
        builder.Property(e => e.FiIdRegSA).HasColumnName("fiIdRegSA").ValueGeneratedNever();
        builder.Property(e => e.FcDescSumAseg).HasColumnName("fcDescSumAseg").HasMaxLength(15).IsUnicode(false);
        builder.Property(e => e.FdFchAltaSumAseg).HasColumnName("fdFchAltaSumAseg").HasColumnType("smalldatetime");
        builder.Property(e => e.FiUsrAltaSumAseg).HasColumnName("fiUsrAltaSumAseg");
        builder.Property(e => e.FdFchModSumAseg).HasColumnName("fdFchModSumAseg").HasColumnType("smalldatetime");
        builder.Property(e => e.FiUsrModSumAseg).HasColumnName("fiUsrModSumAseg");
        builder.Property(e => e.FlStatusSumAseg).HasColumnName("flStatusSumAseg");
    }
}
