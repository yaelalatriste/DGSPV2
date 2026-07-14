using DGSP.Gateway.Proxy.Commands.Seguros.Continuidades.ContactosContinuidades;
using DGSP.Gateway.Proxy.Commands.Seguros.Continuidades.Entregables;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTAreas;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTEntregables;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesMedicas;
using DGSP.Gateway.Proxy.Queries.DGRH.Empleados;
using DGSP.Gateway.Proxy.Queries.Estatus.Continuidades;
using DGSP.Gateway.Proxy.Queries.ExternalServices;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.Seguros.CJFBDRHDF.Movimiento;
using DGSP.Gateway.Proxy.Queries.Seguros.DGSP.Continuidades;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.CEntregables;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.Continuidad;
using DGSP.Shared.Contracts.Commands.Seguros.Continuidades.MediosContacto;
using DGSP.Shared.Contracts.DTOs.Catalogos.Generales;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.DGRH.RH.Empleados;
using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.Seguros.Continuidades
{
    public class DetalleModel : PageModel
    {
        private readonly IUsuarioProxy _usuarios;
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IQCTAreaProxy _ctArea;
        private readonly IQCTEntregableProxy _ctEntregable;
        private readonly IQEstatusContinuidadProxy _estatusContinuidad;
        private readonly IQContinuidadProxy _qContinuidad;
        private readonly IQMovimientoSpProxy _movimientoSp;
        private readonly IQOficioContinuidadProxy _qOficios;
        private readonly IQContactoContinuidadProxy _qContactoContinuidad;
        private readonly IQEntregableContinuidadProxy _qEntregables;
        private readonly ICContactoContinuidadProxy _cContactoContinuidad;
        private readonly ICContinuidadProxy _cContinuidad;
        private readonly ICEntregableContinuidadProxy _cEntregable;
        private readonly IQEmpleadoProxy _empleado;
        private readonly IEmailProxy _emailProxy;
        private readonly IQCTVariablesMedicasProxy _variables;
        private readonly IQFlujoContinuidadProxy _flujoContinuidad;
        private readonly IQFlujoEntregableContinuidadProxy _flujoEntregable;
        private readonly IQCorreoContinuidadProxy _correos;
        private readonly IEmailProxy _email;


        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public CTAreaDto Area { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public EstatusContinuidadDto EstatusContinuidad { get; set; }
        public ContinuidadDto Continuidad { get; set; }
        public List<ContactoContinuidadDto> Contactos { get; set; }
        public List<OficioContinuidadDto> Oficios { get; set; }
        public List<EntregableContinuidadDto> EntregablesContinuidad { get; set; }
        public List<CTEntregableDto> Entregables { get; set; }
        public List<CTVariableMedicaDto> TiposContacto { get; set; }
        public List<FlujoContinuidadDto> FlujoContinuidad { get; set; }
        public List<FlujoEntregableContinuidadDto> FlujoEntregableContinuidad { get; set; }
        public EmpleadoDto Empleado { get; set; }
        public List<EmpleadoDto> Kardex { get; set; }

        public DetalleModel(IUsuarioProxy usuarios, IModuloProxy modulo, IPermisoProxy permisos, IQCTAreaProxy ctArea,
            IQEstatusContinuidadProxy estatusContinuidad, IQContinuidadProxy qContinuidad, IQOficioContinuidadProxy qOficios,
            IQEntregableContinuidadProxy qEntregables, IQEmpleadoProxy empleado, IQMovimientoSpProxy movimientoSp,
            ICContinuidadProxy cContinuidad, ICContactoContinuidadProxy cContactoContinuidad, IEmailProxy emailProxy,
            IQContactoContinuidadProxy qContactoContinuidad, IQCTVariablesMedicasProxy variables, IQFlujoContinuidadProxy flujoContinuidad,
            IQFlujoEntregableContinuidadProxy flujoEntregable, IQCTEntregableProxy ctEntregable, ICEntregableContinuidadProxy cEntregable,
            IQCorreoContinuidadProxy correos, IEmailProxy email)
        {
            _usuarios = usuarios;
            _modulo = modulo;
            _permisos = permisos;
            _correos = correos;
            _email = email;
            _ctArea = ctArea;
            _variables = variables;
            _emailProxy = emailProxy;
            _ctEntregable = ctEntregable;
            _cEntregable = cEntregable;
            _flujoContinuidad = flujoContinuidad;
            _flujoEntregable = flujoEntregable;
            _cContactoContinuidad = cContactoContinuidad;
            _movimientoSp = movimientoSp;
            _estatusContinuidad = estatusContinuidad;
            _qContinuidad = qContinuidad;
            _cContinuidad = cContinuidad;
            _qContactoContinuidad = qContactoContinuidad;
            _qOficios = qOficios;
            _qEntregables = qEntregables;
            _empleado = empleado;
        }

        public async Task OnGet(int moduloId, int submoduloId, int opcionId, int id)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Permisos = await _permisos.GetPermisosByModuloUsuario(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, moduloId);
            if (Permisos.Where(p => p.Permiso.Nombre.Equals("Ver")).Count() != 0)
            {
                Modulo = await _modulo.GetModuloByIdAsync(moduloId);
                Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
                Opcion = await _modulo.GetOpcionById(opcionId);
                Area = await _ctArea.GetAreaByIdAsync((int)Submodulo.AreaId);
                Continuidad = await _qContinuidad.GetContinuidadById(id);
                EstatusContinuidad = await _estatusContinuidad.GetEstatusById(Continuidad.EstatusId);
                Oficios = await ObtenerOficiosContinuidad(id);
                Contactos = await ObtenerContactosContinuidad(id);
                EntregablesContinuidad = await ObtenerEntregablesContinuidad(id);
                Empleado = await _empleado.GetEmpleadoByExpediente(Continuidad.Expediente);
                Kardex = await _empleado.GetMovimientosEmpleado(Continuidad.Expediente);
                FlujoContinuidad = await _flujoContinuidad.GetEstatusConsecutivoNota(Continuidad.EstatusId);
                FlujoEntregableContinuidad = await _flujoEntregable.GetEstatusConsecutivoNota(Continuidad.EstatusId);
                Entregables = await ObtenerEntregables(Continuidad.EstatusId);
                TiposContacto = await _variables.GetVariablesByCategoriaAsync("TipoContacto");
            }
            else
            {
                Response.Redirect("/error/denegado");
            }
        }

        private async Task<List<OficioContinuidadDto>> ObtenerOficiosContinuidad(int continuidadId)
        {
            var oficios = await _qOficios.GetOficiosByContinuidades(continuidadId);
            foreach (var of in oficios)
            {
                of.ServidorPublico = await _empleado.GetEmpleadoByExpediente(of.Expediente);
                of.UsuarioRegistro = await _empleado.GetEmpleadoByExpediente(of.RegistroMovimiento);
                of.Movimiento = await _movimientoSp.GetMovimientoById(of.TipoMovimiento);
            }
            return oficios;
        }

        private async Task<List<ContactoContinuidadDto>> ObtenerContactosContinuidad(int continuidadId)
        {
            var contactos = await _qContactoContinuidad.GetContactoByContinuidadAsync(continuidadId);
            foreach (var cn in contactos)
            {
                cn.TipoContacto = await _variables.GEtVariableByIdAsync(cn.TipoId);
            }
            return contactos;
        }

        private async Task<List<EntregableContinuidadDto>> ObtenerEntregablesContinuidad(int id)
        {
            var entregables = await _qEntregables.GetEntregablesByContinuidad(id);
            foreach (var en in entregables)
            {
                en.Entregable = await _ctEntregable.GetEntregableByIdAsync(en.EntregableId);
                en.Usuario = await _usuarios.GetUsuarioById(en.UsuarioId);
            }
            return entregables;
        }

        private async Task<List<CTEntregableDto>> ObtenerEntregables(int estatus)
        {
            var flujoEntregables = (await _flujoEntregable.GetEstatusConsecutivoNota(estatus)).Select(fe => fe.EntregableId).ToList();
            var entregables = (await _ctEntregable.GetAllEntregablesAsync()).Where(e => flujoEntregables.Contains(e.Id)).ToList();

            return entregables.Where(e => flujoEntregables.Contains(e.Id)).ToList();
        }

        public async Task<IActionResult> OnGetRastrearOficio(int continuidadId)
        {
            var oficio = await _movimientoSp.GetMovimientoSpByContinuidad(continuidadId);

            return new JsonResult(oficio);
        }

        public async Task<IActionResult> OnGetExisteContinuidad(int expediente, string fechaBaja)
        {
            var continuidades = await _qContinuidad.GetAllContinuidades();
            var continuidad = continuidades.Where(c => c.Expediente == expediente && c.FechaBaja.GetValueOrDefault().ToString("yyyy-MM-dd").Equals(fechaBaja)).ToList();
            if (continuidad.Count() != 0)
            {
                return new JsonResult(null);
            }

            return new JsonResult(continuidad);
        }

        public async Task<IActionResult> OnPutActualizarContinuidad([FromBody] ActualizarContinuidadCommand command)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = Usuario;
            var update = await _cContinuidad.ActualizarContinuidadAsync(command);

            if (update != null)
            {
                return new JsonResult(update);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPutEstatusContinuidad([FromBody] EstatusContinuidadCommand command)
        {
            var continuidad = await _qContinuidad.GetContinuidadById(command.Id);
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = Usuario;
            if (command.Corregir)
            {
                var update = await _cContinuidad.EstatusContinuidadAsync(command);

                if (update != null)
                {
                    var estatus = await _estatusContinuidad.GetEstatusById(command.EstatusId);
                    return new JsonResult(update);
                }
            }
            else if (await VerificaCargaEntregables(command.Id) && continuidad.FechaBaja.HasValue)
            {
                var update = await _cContinuidad.EstatusContinuidadAsync(command);

                if (update != null)
                {
                    var estatus = await _estatusContinuidad.GetEstatusById(command.EstatusId);
                    return new JsonResult(update);
                }
            }
            else
            {
                return new JsonResult(null);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostCreateContacto([FromBody] RegistrarContactoContinuidadCommand command)
        {
            var contacto = await _cContactoContinuidad.RegistrarContactoContinuidadAsync(command);
            if (contacto != null)
            {
                return new JsonResult(contacto);
            }
            return BadRequest();
        }

        public async Task<IActionResult> OnPutUpdateContacto([FromBody] ActualizarContactoContinuidadCommand command)
        {
            var contacto = await _cContactoContinuidad.ActualizarContactoContinuidadAsync(command);
            if (contacto != null)
            {
                return new JsonResult(contacto);
            }
            return BadRequest();
        }

        public async Task<IActionResult> OnPostCreateEntregable([FromForm] RegistrarEntregableContinuidadCommand command)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = Usuario;
            var entregable = await _cEntregable.RegistrarEntregableContinuidadAsync(command);

            if (entregable != null)
            {
                ActualizarContinuidadCommand update = new ActualizarContinuidadCommand();
                update.Id = entregable.ContinuidadId;
                update.UsuarioId = entregable.UsuarioId;
                update.FechaEnvioSP = command.FechaEnvioSP;
                update.FechaLimitePago = command.FechaLimitePago;
                update.Importe = command.Importe;
                var updateContinuidad = await _cContinuidad.ActualizarContinuidadAsync(update);
                if (updateContinuidad != null)
                {
                    return new JsonResult(entregable);
                }

            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPutUpdateEntregable([FromForm] ActualizarEntregableContinuidadCommand command)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = Usuario;
            var entregable = await _cEntregable.ActualizarEntregableContinuidadAsync(command);

            if (entregable != null)
            {
                ActualizarContinuidadCommand update = new ActualizarContinuidadCommand();
                update.Id = entregable.ContinuidadId;
                update.UsuarioId = entregable.UsuarioId;
                update.FechaEnvioSP = command.FechaEnvioSP;
                update.FechaLimitePago = command.FechaLimitePago;
                update.Importe = command.Importe;
                var updateContinuidad = await _cContinuidad.ActualizarContinuidadAsync(update);
                if (updateContinuidad != null)
                {
                    return new JsonResult(entregable);
                }
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPutDeleteEntregable([FromForm] EliminarEntregableContinuidadCommand command)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = Usuario;
            var entregable = await _cEntregable.EliminarEntregableContinuidadAsync(command);

            if (entregable != null)
            {
                return new JsonResult(entregable);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnGetVisualizarEntregable(int entregableId)
        {
            string path = await _qEntregables.VisualizarEntregable(entregableId);
            Stream stream = System.IO.File.Open(path, FileMode.Open);
            return File(stream, "application/pdf");
        }

        public async Task<JsonResult> OnPostSendEmailReferenciaPago(int continuidadId)
        {
            try
            {
                var emailRequest = await _correos.EnviarCorreoReferencia(continuidadId);
                var email = await _email.EnviarCorreoAsync(emailRequest);

                return new JsonResult(email);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"Ocurrió un error: {ex.Message}" });
            }
        }

        public async Task<JsonResult> OnPostSendEmailPoliza(int continuidadId)
        {
            try
            {
                var emailRequest = await _correos.EnviarCorreoPoliza(continuidadId);
                var email = await _email.EnviarCorreoAsync(emailRequest);

                return new JsonResult(email);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"Ocurrió un error: {ex.Message}" });
            }
        }
        //Metodos para validar
        private async Task<bool> VerificaCargaEntregables(int continuidadId)
        {
            var continuidad = await _qContinuidad.GetContinuidadById(continuidadId);
            var entregables = await _qEntregables.GetEntregablesByContinuidad(continuidadId);
            var flujoEntregables = (await _flujoEntregable.GetEstatusConsecutivoNota(continuidad.EstatusId)).Select(e => e.EntregableId).ToList();
            if (entregables.Where(e => flujoEntregables.Contains(e.EntregableId)).Count() != 0 || flujoEntregables.Count() == 0)
            {
                return true;
            }
            return false;
        }
    }
}
