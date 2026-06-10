using DGSP.Domain.Catalogos.Consultorios;
using DGSP.Domain.Catalogos.DMeses;
using DGSP.Domain.Catalogos.Medicamentos;
using DGSP.Domain.SMedicos.TiposInsumos;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Infraestructure.Data.Catalogos
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

        public DbSet<CTMes> CTMeses { get; set; }
        public DbSet<CTConsultorio> CTConsultorios { get; set; }
        public DbSet<CTMedicamento> CTMedicamentos { get; set; }
        public DbSet<TipoInsumo> CTTiposInsumos { get; set; }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoDbContext).Assembly);
        }
    }
}
