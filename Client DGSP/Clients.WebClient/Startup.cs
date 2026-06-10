using Api.Gateway.WebClient.Proxy.Config;
using Clients.Services;
using Clients.WebClient.Config;
using Clients.WebClient.Config.Catalogos;
using Clients.WebClient.Config.Estatus;
using Clients.WebClient.Config.Seguros;
using Clients.WebClient.Config.SMedicos;
using DGSP.Gateway.Proxy.Queries.ExternalServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Client.WebClient
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
            //Debug Compilation
            services.AddRazorPages().AddRazorRuntimeCompilation();
            // Proxies
            services.AddSingleton(new ApiGatewayUrl(Configuration.GetValue<string>("ApiGatewayUrl")));
            services.AddSingleton(new UsuariosUrl(Configuration.GetValue<string>("UsuariosUrl")));
            services.AddHttpContextAccessor();

            services.AddProxiesServices(Configuration);
            services.AddProxiesCatalogosQueries(Configuration);
            services.AddProxiesEstatusQueries(Configuration);
            services.AddProxiesSegurosQueries(Configuration);
            services.AddProxiesSegurosCommands(Configuration);
            services.AddProxiesSMedicosQueries(Configuration);
            services.AddProxiesSMedicosCommands(Configuration);
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 2147483648;
            });

            services.AddHttpClient<IEmailProxy, EmailProxy>(client =>
            {
                client.BaseAddress = new Uri("https://cjfappspba.cjf.gob.mx/Gestion2/");
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            });



            // Razor Pages & MVC
            services.AddRazorPages(o => o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));
            services.AddControllers();

            services.AddTransient<PermisosServicios>();

            // Add Cookie Authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie();

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 2147483648;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
