using Api.Gateway.WebClient.Config;
using Api.Gateway.WebClient.Config.Catalogos;
using Api.Gateway.WebClient.Config.Cendis;
using Api.Gateway.WebClient.Config.DG;
using Api.Gateway.WebClient.Config.DGRH;
using Api.Gateway.WebClient.Config.Estatus;
using Api.Gateway.WebClient.Config.Generales;
using Api.Gateway.WebClient.Config.Planeacion;
using Api.Gateway.WebClient.Config.Prestaciones;
using Api.Gateway.WebClient.Config.Seguros;
using Api.Gateway.WebClient.Config.SMedicos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Gateway.WebClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddAppsettingBinding(Configuration).AddProxiesGenerales(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesPlaneacionQueries(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesPlaneacionCommands(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesCatalogosQueries(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesSegurosQueries(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesSegurosCommands(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesPrestacionesQueries(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesPrestacionesCommands(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesSMedicosQueries(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesSMedicosCommands(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesCendisQueries(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesCendisCommands(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesEstatusQueries(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesDGQueries(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesDGCommands(Configuration);
            services.AddAppsettingBinding(Configuration).AddProxiesDGRHQueries(Configuration);


            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                    System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            services.AddControllers();

            var secretKey = Encoding.ASCII.GetBytes(
                Configuration["JWT:Secret"]
            );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
