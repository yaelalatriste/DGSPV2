using DGSP.Gateway.Proxy.Queries.Catalogos.CTAreas;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMeses;
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
using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Catalogos;
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
        private readonly IQCorreoContinuidadProxy _correos;
        private readonly IQEmpleadoProxy _empleado;
        private readonly IQCalcularPolizaSgmmProxy _calcularSgmm;
        private readonly IEmailProxy _email;

        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public CTAreaDto Area { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public EmpleadoDto Empleado { get; set; }
        public List<EmpleadoDto> Kardex { get; set; }
        public CatalogosSgmmDto Catalogos { get; set; } = new CatalogosSgmmDto();

        public IndexModel(IUsuarioProxy usuarios, IModuloProxy modulo, IPermisoProxy permisos, IQCTAreaProxy ctArea, IQCTMesProxy ctMes,
            IQCalcularPolizaSgmmProxy calcularSgmm, IQCorreoContinuidadProxy correos, IQEmpleadoProxy empleado, IEmailProxy email)
        {
            _usuarios = usuarios;
            _modulo = modulo;
            _permisos = permisos;
            _ctArea = ctArea;
            _ctMes = ctMes;
            _correos = correos;
            _empleado = empleado;
            _email = email;
            _calcularSgmm = calcularSgmm;
        }


        public async Task OnGet(int moduloId, int submoduloId, int opcionId)
        {
            string usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int anio = DateTime.Now.Year;
            Permisos = await _permisos.GetPermisosByModuloUsuario(usuario, moduloId);
            if (Permisos.Where(p => p.Permiso.Nombre.Equals("Ver")).Count() != 0)
            {
                Modulo = await _modulo.GetModuloByIdAsync(moduloId);
                Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
                Opcion = await _modulo.GetOpcionById(opcionId);
                Area = await _ctArea.GetAreaByIdAsync((int)Submodulo.AreaId);
                Catalogos = await _calcularSgmm.ObtenerCatalogosSgmm(ObtieneCatalogos());
            }
            else
            {
                Response.Redirect("/error/denegado");
            }
        }

        private ObtenerCatalogosSgmmDto ObtieneCatalogos()
        {
            return new ObtenerCatalogosSgmmDto((short)DateTime.Now.Year);
        }
    }
}
