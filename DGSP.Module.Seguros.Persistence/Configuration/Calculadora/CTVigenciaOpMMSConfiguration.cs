using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Calculadora;

public class CTVigenciaOpMMSConfiguration : IEntityTypeConfiguration<CTVigenciaOpMMS>
{
    public void Configure(EntityTypeBuilder<CTVigenciaOpMMS> builder)
    {
        builder.ToTable("CTVigenciaOpMMS", "dbo");
        builder.HasKey(e => e.FiIdRegVig).HasName("pk001CTVigencia");
        builder.Property(e => e.FiIdRegVig).HasColumnName("fiIdRegVig").ValueGeneratedNever();
        builder.Property(e => e.FiIdTpoPol).HasColumnName("fiIdTpoPol");
        builder.Property(e => e.FiIdRegAseg).HasColumnName("fiIdRegAseg");
        builder.Property(e => e.FcCveAsegVig).HasColumnName("fcCveAsegVig").HasMaxLength(15).IsUnicode(false);
        builder.Property(e => e.FdFchIniVig).HasColumnName("fdFchIniVig").HasColumnType("smalldatetime");
        builder.Property(e => e.FdFchFinVig).HasColumnName("fdFchFinVig").HasColumnType("smalldatetime");
        builder.Property(e => e.FdFchAltaVig).HasColumnName("fdFchAltaVig").HasColumnType("smalldatetime");
        builder.Property(e => e.FiUsrAltaVig).HasColumnName("fiUsrAltaVig");
        builder.Property(e => e.FiIdRegVigAnt).HasColumnName("fiIdRegVigAnt");
        builder.Property(e => e.FcVigencia).HasColumnName("fcVigencia").HasMaxLength(5).IsUnicode(false);
    }
}
