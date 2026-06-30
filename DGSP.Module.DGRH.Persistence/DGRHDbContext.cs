using DGSP.Module.DGRH.Domain.RH.DAdscripciones;
using DGSP.Module.DGRH.Domain.RH.DCiudad;
using DGSP.Module.DGRH.Domain.RH.DEmpleado;
using DGSP.Module.DGRH.Domain.RH.DEstado;
using DGSP.Module.DGRH.Domain.RH.DKardex;
using DGSP.Module.DGRH.Domain.RH.DPuestos;
using DGSP.Module.DGRH.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.DGRH.Persistence
{
    public class DGRHDbContext : DbContext
    {
        public DGRHDbContext(DbContextOptions<DGRHDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ModelConfig(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<Adscripcion> Adscripciones { get;set; }
        public DbSet<Puesto> Puestos { get;set; }
        public DbSet<Estado> Estados { get;set; }
        public DbSet<Empleado> Empleados { get;set; }
        public DbSet<Ciudad> Ciudades { get;set; }
        public DbSet<KardexModel> Kardex { get;set; }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adscripcion>().ToTable("c_adsc","dbo");
            modelBuilder.Entity<Puesto>().ToTable("c_puesto","dbo");
            modelBuilder.Entity<Ciudad>().ToTable("c_ciudad","dbo");
            modelBuilder.Entity<Estado>().ToTable("c_estado","dbo");
            modelBuilder.Entity<Empleado>().ToTable("empleado","dbo");
            modelBuilder.Entity<KardexModel>().ToTable("kdx_nomb","dbo");

            new AdscripcionConfiguration(modelBuilder.Entity<Adscripcion>());
            new EmpleadoConfiguration(modelBuilder.Entity<Empleado>());
            new KardexConfiguration(modelBuilder.Entity<KardexModel>());
            new CiudadConfiguration(modelBuilder.Entity<Ciudad>());
            new EstadoConfiguration(modelBuilder.Entity<Estado>());
            new PuestoConfiguration(modelBuilder.Entity<Puesto>());
        }
    }
}
