using DGSP.Gateway.Proxy.Commands.Seguros.Continuidades.ContactosContinuidades;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTAreas;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMeses;
using DGSP.Gateway.Proxy.Queries.DGRH.Empleados;
using DGSP.Gateway.Proxy.Queries.Estatus.Continuidades;
using DGSP.Gateway.Proxy.Queries.ExternalServices;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.Seguros.DGSP.Continuidades;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.Continuidad;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;
using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.Seguros.Continuidades
{
    public class IndexModel : PageModel
    {
        private readonly IUsuarioProxy _usuarios;
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IQEstatusContinuidadProxy _estatusContinuidad;
        private readonly IQCTAreaProxy _ctArea;
        private readonly IQCTMesProxy _ctMes;
        private readonly IQContinuidadProxy _qContinuidad;
        private readonly ICContinuidadProxy _cContinuidad;
        private readonly IQCorreoContinuidadProxy _correos;
        private readonly IQEmpleadoProxy _empleado;
        private readonly IEmailProxy _email;

        [BindProperty(SupportsGet = true)]
        public int EstatusId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Expediente { get; set; }
        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public CTAreaDto Area { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public List<EstatusContinuidadDto> Estatus { get; set; }
        public List<EstatusContinuidadDto> FiltrosEstatus { get; set; } = new List<EstatusContinuidadDto>();
        public List<ContinuidadDto> Continuidades { get; set; } = new List<ContinuidadDto>();
        public List<ContinuidadDto> continuidades { get; set; } = new List<ContinuidadDto>();

        public IndexModel(IUsuarioProxy usuarios, IModuloProxy modulo, IPermisoProxy permisos, IQEstatusContinuidadProxy estatusContinuidad,
            IQCTAreaProxy ctArea, IQCTMesProxy ctMes, IQContinuidadProxy qContinuidad, ICContinuidadProxy cContinuidad, IEmailProxy email,
            IQCorreoContinuidadProxy correos, IQEmpleadoProxy empleado)
        {
            _usuarios = usuarios;
            _modulo = modulo;
            _permisos = permisos;
            _empleado = empleado;
            _estatusContinuidad = estatusContinuidad;
            _email = email;
            _correos = correos;
            _ctArea = ctArea;
            _ctMes = ctMes;
            _qContinuidad = qContinuidad;
            _cContinuidad = cContinuidad;
        }

        public async Task OnGet(int moduloId, int submoduloId, int opcionId)
        {
            string usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Permisos = await _permisos.GetPermisosByModuloUsuario(usuario, moduloId);
            if (Permisos.Where(p => p.Permiso.Nombre.Equals("Ver")).Count() != 0)
            {
                Modulo = await _modulo.GetModuloByIdAsync(moduloId);
                Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
                Opcion = await _modulo.GetOpcionById(opcionId);
                Area = await _ctArea.GetAreaByIdAsync((int)Submodulo.AreaId);
                Estatus = await _estatusContinuidad.GetAllEstatus();
                continuidades = await _qContinuidad.GetAllContinuidades();
                if (Expediente != 0) 
                {
                    Continuidades = await ContinuidadByExpediente(Expediente);
                }
                else
                {
                    Continuidades = await CargarContinuidades((EstatusId != 0 ? EstatusId : Estatus.First().Id));
                }
            }
            else
            {
                Response.Redirect("/error/denegado");
            }
        }

        private async Task<List<ContinuidadDto>> CargarContinuidades(int estatus)
        {
            var continuidades = await _qContinuidad.GetContinuidadesByEstatus(estatus);

            foreach (var cn in continuidades)
            {
                cn.Estatus = await _estatusContinuidad.GetEstatusById(cn.EstatusId);
                cn.Usuario = await _usuarios.GetUsuarioById(cn.UsuarioId);
                cn.ServidorPublico = await _empleado.GetEmpleadoByExpediente(cn.Expediente);
            }

            return continuidades;
        }
        private async Task<List<ContinuidadDto>> ContinuidadByExpediente(int expediente)
        {
            var continuidad = (await _qContinuidad.GetAllContinuidades()).Where(c => c.Expediente == expediente).ToList();

            foreach (var cn in continuidad)
            {
                cn.Estatus = await _estatusContinuidad.GetEstatusById(cn.EstatusId);
                cn.Usuario = await _usuarios.GetUsuarioById(cn.UsuarioId);
                cn.ServidorPublico = await _empleado.GetEmpleadoByExpediente(cn.Expediente);
            }
            return continuidad;
        }
        
        public async Task<IActionResult> OnPostCreateContinuidad([FromBody] RegistrarContinuidadCommand command)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = Usuario;
            var create = await _cContinuidad.RegistrarContinuidadAsync(command);

            if (create != null)
            {
                return new JsonResult(create);
            }

            return BadRequest();
        }

        public async Task<JsonResult> OnPostSendEmailMovimientos()
        {
            try
            {
                var emailRequest = await _correos.EnviarCorreoMovimientos();
                var email = await _email.EnviarCorreoAsync(emailRequest);

                return new JsonResult(email);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"Ocurrió un error: {ex.Message}" });
            }
        }

    }
}
