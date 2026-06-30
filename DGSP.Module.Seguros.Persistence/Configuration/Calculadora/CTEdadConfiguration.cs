using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Calculadora;

public class CTEdadConfiguration : IEntityTypeConfiguration<CTEdad>
{
    public void Configure(EntityTypeBuilder<CTEdad> builder)
    {
        builder.ToTable("CTEdad", "dbo");
        builder.HasKey(e => e.fiIdRegEdad).HasName("PK_CTEdad");
        builder.Property(e => e.fiIdRegEdad).HasColumnName("fiIdRegEdad").ValueGeneratedNever();
        builder.Property(e => e.fiLimInfEdad).HasColumnName("fiLimInfEdad");
        builder.Property(e => e.fiLimSupEdad).HasColumnName("fiLimSupEdad");
        builder.Property(e => e.fdFchAltaEdad).HasColumnName("fdFchAltaEdad").HasColumnType("smalldatetime");
        builder.Property(e => e.fiUsrAltaEdad).HasColumnName("fiUsrAltaEdad");
        builder.Property(e => e.fdFchModEdad).HasColumnName("fdFchModEdad").HasColumnType("smalldatetime");
        builder.Property(e => e.fiUsrModEdad).HasColumnName("fiUsrModEdad");
        builder.Property(e => e.flStatusEdad).HasColumnName("flStatusEdad");
    }
}
