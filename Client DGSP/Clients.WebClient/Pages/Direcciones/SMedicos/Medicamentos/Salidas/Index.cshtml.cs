using DGSP.Gateway.Proxy.Queries.Catalogos.CTConsultorios;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMedicamentos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposInsumos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposMovimientos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesMedicas;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Entradas;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Salidas;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Salidas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.SMedicos.Medicamentos.Salidas
{
    public class IndexModel : PageModel
    {
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IQCTConsultoriosProxy _qCTConsultorio;
        private readonly IQCTMedicamentosProxy _qCTMedicamentos;
        private readonly IQCTTipoInsumoProxy _qCTTipoInsumo;
        private readonly IQCTTipoMovimientoProxy _qCTTipoMovimiento;
        private readonly IQCTVariablesMedicasProxy _qVariables;
        private readonly IQSalidaMedicamentoProxy _qSalidas;
        private readonly IQInventariosProxy _lotes;
        private readonly IQSalidaMedicamentoDetalleProxy _qSalidasDetalle;

        [BindProperty(SupportsGet = true)]
        public int? ConsultorioId { get; set; }

        public string? Error { get; private set; }
        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public List<SalidaMedicamentoDto> Salidas { get; set; } = new();

        public IndexModel(IModuloProxy modulo, IPermisoProxy permisos, IQCTConsultoriosProxy qCTConsultorio, IQCTMedicamentosProxy qCTMedicamentos, 
            IQCTTipoInsumoProxy qCTTipoInsumo, IQCTTipoMovimientoProxy qCTTipoMovimiento, IQCTVariablesMedicasProxy qVariables, 
            IQSalidaMedicamentoProxy qSalidas, IQSalidaMedicamentoDetalleProxy qSalidasDetalle, IQInventariosProxy lotes)
        {
            _modulo = modulo;
            _permisos = permisos;
            _qCTConsultorio = qCTConsultorio;
            _qCTMedicamentos = qCTMedicamentos;
            _qCTTipoInsumo = qCTTipoInsumo;
            _qCTTipoMovimiento = qCTTipoMovimiento;
            _qVariables = qVariables;
            _lotes = lotes;
            _qSalidas = qSalidas;
            _qSalidasDetalle = qSalidasDetalle;
        }

        public async Task<IActionResult> OnGet(int moduloId, int submoduloId, int opcionId)
        {
            string usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Permisos = await _permisos.GetPermisosByModuloUsuario(usuario, moduloId);

            if (Permisos.Any(p => p.Permiso.Nombre == "Ver" && p.Submodulo.Id == submoduloId && p.OpcionId == opcionId))
            {
                Modulo = await _modulo.GetModuloByIdAsync(moduloId);
                Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
                Opcion = await _modulo.GetOpcionById(opcionId);
                Salidas = await _qSalidas.GetAllSalidasAsync();
                foreach (var sd in Salidas)
                {
                    sd.Consultorio = await _qCTConsultorio.GetConsultorioByIdAsync(sd.ConsultorioId);
                    sd.Detalles = await _qSalidasDetalle.GetDetallesBySalidaAsync(sd.Id);
                    foreach (var ds in sd.Detalles)
                    {
                        ds.TipoInsumo = await _qCTTipoInsumo.GetTipoInsumoByIdAsync(ds.TipoInsumoId);
                        ds.TipoMovimiento = await _qCTTipoMovimiento.GetTipoMovimientoByIdAsync(ds.TipoMovimientoId);
                        ds.FormaFarmaceutica = await _qVariables.GEtVariableByIdAsync(ds.FormaFarmaceuticaId);
                        ds.TipoEnvase = await _qVariables.GEtVariableByIdAsync(ds.TipoEnvaseId);
                        ds.Lote = await GetLoteById(ds.LoteId);
                    }
                }
                return Page();
            }

            return Redirect("/error/denegado");
        }

        private async Task<LoteDto> GetLoteById(int loteId)
        {
            var lote = await _lotes.GetLoteByIdAsync(loteId);

            lote.Medicamento = await _qCTMedicamentos.GetMedicamentoByIdAsync(lote.MedicamentoId);

            return lote;
        }
    }
}
