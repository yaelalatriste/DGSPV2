using DGSP.Domain.SMedicos.Medicamentos;
using DGSP.Domain.SMedicos.Movimientos;
using DGSP.Domain.SMedicos.TiposInsumos;
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

            builder.Entity<MedicamentoLote>().HasKey(x => x.LoteId);

            builder.Entity<TipoInsumo>().HasKey(x => x.TipoInsumoId);

            builder.Entity<MedicamentoLote>().HasIndex(x => new { x.ConsultorioId, x.MedicamentoId, x.Lote }).IsUnique();

            builder.Entity<MovimientoInventario>().HasKey(x => x.MovimientoId);

            base.OnModelCreating(builder);

            ModelConfig(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<MedicamentoLote> MedicamentosLote => Set<MedicamentoLote>();
        public DbSet<MovimientoInventario> MovimientosInventario => Set<MovimientoInventario>();

        private void ModelConfig(ModelBuilder modelBuilder)
        {
        }
    }
}

