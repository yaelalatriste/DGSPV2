using DGSP.Domain.SMedicos.SIACOM.DCatalogos;
using DGSP.Domain.SMedicos.SIACOM.DConsultas;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Infraestructure.Data.SMedicos.Persistence
{
    public class SiacomDbContext : DbContext
    {
        public SiacomDbContext(DbContextOptions<SiacomDbContext> options) : base(options)
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

        public DbSet<ConsultaMedica> ConsultasMedicas { get; set; }
        public DbSet<ConsultaOdontologica> ConsultasOdontologicas { get; set; }
        public DbSet<RevisionEnfermeria> RevisionesEnfermeria { get; set; }
        public DbSet<SignosVitales> SignosVitales { get; set; }
        public DbSet<AplicacionMedicamento> AplicacionMedicamentos { get; set; }
        public DbSet<TomaEstudio> TomaEstudios { get; set; }
        public DbSet<CTConsultorio> CTConsultorios { get; set; }
        public DbSet<CTTipoConsulta> CTTipoConsultas{ get; set; }
        public DbSet<CTTipoConsultaDetalle> CTTipoConsultasDetalle { get; set; }
        public DbSet<CTTipoServicio> CTTiposServicios { get; set; }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SiacomDbContext).Assembly);
        }
    }
}
