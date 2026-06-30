using Catalogos.Persistence.Database;
using DGSP.API.Config.Catalogos;
using DGSP.API.Config.DGRH;
using DGSP.API.Config.Estatus;
using DGSP.API.Config.Modulos;
using DGSP.API.Config.Permisos;
using DGSP.API.Config.Seguros;
using DGSP.API.Config.SMedicos;
using DGSP.Module.DGRH.Persistence;
using DGSP.Module.Estatus.Persistence;
using DGSP.Module.Seguros.Persistence;
using DGSP.Module.SMedicos.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Modulos.Persistence.Database;
using Permisos.Persistence.Database;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var secretKey = Encoding.UTF8.GetBytes(configuration["JWT:Secret"]);

services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

// DbContext's 
services.AddDbContext<SMedicosDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SMedicosConnection")));
services.AddDbContext<SegurosDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SegurosConnection")));
services.AddDbContext<EstatusDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("EstatusConnection")));
services.AddDbContext<ModulosDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SistemaConnection")));
services.AddDbContext<PermisosDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SistemaConnection")));
services.AddDbContext<SiacomDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SiacomConnection")));
services.AddDbContext<CatalogoDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CatalogosConnection")));
services.AddDbContext<SegurosSGMMContext>(options => options.UseSqlServer(configuration.GetConnectionString("SegurosSGMMConnection")));
services.AddDbContext<DGRHDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DGRHConnection")));

//Services
services.AddSMedicosQueries(configuration);
services.AddSegurosQueries(configuration);
services.AddCatalogosQueries(configuration);
services.AddEstatusQueries(configuration);
services.AddModulosQueries(configuration);
services.AddPermisosQueries(configuration);
services.AddDGRHQueries(configuration);
//builder.Services.AddHttpClient<IEmailService, EmailService>(client =>
//{
//    client.BaseAddress = new Uri("https://cjfappspba.cjf.gob.mx/Gestion2/");
//    client.DefaultRequestHeaders.Accept.Add(
//        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
//    );
//});

//MediaTr
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(DGSP.Module.Catalogos.Application.AssemblyMarker).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DGSP.Module.Permisos.Application.AssemblyMarker).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DGSP.Module.SMedicos.Application.AssemblyMarker).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DGSP.Module.Seguros.Application.AssemblyMarker).Assembly);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Swagger (para probar)
services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DGSP API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa el token JWT con el prefijo Bearer. Ej: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DGSP API v1");
        c.EnablePersistAuthorization();
    });
}

// Aquí podrías meter UseAuthentication/UseAuthorization si ya lo tienes
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();