using Microsoft.EntityFrameworkCore;
using DGSP.Module.SMedicos.Domain.SIACOM.DCatalogos;
using DGSP.Module.SMedicos.Domain.SIACOM.DConsultas;
using SMedicos.Persistence.Database.Configuration;

namespace DGSP.Module.SMedicos.Persistence
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
            modelBuilder.Entity<ConsultaMedica>().ToTable("ConsultaMedica", "dbo");
            modelBuilder.Entity<ConsultaOdontologica>().ToTable("ConsultaOdont", "dbo");
            
            modelBuilder.Entity<RevisionEnfermeria>().ToTable("RevisionEnfermeria", "dbo");
            modelBuilder.Entity<AplicacionMedicamento>().ToTable("AplicaMedicamentos", "dbo");
            modelBuilder.Entity<SignosVitales>().ToTable("SignosVitales", "dbo");
            modelBuilder.Entity<TomaEstudio>().ToTable("TomaEstudios", "dbo");

            modelBuilder.Entity<CTConsultorio>().ToTable("CTConsultorio", "dbo");
            modelBuilder.Entity<CTTipoConsulta>().ToTable("CTTipoConsulta", "dbo");
            modelBuilder.Entity<CTTipoConsultaDetalle>().ToTable("CTTipoConsultaDetalle", "dbo");
            modelBuilder.Entity<CTTipoServicio>().ToTable("CTTipoServicio", "dbo");

            new ConsultaMedicaConfiguration(modelBuilder.Entity<ConsultaMedica>());
            new ConsultaOdontologicaConfiguration(modelBuilder.Entity<ConsultaOdontologica>());
            new RevisionEnfermeriaConfiguration(modelBuilder.Entity<RevisionEnfermeria>());
            new SignosVitalesConfiguration(modelBuilder.Entity<SignosVitales>());
            new TomaEstudiosConfiguration(modelBuilder.Entity<TomaEstudio>());
            new AplicacionMedicamentoConfiguration(modelBuilder.Entity<AplicacionMedicamento>());
            
            new CTConsultorioConfiguration(modelBuilder.Entity<CTConsultorio>());
            new CTTipoConsultaConfiguration(modelBuilder.Entity<CTTipoConsulta>());
            new CTTipoServicioConfiguration(modelBuilder.Entity<CTTipoServicio>());
            new CTTipoConsultaDetalleConfiguration(modelBuilder.Entity<CTTipoConsultaDetalle>());
        }

    }
}
