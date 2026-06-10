using DGSP.Module.Estatus.Domain;
using DGSP.Module.Estatus.Domain.Continuidade;
using DGSP.Module.Estatus.Domain.NotasTraspaso;
using DGSP.Module.Estatus.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Estatus.Persistence
{
    public class EstatusDbContext : DbContext
    {
        public EstatusDbContext(DbContextOptions<EstatusDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Estatus");

            ModelConfiguration(builder);
        }

        public DbSet<EstatusContinuidad> EstatusContinuidades { get; set; }
        public DbSet<ENotaTraspaso> ENotasTraspaso { get; set; }
        public DbSet<FlujoContinuidad> FlujoContinuidades { get; set; }
        public DbSet<FlujoEntregableContinuidad> FlujoEntregablesContinuidades { get; set; }
        public DbSet<FlujoNotaTraspaso> FlujoNotasTraspaso { get; set; }

        private void ModelConfiguration(ModelBuilder builder)
        {
            builder.Entity<EstatusContinuidad>().ToTable("EstatusContinuidades", "Estatus");
            builder.Entity<ENotaTraspaso>().ToTable("ENotasTraspaso", "Estatus");
            builder.Entity<FlujoContinuidad>().ToTable("FlujoContinuidades", "Estatus");
            builder.Entity<FlujoEntregableContinuidad>().ToTable("FlujoEntregablesContinuidades", "Estatus");
            builder.Entity<FlujoNotaTraspaso>().ToTable("FlujoNotasTraspaso", "Estatus");

            new ENotaTraspasoConfiguration(builder.Entity<ENotaTraspaso>());
            new ContinuidadConfiguration(builder.Entity<EstatusContinuidad>());
            new FlujoContinuidadConfiguration(builder.Entity<FlujoContinuidad>());
            new FlujoEntregableContinuidadConfiguration(builder.Entity<FlujoEntregableContinuidad>());
            new FlujoNotaTraspasoConfiguration(builder.Entity<FlujoNotaTraspaso>());
        }
    }
}
