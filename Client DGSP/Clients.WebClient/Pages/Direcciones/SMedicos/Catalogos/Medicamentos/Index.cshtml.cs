using DGSP.Gateway.Proxy.Commands.Catalogos.SMedicos.Medicamentos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMedicamentos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposInsumos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesMedicas;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using DGSP.Shared.Contracts.Commands.Catalogos.SMedicos.Medicamentos;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.SMedicos.Catalogos.Medicamentos
{
    public class IndexModel : PageModel
    {
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IUsuarioProxy _usuario;
        private readonly IQCTMedicamentosProxy _qCtMedicamentos;
        private readonly ICCTMedicamentoProxy _cCtMedicamentos;
        private readonly IQCTVariablesMedicasProxy _qVariables;
        private readonly IQCTTipoInsumoProxy _qCTTipoInsumo;

        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public List<CTMedicamentoDto> Medicamentos { get; set; }
        public List<CTTipoInsumoDto> TiposInsumos { get; set; } = new();
        public List<CTVariableMedicaDto> TiposEnvase { get; set; } = new();
        public List<CTVariableMedicaDto> UnidadesContenido { get; set; } = new();

        public IndexModel(IModuloProxy modulo, IPermisoProxy permisos, IUsuarioProxy usuario, IQCTMedicamentosProxy qCtMedicamentos, ICCTMedicamentoProxy cCtMedicamentos,
            IQCTTipoInsumoProxy qCTTipoInsumo, IQCTVariablesMedicasProxy qVariables)
        {
            _modulo = modulo;
            _permisos = permisos;
            _usuario = usuario;
            _qCTTipoInsumo = qCTTipoInsumo;
            _qVariables = qVariables;
            _qCtMedicamentos = qCtMedicamentos;
            _cCtMedicamentos = cCtMedicamentos;
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
                Medicamentos = await ObtenerMedicamentos();
                await CargarCombosAsync();

                return Page();
            }

            return Redirect("/error/denegado");
        }

        private async Task<List<CTMedicamentoDto>> ObtenerMedicamentos()
        {
            var medicamentos = await _qCtMedicamentos.GetMedicamentosByAnioAsync(DateTime.Now.Year);
            foreach (var m in medicamentos)
            {
                m.TipoInsumo = await _qCTTipoInsumo.GetTipoInsumoByIdAsync(m.TipoInsumoId);
                m.TipoEnvase = await _qVariables.GEtVariableByIdAsync(m.TipoEnvaseId);
                m.Usuario = await _usuario.GetUsuarioById(m.UsuarioId);
            }
            return medicamentos;
        }

        private async Task CargarCombosAsync()
        {
            TiposInsumos = await _qCTTipoInsumo.GetAllTiposInsumosAsync() ?? new();
            TiposEnvase = await _qVariables.GetVariablesByCategoriaAsync("TipoEnvase");
            UnidadesContenido = await _qVariables.GetVariablesByCategoriaAsync("UnidadContenido");
        }

        public async Task<IActionResult> OnPostCreateMedicamento([FromBody] RegistrarCTMedicamentoCommand command)
        {
            string usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = usuario;
            var medicamento = await _cCtMedicamentos.RegistrarMedicamentoAsync(command);
            if (medicamento != null)
            {
                return new JsonResult(medicamento);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPutUpdateMedicamento([FromBody] ActualizarCTMedicamentoCommand command)
        {
            string usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = usuario;
            var medicamento = await _cCtMedicamentos.ActualizarMedicamentoAsync(command);
            if (medicamento != null)
            {
                return new JsonResult(medicamento);
            }

            return BadRequest();
        }
    }
}
