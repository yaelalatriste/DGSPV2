using DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.Entradas;
using DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.Salidas;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Entradas;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Movimientos;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Salidas;
using DGSP.Gateway.Proxy.Queries.SMedicos.Reportes;
using DGSP.Gateway.Proxy.Queries.SMedicos.Siacom.Catalogos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.WebClient.Config.SMedicos
{
    public static class StartUpConfiguration
    {
        public static IServiceCollection AddProxiesSMedicosQueries(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpContextAccessor();
            service.AddHttpClient<IQSMReporteProxy, QSMReporteProxy>();
            service.AddHttpClient<IQCTTipoConsultaDetallesProxy, QCTTipoConsultaDetallesProxy>();
            service.AddHttpClient<IQCTTipoConsultaProxy, QCTTipoConsultaProxy>();
            service.AddHttpClient<IQCTTipoServicioProxy, QCTTipoServicioProxy>();
            service.AddHttpClient<IQCTConsultorioProxy, QCTConsultorioProxy>();
            service.AddHttpClient<IQInventariosProxy, QInventariosProxy>();
            service.AddHttpClient<IQMovimientosProxy, QMovimientosProxy>();
            service.AddHttpClient<IQNotasTraspasoProxy, QNotasTraspasoProxy>();
            service.AddHttpClient<IQDetalleNotasTraspasoProxy, QDetalleNotasTraspasoProxy>();
            service.AddHttpClient<IQSalidaMedicamentoProxy, QSalidaMedicamentoProxy>();
            service.AddHttpClient<IQSalidaMedicamentoDetalleProxy, QSalidaMedicamentoDetalleProxy>();

            return service;
        }

        public static IServiceCollection AddProxiesSMedicosCommands(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpContextAccessor();
            service.AddHttpClient<ICInventariosProxy, CInventariosProxy>();
            service.AddHttpClient<ICNotasTraspasoProxy, CNotasTraspasoProxy>();
            service.AddHttpClient<ICDetalleNotasTraspasoProxy, CDetalleNotasTraspasoProxy>();
            service.AddHttpClient<ICSalidaMedicamentoProxy, CSalidaMedicamentoProxy>();
            service.AddHttpClient<ICSalidaMedicamentoDetalleProxy, CSalidaMedicamentoDetalleProxy>();

            return service;
        }

    }
}
