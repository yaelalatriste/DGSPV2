using DGSP.Infraestructure.Data.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DGSP.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var bdCatalogos = configuration.GetConnectionString("BDCatalogos");
            var bdSMedicos = configuration.GetConnectionString("BDSIO");

            services.AddDbContext<CatalogoDbContext>(options => options.UseSqlServer(bdCatalogos));

            services.Scan(scan => scan
             .FromAssemblyOf<CatalogoDbContext>()
             .AddClasses()
             .AsImplementedInterfaces()
             .WithScopedLifetime());

            return services;
        }
    }
}
