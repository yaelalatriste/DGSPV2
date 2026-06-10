using DGSP.Module.Usuarios.Domain.DUsuarios;
using DGSP.Module.Usuarios.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Usuarios.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
        )
            : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Database schema
            builder.HasDefaultSchema("DGSP");

            // Model Contraints
            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new UsuarioConfiguration(modelBuilder.Entity<Usuario>());
        }
    }
}
