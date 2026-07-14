using DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTConsultorios;
using DGSP.Gateway.Proxy.Queries.Estatus.NotasTraspaso;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.SMedicos.Medicamentos.NotasTraspaso
{
    public class SalidaModel : PageModel
    {
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IUsuarioProxy _usuario;
        private readonly IQCTConsultoriosProxy _qCTConsultorio;
        private readonly IQCTENotaTraspasoProxy _qEstatusNotas;
        private readonly IQNotasTraspasoProxy _qNotasTraspaso;
        private readonly ICNotasTraspasoProxy _cNotasTraspaso;

        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public List<CTConsultorioDto> Consultorios { get; set; }
        public List<NotaTraspasoDto> NotasTraspaso { get; set; }

        public SalidaModel(IModuloProxy modulo, IPermisoProxy permisos, IQCTConsultoriosProxy qCTConsultorio, IQNotasTraspasoProxy qNotasTraspaso,
            ICNotasTraspasoProxy cNotasTraspaso, IQCTENotaTraspasoProxy qEstatusNotas, IUsuarioProxy usuario)
        {
            _modulo = modulo;
            _permisos = permisos;
            _usuario = usuario;
            _qCTConsultorio = qCTConsultorio;
            _qNotasTraspaso = qNotasTraspaso;
            _cNotasTraspaso = cNotasTraspaso;
            _qEstatusNotas = qEstatusNotas;
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
                NotasTraspaso = await GetNotasTraspaso();
                await CargarCombosAsync();

                return Page(); // ?? importante
            }

            return Redirect("/error/denegado"); // ?? ahora sí funciona
        }

        private async Task CargarCombosAsync()
        {
            Consultorios = await _qCTConsultorio.GetAllConsultoriosAsync() ?? new();
        }

        public async Task<IActionResult> OnPostCreateNotaTraspaso([FromBody] RegistrarNotaTraspasoCommand command)
        {
            string usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = usuario;
            var nota = await _cNotasTraspaso.AddNotaTraspasoAsync(command);
            if (nota != null)
            {
                return new JsonResult(nota);
            }

            return BadRequest();
        }

        private async Task<List<NotaTraspasoDto>> GetNotasTraspaso()
        {
            var notas = await _qNotasTraspaso.GetAllNotasTraspasoAsync();
            foreach (var nt in notas)
            {
                nt.ConsultorioOrigen = await _qCTConsultorio.GetConsultorioByIdAsync(nt.ConsultorioId);
                nt.ConsultorioDestino = await _qCTConsultorio.GetConsultorioByIdAsync(nt.ConsultorioDestinoId);
                nt.Estatus = await _qEstatusNotas.GetEstatusByIdAsync(nt.EstatusId);
                nt.Usuario = await _usuario.GetUsuarioById(nt.UsuarioId);
            }

            return notas;
        }
    }
}
