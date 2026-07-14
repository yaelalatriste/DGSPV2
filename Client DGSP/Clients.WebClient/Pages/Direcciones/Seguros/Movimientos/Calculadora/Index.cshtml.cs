using DGSP.Gateway.Proxy.Queries.Catalogos.CTAreas;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMeses;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesGenerales;
using DGSP.Gateway.Proxy.Queries.DGRH.Empleados;
using DGSP.Gateway.Proxy.Queries.ExternalServices;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.Seguros.CJFBDRHDF.Calculadora;
using DGSP.Gateway.Proxy.Queries.Seguros.DGSP.Continuidades;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;
using DGSP.Shared.Contracts.DTOs.DGRH.RH.Empleados;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora;
using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.Seguros.Movimientos.Calculadora
{
    public class IndexModel : PageModel
    {
        private readonly IUsuarioProxy _usuarios;
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IQCTAreaProxy _ctArea;
        private readonly IQCTMesProxy _ctMes;
        private readonly IQCTVariablesGeneralesProxy _variablesGenerales;
        private readonly IQCorreoContinuidadProxy _correos;
        private readonly IQEmpleadoProxy _empleado;
        private readonly IQCalcularPolizaSgmmProxy _calcularSgmm;
        private readonly IEmailProxy _email;

        [BindProperty(SupportsGet = true)]
        public FiltroSGMMDto FiltrosPoliza { get; set; } = new FiltroSGMMDto();

        public decimal UMA { get; set; } = 0;
        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public CTAreaDto Area { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; } = new List<PermisoUsuarioDto>();
        public EmpleadoDto Empleado { get; set; }
        public List<EmpleadoDto> Kardex { get; set; } = new List<EmpleadoDto>();
        public CatalogosSgmmDto Catalogos { get; set; } = new CatalogosSgmmDto();
        public List<PrimaPotenciadaDto> Poliza { get; set; } = new List<PrimaPotenciadaDto>();

        public IndexModel(
            IUsuarioProxy usuarios,
            IModuloProxy modulo,
            IPermisoProxy permisos,
            IQCTAreaProxy ctArea,
            IQCTMesProxy ctMes,
            IQCTVariablesGeneralesProxy variablesGenerales,
            IQCalcularPolizaSgmmProxy calcularSgmm,
            IQCorreoContinuidadProxy correos,
            IQEmpleadoProxy empleado,
            IEmailProxy email)
        {
            _usuarios = usuarios;
            _modulo = modulo;
            _permisos = permisos;
            _variablesGenerales = variablesGenerales;
            _ctArea = ctArea;
            _ctMes = ctMes;
            _correos = correos;
            _empleado = empleado;
            _email = email;
            _calcularSgmm = calcularSgmm;
        }

        public async Task<IActionResult> OnGetAsync(int moduloId, int submoduloId, int opcionId)
        {
            var tienePermiso = await CargarInformacionBaseAsync(moduloId, submoduloId, opcionId);

            if (!tienePermiso)
            {
                return Redirect("/error/denegado");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int moduloId, int submoduloId, int opcionId)
        {
            var tienePermiso = await CargarInformacionBaseAsync(moduloId, submoduloId, opcionId);

            if (!tienePermiso)
            {
                return Redirect("/error/denegado");
            }

            FiltrosPoliza.Anio = (short)DateTime.Now.Year;

            Poliza = await _calcularSgmm.CalcularPolizaSgmmAsync(FiltrosPoliza);

            return Page();
        }

        private async Task<bool> CargarInformacionBaseAsync(int moduloId, int submoduloId, int opcionId)
        {
            string usuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(usuario))
            {
                return false;
            }

            Permisos = await _permisos.GetPermisosByModuloUsuario(usuario, moduloId);

            bool tienePermisoVer = Permisos.Any(p => p.Permiso.Nombre.Equals("Ver"));

            if (!tienePermisoVer)
            {
                return false;
            }

            Modulo = await _modulo.GetModuloByIdAsync(moduloId);
            Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
            Opcion = await _modulo.GetOpcionById(opcionId);
            Area = await _ctArea.GetAreaByIdAsync((int)Submodulo.AreaId);
            UMA = Convert.ToDecimal((await _variablesGenerales.GetVariableGeneralxAnioAbreviacion(DateTime.Now.Year, "UMA")).Valor);
            Catalogos = await _calcularSgmm.ObtenerCatalogosSgmm(ObtieneCatalogos());

            return true;
        }

        private ObtenerCatalogosSgmmDto ObtieneCatalogos()
        {
            return new ObtenerCatalogosSgmmDto((short)DateTime.Now.Year);
        }
    }
}