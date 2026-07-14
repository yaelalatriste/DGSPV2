using DGSP.Gateway.Proxy.Queries.Catalogos.CTAreas;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMeses;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.SMedicos.Reportes;
using DGSP.Gateway.Proxy.Queries.SMedicos.Siacom.Catalogos;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Catalogos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Dashboards;
using DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Reportes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.SMedicos.Reportes
{
    public class IndexModel : PageModel
    {
        private readonly IUsuarioProxy _usuarios;
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IQCTAreaProxy _ctareas;
        private readonly IQCTMesProxy _ctmeses;
        private readonly IQSMReporteProxy _reportes;
        private readonly IQCTTipoServicioProxy _ctTipoServicio;
        private readonly IQCTTipoConsultaProxy _ctTipoConsulta;
        private readonly IQCTTipoConsultaDetallesProxy _ctTipoConsultaDetalle;



        [BindProperty(SupportsGet = true)]
        public int FAnios { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<int> FMeses { get; set; }
        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public CTAreaDto Area { get; set; }
        public List<CTTipoConsultaDto> CTTiposConsultas { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public List<int> Anios { get; set; } = new List<int>();
        public List<CTMesDto> Meses { get; set; } = new List<CTMesDto>();
        public DashboardConsultasResponseDto Dashboard { get; set; } = new DashboardConsultasResponseDto();
        public List<RConsultaDto> ConsultasMedicas { get; set; } = new List<RConsultaDto>();

        public IndexModel(IUsuarioProxy usuarios, IModuloProxy modulo, IPermisoProxy permisos, IQCTAreaProxy ctareas, IQCTMesProxy ctmeses,
                          IQSMReporteProxy reportes, IQCTTipoServicioProxy ctTipoServicio, IQCTTipoConsultaProxy ctTipoConsulta,
                          IQCTTipoConsultaDetallesProxy ctTipoConsultaDetalle)
        {
            _usuarios = usuarios;
            _modulo = modulo;
            _permisos = permisos;
            _ctareas = ctareas;
            _ctmeses = ctmeses;
            _reportes = reportes;
            _ctTipoServicio = ctTipoServicio;
            _ctTipoConsulta = ctTipoConsulta;
            _ctTipoConsultaDetalle = ctTipoConsultaDetalle;
        }

        public async Task OnGet(int moduloId, int submoduloId, int area)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Permisos = await _permisos.GetPermisosByModuloUsuario(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, moduloId);
            if (Permisos.Where(p => p.Permiso.Nombre.Equals("Ver")).Count() != 0)
            {
                Modulo = await _modulo.GetModuloByIdAsync(moduloId);
                Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
                Area = await _ctareas.GetAreaByIdAsync(area);
                Meses = await _ctmeses.GetAllMesesAsync();
                Anios = await _reportes.GetAniosOfConsultas();
                CTTiposConsultas = await _ctTipoConsulta.GetAllTiposConsultas();

                if (FAnios != 0 && FMeses.Count() != 0)
                {
                    FiltrosSmDto filtros = new FiltrosSmDto();
                    filtros.Anios = FAnios;
                    filtros.Meses = FMeses;
                    Dashboard = await _reportes.ObtenerDashboard(filtros);
                }
            }
            else
            {
                Response.Redirect("/error/denegado");
            }
        }
    }
}
