using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Calculadora;

public class CTTpoPolizaConfiguration : IEntityTypeConfiguration<CTTpoPoliza>
{
    public void Configure(EntityTypeBuilder<CTTpoPoliza> builder)
    {
        builder.ToTable("CTTpoPoliza", "dbo");
        builder.HasKey(e => e.FiIdTpoPol).HasName("PK_CTTpoPoliza");
        builder.Property(e => e.FiIdTpoPol).HasColumnName("fiIdTpoPol").ValueGeneratedNever();
        builder.Property(e => e.FcDescTpoPol).HasColumnName("fcDescTpoPol").HasMaxLength(100).IsUnicode(false);
        builder.Property(e => e.FcNomCortoTpoPol).HasColumnName("fcNomCortoTpoPol").HasMaxLength(15).IsUnicode(false);
        builder.Property(e => e.FdFchAltaTpoPol).HasColumnName("fdFchAltaTpoPol").HasColumnType("smalldatetime");
        builder.Property(e => e.FiUsrAltaTpoPol).HasColumnName("fiUsrAltaTpoPol");
        builder.Property(e => e.FdFchModTpoPol).HasColumnName("fdFchModTpoPol").HasColumnType("smalldatetime");
        builder.Property(e => e.FiUsrModTpoPol).HasColumnName("fiUsrModTpoPol");
        builder.Property(e => e.FlStatusTpoPol).HasColumnName("flStatusTpoPol");
    }
}
