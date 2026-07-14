using DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.Entradas;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTConsultorios;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMedicamentos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesMedicas;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.SMedicos.Medicamentos.Entradas
{
    public class IndexModel : PageModel
    {
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IQCTConsultoriosProxy _qCTConsultorio;
        private readonly IQCTMedicamentosProxy _qCTMedicamentos;
        private readonly IQInventariosProxy _qInventarios;
        private readonly ICInventariosProxy _iInventarios;
        private readonly IQCTVariablesMedicasProxy _variables;

        [BindProperty(SupportsGet = true)]
        public int? ConsultorioId { get; set; }

        public string? Error { get; private set; }
        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public List<LoteDto> Lotes { get; set; } = new();

        public IndexModel(IModuloProxy modulo, IPermisoProxy permisos, IQInventariosProxy qInventarios, ICInventariosProxy iInventarios, IQCTMedicamentosProxy qCTMedicamentos,
                          IQCTConsultoriosProxy qCTConsultorio, IQCTVariablesMedicasProxy variables)
        {
            _modulo = modulo;
            _permisos = permisos;
            _variables = variables;
            _qInventarios = qInventarios;
            _iInventarios = iInventarios;
            _qCTConsultorio = qCTConsultorio;
            _qCTMedicamentos = qCTMedicamentos;
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
                Lotes = await _qInventarios.GetLotesAsync();
                foreach (var lt in Lotes)
                {
                    lt.Consultorio = await _qCTConsultorio.GetConsultorioByIdAsync(lt.ConsultorioId);
                    lt.Medicamento = await _qCTMedicamentos.GetMedicamentoByIdAsync(lt.MedicamentoId);
                    lt.FormaFarmaceutica = await _variables.GEtVariableByIdAsync(lt.FormaFarmaceuticaId);
                }
                return Page();
            }

            return Redirect("/error/denegado");
        }
    }
}
