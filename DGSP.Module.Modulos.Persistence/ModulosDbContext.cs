using DGSP.Module.Modulos.Domain.DModulos;
using DGSP.Module.Modulos.Domain.DOpciones;
using DGSP.Module.Modulos.Domain.DSubmodulos;
using Microsoft.EntityFrameworkCore;
using Modulos.Persistence.Database.Configuration;

namespace Modulos.Persistence.Database
{
    public class ModulosDbContext : DbContext
    {
        private readonly string _connectionString;

        public ModulosDbContext(DbContextOptions<ModulosDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("DGSP");

            ModelConfig(builder);
        }

        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Submodulo> Submodulos { get; set; }
        public DbSet<Opcion> Opciones { get; set; }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new ModulosConfiguration(modelBuilder.Entity<Modulo>());
            new SubmodulosConfiguration(modelBuilder.Entity<Submodulo>());
            new OpcionConfiguration(modelBuilder.Entity<Opcion>());
        }
    }
}
