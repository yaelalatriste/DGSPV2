using DGSP.Gateway.Proxy.Commands.Catalogos.SMedicos.Consultorios;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTConsultorios;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposInsumos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesMedicas;
using DGSP.Gateway.Proxy.Queries.DGRH.Empleados;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Consultorios;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.SMedicos.Catalogos.Consultorios
{
    public class IndexModel : PageModel
    {
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IQEmpleadoProxy _empleado;
        private readonly IQCTConsultoriosProxy _qCtConsultorios;
        private readonly ICCTConsultorioProxy _cCtConsultorios;
        private readonly IQCTVariablesMedicasProxy _qVariables;
        private readonly IQCTTipoInsumoProxy _qCTTipoInsumo;

        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public List<CTConsultorioDto> Consultorios { get; set; }
        public List<CTTipoInsumoDto> TiposInsumos { get; set; } = new();
        public List<CTVariableMedicaDto> TiposEnvase { get; set; } = new();
        public List<CTVariableMedicaDto> UnidadesContenido { get; set; } = new();

        public IndexModel(IModuloProxy modulo, IPermisoProxy permisos, IQEmpleadoProxy empleado, IQCTConsultoriosProxy qCtConsultorios, ICCTConsultorioProxy cCtConsultorios,
            IQCTTipoInsumoProxy qCTTipoInsumo, IQCTVariablesMedicasProxy qVariables)
        {
            _modulo = modulo;
            _permisos = permisos;
            _empleado = empleado;
            _qCTTipoInsumo = qCTTipoInsumo;
            _qVariables = qVariables;
            _qCtConsultorios = qCtConsultorios;
            _cCtConsultorios = cCtConsultorios;
        }

        public async Task<IActionResult> OnGet(int moduloId, int submoduloId, int opcionId)
        {
            string usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Permisos = await _permisos.GetPermisosByModuloUsuario(usuario, moduloId);

            if (Permisos.Any(p => p.Permiso.Nombre == "Ver"
                                && p.Submodulo.Id == submoduloId
                                && p.OpcionId == opcionId))
            {
                Modulo = await _modulo.GetModuloByIdAsync(moduloId);
                Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
                Opcion = await _modulo.GetOpcionById(opcionId);
                Consultorios = await ObtenerConsultorios();

                return Page();
            }

            return Redirect("/error/denegado");
        }

        private async Task<List<CTConsultorioDto>> ObtenerConsultorios()
        {
            var Consultorios = await _qCtConsultorios.GetAllConsultoriosAsync();
            foreach (var m in Consultorios)
            {
                if (m.ExpedienteResponsable != 0)
                {
                    m.ServidorPublico = await _empleado.GetEmpleadoByExpediente(m.ExpedienteResponsable);
                }
            }
            return Consultorios;
        }

        public async Task<IActionResult> OnGetEmpleadoByExpediente(int expediente)
        {
            var empleado = await _empleado.GetEmpleadoByExpediente(expediente);
            var ultimoMovimiento = (await _empleado.GetMovimientosEmpleado(expediente)).First();
            empleado.Puesto = ultimoMovimiento.Puesto;

            if (empleado != null)
            {
                return new JsonResult(empleado);
            }
            return new JsonResult(null);
        }

        public async Task<IActionResult> OnPutUpdateConsultorio([FromBody] ActualizarCTConsultorioCommand command)
        {
            var Consultorio = await _cCtConsultorios.ActualizarConsultorioAsync(command);
            if (Consultorio != null)
            {
                return new JsonResult(Consultorio);
            }

            return BadRequest();
        }
    }
}
