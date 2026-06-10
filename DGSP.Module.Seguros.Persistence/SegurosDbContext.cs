using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using DGSP.Module.Seguros.Persistence.Configuration.Reportes;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence
{
    public class SegurosDbContext : DbContext
    {
        public SegurosDbContext(DbContextOptions<SegurosDbContext> options) : base(options)
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

        public DbSet<Continuidad> Continuidades { get; set; }
        public DbSet<LogContinuidad> LogsContinuidades { get; set; }
        public DbSet<ContactoContinuidad> CorreosContinuidades { get; set; }
        public DbSet<EntregableContinuidad> EntregablesContinuidades { get; set; }
        public DbSet<OficioContinuidad> OficiosContinuidades { get; set; }
        public DbSet<ContactoContinuidad> ContactosContinuidades { get; set; }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Continuidad>().ToTable("Continuidades", "Seguros");
            modelBuilder.Entity<LogContinuidad>().ToTable("LogsContinuidades", "Seguros");
            modelBuilder.Entity<ContactoContinuidad>().ToTable("ContactoContinuidades", "Seguros");
            modelBuilder.Entity<EntregableContinuidad>().ToTable("EntregablesContinuidades", "Seguros");
            modelBuilder.Entity<OficioContinuidad>().ToTable("OficiosContinuidad", "Seguros");

            new ContinuidadConfiguration(modelBuilder.Entity<Continuidad>());
            new LogContinuidadConfiguration(modelBuilder.Entity<LogContinuidad>());
            new EntregableContinuidadConfiguration(modelBuilder.Entity<EntregableContinuidad>());
            new ContactoContinuidadConfiguration(modelBuilder.Entity<ContactoContinuidad>());
            new OficioContinuidadConfiguration(modelBuilder.Entity<OficioContinuidad>());
        }
    }
}

