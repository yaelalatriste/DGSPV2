using Catalogos.Persistence.Database.Configuration;
using DGSP.Module.Catalogos.Domain.Generales;
using DGSP.Module.Catalogos.Domain.Seguros;
using DGSP.Module.Catalogos.Domain.SMedicos;
using Microsoft.EntityFrameworkCore;

namespace Catalogos.Persistence.Database
{
    public class CatalogoDbContext : DbContext
    {
        public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options) : base(options)
        {
        }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Catalogo");

            ModelConfig(builder);
        }

        public DbSet<CTArea> CTAreas { get; set; }
        public DbSet<CTMes> CTMeses { get; set; }
        public DbSet<CTUnidad> CTUnidades { get; set; }
        public DbSet<CTTipoMovimiento> CTTiposMovimientos { get; set; }
        public DbSet<CTConsultorio> CTConsultorios { get; set; }
        public DbSet<CTMedicamento> CTMedicamentos { get; set; }
        public DbSet<CTVariableMedica> CTVariablesMedicas { get; set; }
        public DbSet<CTTipoInsumo> CTTiposInsumos { get; set; }
        public DbSet<CTEntregable> CTEntregables { get; set; }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CTArea>().ToTable("CTAreas", "Catalogo");
            modelBuilder.Entity<CTMes>().ToTable("CTMeses", "Catalogo");
            modelBuilder.Entity<CTEntregable>().ToTable("CTEntregables", "Catalogo");
            modelBuilder.Entity<CTConsultorio>().ToTable("CTConsultorios", "Catalogo");
            modelBuilder.Entity<CTMedicamento>().ToTable("CTMedicamentos", "Catalogo");
            modelBuilder.Entity<CTVariableMedica>().ToTable("CTVariablesMedicas", "Catalogo");
            modelBuilder.Entity<CTUnidad>().ToTable("CTUnidades", "Catalogo");
            modelBuilder.Entity<CTTipoMovimiento>().ToTable("CTTiposMovimientos", "Catalogo");
            modelBuilder.Entity<CTTipoInsumo>().ToTable("CTTIposInsumos", "Catalogo");
            
            new CTAreaConfiguration(modelBuilder.Entity<CTArea>());
        }
    }
}
