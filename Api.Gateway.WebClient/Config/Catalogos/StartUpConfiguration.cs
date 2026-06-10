using Api.Gateway.Proxies.Catalogos.CTActividades.Queries;
using Api.Gateway.Proxies.Catalogos.CTArchivos;
using Api.Gateway.Proxies.Catalogos.CTAreas.Queries;
using Api.Gateway.Proxies.Catalogos.CTArticulos.Queries;
using Api.Gateway.Proxies.Catalogos.CTAsuntos.Queries;
using Api.Gateway.Proxies.Catalogos.CTCategorias.Queries;
using Api.Gateway.Proxies.Catalogos.CTCendis.Queries;
using Api.Gateway.Proxies.Catalogos.CTEntregables.Queries;
using Api.Gateway.Proxies.Catalogos.CTExpedientes.Queries;
using Api.Gateway.Proxies.Catalogos.CTIncidencias;
using Api.Gateway.Proxies.Catalogos.CTMeses.Queries;
using Api.Gateway.Proxies.Catalogos.CTMovimientos.Queries;
using Api.Gateway.Proxies.Catalogos.CTPProcesos.Queries;
using Api.Gateway.Proxies.Catalogos.CTUnidades.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.WebClient.Config.Catalogos
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesCatalogosQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpClient<IQCTPProcesoProxy, QCTPProcesoProxy>();
            service.AddHttpClient<IQCTAreaProxy, QCTAreaProxy>();
            service.AddHttpClient<IQCTActividadProxy, QCTActividadProxy>();
            service.AddHttpClient<IQCTMesProxy, QCTMesProxy>();
            service.AddHttpClient<IQCTEntregableProxy, QCTEntregableProxy>();
            service.AddHttpClient<IQCTArticuloProxy, QCTArticuloProxy>();
            service.AddHttpClient<IQCTCategoriaProxy, QCTCategoriaProxy>();
            service.AddHttpClient<IQCTUnidadProxy, QCTUnidadProxy>();
            service.AddHttpClient<IQCTIncidenciaProxy, QCTIncidenciaProxy>();
            service.AddHttpClient<IQCTCendisProxy, QCTCendisProxy>();
            service.AddHttpClient<IQCTAsuntoProxy, QCTAsuntoProxy>();
            service.AddHttpClient<IQCTExpedienteProxy, QCTExpedienteProxy>();
            service.AddHttpClient<IQCTArchivoProxy, QCTArchivoProxy>();
            service.AddHttpClient<IQCTMovimientoProxy, QCTMovimientoProxy>();

            return service;
        }
    }
}
