using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Calculadora;

public class CTParentescoConfiguration : IEntityTypeConfiguration<CTParentesco>
{
    public void Configure(EntityTypeBuilder<CTParentesco> builder)
    {
        builder.ToTable("CTParentesco", "dbo");
        builder.HasKey(e => e.FiIdParent).HasName("PK_CTParentesco");
        builder.Property(e => e.FiIdParent).HasColumnName("fiIdParent").ValueGeneratedNever();
        builder.Property(e => e.FcDescParent).HasColumnName("fcDescParent").HasMaxLength(30).IsUnicode(false);
        builder.Property(e => e.FcCveParentAnt).HasColumnName("fcCveParentAnt").HasMaxLength(50).IsUnicode(false);
        builder.Property(e => e.FdFchAltaParent).HasColumnName("fdFchAltaParent").HasColumnType("smalldatetime");
        builder.Property(e => e.FiUsrAltaParent).HasColumnName("fiUsrAltaParent");
        builder.Property(e => e.FdFchModParent).HasColumnName("fdFchModParent").HasColumnType("smalldatetime");
        builder.Property(e => e.FiUsrModParent).HasColumnName("fiUsrModParent");
        builder.Property(e => e.FlStatusParent).HasColumnName("flStatusParent");
        builder.Property(e => e.FlEstatusSGMM).HasColumnName("flEstatusSGMM");
        builder.Property(e => e.FlEstatusSVeh).HasColumnName("flEstatusSVeh");
    }
}
