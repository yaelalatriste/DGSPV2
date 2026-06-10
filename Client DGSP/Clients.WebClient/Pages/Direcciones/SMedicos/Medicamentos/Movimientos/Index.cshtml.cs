using DGSP.Gateway.Proxy.Queries.Catalogos.CTConsultorios;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMedicamentos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposInsumos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesMedicas;
using DGSP.Gateway.Proxy.Queries.Estatus.NotasTraspaso;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Entradas;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Movimientos;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Movimientos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.SMedicos.Medicamentos.Movimientos
{
    public class IndexModel : PageModel
    {
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IUsuarioProxy _usuario;
        private readonly IQCTConsultoriosProxy _qCTConsultorio;
        private readonly IQCTMedicamentosProxy _qCTMedicamentos;
        private readonly IQCTTipoInsumoProxy _qCTTipoInsumo;
        private readonly IQCTVariablesMedicasProxy _variables;
        private readonly IQCTENotaTraspasoProxy _qEstatusNotas;
        private readonly IQInventariosProxy _inventarios;
        private readonly IQMovimientosProxy _qMovimientos;

        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public List<CTConsultorioDto> Consultorios { get; set; }
        public List<LoteDto> Lotes { get; set; }

        public IndexModel(IModuloProxy modulo, IPermisoProxy permisos, IQCTConsultoriosProxy qCTConsultorio, IQInventariosProxy inventarios,
            IQCTENotaTraspasoProxy qEstatusNotas, IUsuarioProxy usuario, IQCTMedicamentosProxy qCTMedicamentos, IQMovimientosProxy qMovimientos,
            IQCTTipoInsumoProxy qCTTipoInsumo, IQCTVariablesMedicasProxy variables)
        {
            _modulo = modulo;
            _permisos = permisos;
            _usuario = usuario;
            _inventarios = inventarios;
            _qCTTipoInsumo = qCTTipoInsumo;
            _variables = variables;
            _qMovimientos = qMovimientos;
            _qCTConsultorio = qCTConsultorio;
            _qCTMedicamentos = qCTMedicamentos;
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
                Lotes = await CargarLotesAsync();

                return Page(); // ?? importante
            }

            return Redirect("/error/denegado"); // ?? ahora sí funciona
        }

        private async Task<List<LoteDto>> CargarLotesAsync()
        {
            var lotes = (await _inventarios.GetLotesAsync()).Select(m => new LoteDto {
                UsuarioId = m.UsuarioId,
                Lote = m.Lote,
                MedicamentoId = m.MedicamentoId,
                TipoInsumoId = m.TipoInsumoId,
                FormaFarmaceuticaId = m.FormaFarmaceuticaId,
                FechaCaducidad = m.FechaCaducidad,
            }).DistinctBy(x => new {
                x.UsuarioId,
                x.Lote,
                x.MedicamentoId,
                x.TipoInsumoId,
                x.FormaFarmaceuticaId,
                x.FechaCaducidad
            }).ToList();

            foreach (var m in lotes)
            {
                m.Usuario = await _usuario.GetUsuarioById(m.UsuarioId);
                m.Medicamento = await _qCTMedicamentos.GetMedicamentoByIdAsync(m.MedicamentoId);
                m.TipoInsumo = await _qCTTipoInsumo.GetTipoInsumoByIdAsync(m.TipoInsumoId);
                m.FormaFarmaceutica = await _variables.GEtVariableByIdAsync(m.FormaFarmaceuticaId);
            }

            return lotes;
        }
    }
}
