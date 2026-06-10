using DGSP.Module.SMedicos.Domain.Inventarios;
using DGSP.Module.SMedicos.Domain.Movimientos;
using DGSP.Module.SMedicos.Domain.NotasTraspaso;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.SMedicos.Persistence
{
    public class SMedicosDbContext : DbContext
    {
        public SMedicosDbContext(DbContextOptions<SMedicosDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.HasDefaultSchema("SMedicos");

            base.OnModelCreating(builder);

            ModelConfig(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<LoteMedicamento> LotesMedicamentos => Set<LoteMedicamento>();
        public DbSet<MovimientoInventario> MovimientosInventario => Set<MovimientoInventario>();
        public DbSet<SalidaMedicamento> SalidasMedicamentos => Set<SalidaMedicamento>();
        public DbSet<SalidaMedicamentoDetalle> SalidasMedicamentosDetalle => Set<SalidaMedicamentoDetalle>();
        public DbSet<NotaTraspaso> NotasTraspaso => Set<NotaTraspaso>();
        public DbSet<DetalleNotaTraspaso> DetallesNotaTraspaso => Set<DetalleNotaTraspaso>();
        public DbSet<LogNotaTraspaso> LogsNotasTraspaso => Set<LogNotaTraspaso>();

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotaTraspaso>(entity => {
                entity.ToTable("NotasTraspaso", "SMedicos");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.EstatusId).HasDefaultValue(1);
            });

            modelBuilder.Entity<LogNotaTraspaso>(entity => {
                entity.ToTable("LogsNotasTraspaso", "SMedicos");
                entity.HasKey(x => x.Id);
            });
            
            modelBuilder.Entity<DetalleNotaTraspaso>(entity => {
                entity.ToTable("DetallesNotaTraspaso", "SMedicos");
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<LoteMedicamento>(entity =>
            {                                
                entity.ToTable("LotesMedicamentos", "SMedicos");
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => new { x.ConsultorioId, x.MedicamentoId, x.Lote, x.FechaCaducidad }).IsUnique();
            });

            modelBuilder.Entity<MovimientoInventario>(entity =>
            {
                entity.ToTable("MovimientosInventario", "SMedicos");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.TipoMovimiento).HasMaxLength(20).IsRequired();
                entity.Property(x => x.Origen).HasMaxLength(30);
                entity.Property(x => x.UsuarioId).HasMaxLength(256);
                entity.Property(x => x.Observaciones).HasMaxLength(400);
            });

            modelBuilder.Entity<SalidaMedicamento>(entity =>
            {
                entity.ToTable("SalidasMedicamentos", "SMedicos");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Observaciones).HasMaxLength(400);
                entity.Property(x => x.UsuarioId).HasMaxLength(100);
            });

            modelBuilder.Entity<SalidaMedicamentoDetalle>(entity =>
            {
                entity.ToTable("SalidasMedicamentosDetalle", "SMedicos");
                entity.HasKey(x => x.Id);
            });
        }
    }
}

