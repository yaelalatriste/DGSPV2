using DGSP.Gateway.Proxy.Commands.Catalogos.SMedicos.Consultorios;
using DGSP.Gateway.Proxy.Commands.Catalogos.SMedicos.Medicamentos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTAreas;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTConsultorios;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTEntregables;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMedicamentos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMeses;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposInsumos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposMovimientos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesMedicas;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.WebClient.Config.Catalogos
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesCatalogosQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpContextAccessor();

            service.AddHttpClient<IQCTAreaProxy, QCTAreaProxy>();
            service.AddHttpClient<IQCTMesProxy, QCTMesProxy>();
            service.AddHttpClient<IQCTConsultoriosProxy, QCTConsultoriosProxy>();
            service.AddHttpClient<IQCTMedicamentosProxy, QCTMedicamentosProxy>();
            service.AddHttpClient<IQCTTipoMovimientoProxy, QCTTipoMovimientoProxy>();
            service.AddHttpClient<IQCTTipoInsumoProxy, QCTTipoInsumoProxy>();
            service.AddHttpClient<IQCTVariablesMedicasProxy, QCTVariablesMedicasProxy>();
            service.AddHttpClient<IQCTEntregableProxy, QCTEntregableProxy>();            
            service.AddHttpClient<ICCTMedicamentoProxy, CCTMedicamentoProxy>();
            service.AddHttpClient<ICCTConsultorioProxy, CCTConsultorioProxy>();

            return service;
        }

    }
}
