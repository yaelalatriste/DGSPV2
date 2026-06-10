using DGSP.Module.Permisos.Domain.DPermisos;
using Microsoft.EntityFrameworkCore;
using Permisos.Persistence.Database.Configuration;

namespace Permisos.Persistence.Database
{
    public class PermisosDbContext : DbContext
    {
        public PermisosDbContext(
           DbContextOptions<PermisosDbContext> options) : base(options)
        {

        }

        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<PermisoUsuario> PermisosUsuario { get; set; }
        public DbSet<PermisoOpcion> PermisosOpciones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("DGSP");
            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new PermisosUsuarioConfiguration(modelBuilder.Entity<PermisoUsuario>());
            new PermisosOpcionesConfiguration(modelBuilder.Entity<PermisoOpcion>());
        }
    }
}
