using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Calculadora;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGSP.Module.Seguros.Persistence.Configuration.Calculadora;

public class PrimasOpMMSConfiguration : IEntityTypeConfiguration<PrimasOpMMS>
{
    public void Configure(EntityTypeBuilder<PrimasOpMMS> builder)
    {
        builder.ToTable("PrimasOpMMS", "dbo");
        builder.HasKey(e => e.FiIdPrim).HasName("PK_PrimasOpMMS");
        builder.Property(e => e.FiIdPrim).HasColumnName("fiIdPrim").ValueGeneratedNever();
        builder.Property(e => e.FiAnio).HasColumnName("fiAnio");
        builder.Property(e => e.FiIdParent).HasColumnName("fiIdParent");
        builder.Property(e => e.FiIdSAOrigen).HasColumnName("fiIdSAOrigen");
        builder.Property(e => e.FiIdSAPotenciada).HasColumnName("fiIdSAPotenciada");
        builder.Property(e => e.FnMtoPrim).HasColumnName("fnMtoPrim").HasColumnType("numeric(10, 2)");
        builder.Property(e => e.FdFchIniVigPrim).HasColumnName("fdFchIniVigPrim").HasColumnType("smalldatetime");
        builder.Property(e => e.FdFchFinVigPrim).HasColumnName("fdFchFinVigPrim").HasColumnType("smalldatetime");
        builder.Property(e => e.FdFchAltaPrim).HasColumnName("fdFchAltaPrim").HasColumnType("smalldatetime");
        builder.Property(e => e.FiUsrAltaPrim).HasColumnName("fiUsrAltaPrim");
        builder.Property(e => e.FdFchModPrim).HasColumnName("fdFchModPrim").HasColumnType("smalldatetime");
        builder.Property(e => e.FiUsrModPrim).HasColumnName("fiUsrModPrim");
        builder.Property(e => e.FlStatusPrim).HasColumnName("flStatusPrim");
        builder.Property(e => e.FlIndBasicaPrim).HasColumnName("flIndBasicaPrim");
        builder.Property(e => e.FiIdIQ).HasColumnName("fiIdIQ");
        builder.Property(e => e.FiTpoExtraPrima).HasColumnName("fiTpoExtraPrima");
        builder.Property(e => e.FiTpoCAdicional).HasColumnName("fiTpoCAdicional");
        builder.Property(e => e.FiIdRegVig).HasColumnName("fiIdRegVig");
        builder.Property(e => e.FiCveDed).HasColumnName("fiCveDed");
        builder.HasIndex(e => new { e.FiIdRegVig, e.FiIdParent, e.FiIdSAOrigen, e.FiIdSAPotenciada, e.FlIndBasicaPrim, e.FiIdIQ, e.FiTpoExtraPrima, e.FiTpoCAdicional }, "UniquePrimasOpMMSValues").IsUnique();
    }
}
