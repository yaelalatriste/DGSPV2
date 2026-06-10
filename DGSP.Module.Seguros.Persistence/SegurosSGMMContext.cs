using DGSP.Module.Seguros.Domain.CJFBDRHDF;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence
{
    public class SegurosSGMMContext : DbContext
    {
        public SegurosSGMMContext(DbContextOptions<SegurosSGMMContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ModelConfig(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<Movimiento> CTMovimientos { get; set; }
        public DbSet<MovimientoSp> MovimientosSP { get; set; }
        public DbSet<Correspondencia> Correspondencias { get; set; }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.fiIdMov).HasName("PK_CTMovimiento");

                entity.ToTable("CTMovimiento");
            });
            
            modelBuilder.Entity<MovimientoSp>(entity =>
            {
                entity.HasKey(e => e.fiIdRegMovSp).HasName("PK_MovimientoSP_1");

                entity.ToTable("MovimientoSP");

                entity.HasIndex(e => new { e.fiExpSp, e.fiIdMov }, "IDX001MovimientoSP").HasFillFactor(90);
            });
            
            modelBuilder.Entity<Correspondencia>(entity =>
            {
                entity.HasKey(e => e.fiIdRegOfic).HasName("PK_Correspondencia_1");
                entity.ToTable("Correspondencia");

                entity.HasIndex(e => new { e.fiAnioRegOfic, e.fcNumSalida }, "IDX001Correspondencia").HasFillFactor(90);
            });
        }
    }
}

