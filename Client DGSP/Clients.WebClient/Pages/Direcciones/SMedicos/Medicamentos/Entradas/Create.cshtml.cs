using DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.Entradas;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTConsultorios;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMedicamentos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposInsumos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposMovimientos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesMedicas;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.SMedicos.Medicamentos.Entradas
{
    public class CreateModel : PageModel
    {
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IQCTConsultoriosProxy _qCTConsultorio;
        private readonly IQCTMedicamentosProxy _qMedicamentos;
        private readonly IQCTTipoInsumoProxy _qCTTipoInsumo;
        private readonly IQCTTipoMovimientoProxy _qCTTipoMovimiento;
        private readonly IQCTVariablesMedicasProxy _qVariables;
        private readonly IQInventariosProxy _qInventarios;
        private readonly ICInventariosProxy _iInventarios;

        [BindProperty(SupportsGet = true)]
        public int? ConsultorioId { get; set; }
        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public List<CTMedicamentoDto> Medicamentos { get; set; } = new();
        public List<CTConsultorioDto> Consultorios { get; set; } = new();
        public List<CTTipoInsumoDto> TiposInsumos { get; set; } = new();
        public List<CTTipoMovimientoDto> TiposMovimiento { get; set; } = new();
        public List<LoteDto> Lotes { get; set; } = new();
        public List<CTVariableMedicaDto> FormasFarmaceuticas { get; set; } = new();
        public List<CTVariableMedicaDto> TiposEnvase { get; set; } = new();
        public List<CTVariableMedicaDto> UnidadesContenido{ get; set; } = new();


        [BindProperty]
        public RegistrarLoteMedicamentoCommand Form { get; set; } = new();

        [TempData]
        public string Error { get; set; } = string.Empty;

        [TempData]
        public string Ok { get; set; } = string.Empty;

        public CreateModel(IModuloProxy modulo, IPermisoProxy permisos, IQCTConsultoriosProxy qCTConsultorio, IQCTMedicamentosProxy qMedicamentos, 
            IQCTTipoInsumoProxy qCTTipoInsumo, IQCTTipoMovimientoProxy qCTTipoMovimiento, IQInventariosProxy qInventarios, 
            ICInventariosProxy iInventarios, IQCTVariablesMedicasProxy qVariables)
        {
            _modulo = modulo;
            _permisos = permisos;
            _qCTConsultorio = qCTConsultorio;
            _qMedicamentos = qMedicamentos;
            _qCTTipoInsumo = qCTTipoInsumo;
            _qCTTipoMovimiento = qCTTipoMovimiento;
            _qVariables = qVariables;
            _qInventarios = qInventarios;
            _iInventarios = iInventarios;
        }

        public async Task OnGet(int moduloId, int submoduloId, int opcionId)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Permisos = await _permisos.GetPermisosByModuloUsuario(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, moduloId);
            if (Permisos.Where(p => p.Permiso.Nombre.Equals("Crear")).Count() != 0)
            {
                Modulo = await _modulo.GetModuloByIdAsync(moduloId);
                Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
                Opcion = await _modulo.GetOpcionById(opcionId);
                await CargarCombosAsync();
            }
            else
            {
                Response.Redirect("/error/denegado");
            }
        }

        private async Task CargarCombosAsync()
        {
            Consultorios = await _qCTConsultorio.GetAllConsultoriosAsync() ?? new();
            Medicamentos = await _qMedicamentos.GetAllMedicamentosAsync() ?? new();
            TiposInsumos = await _qCTTipoInsumo.GetAllTiposInsumosAsync() ?? new();
            TiposMovimiento = await _qCTTipoMovimiento.GetMovimientosEntradaAsync() ?? new();
            FormasFarmaceuticas = await _qVariables.GetVariablesByCategoriaAsync("FormaFarmaceutica");
            TiposEnvase = await _qVariables.GetVariablesByCategoriaAsync("TipoEnvase");
            UnidadesContenido = await _qVariables.GetVariablesByCategoriaAsync("UnidadContenido");
        }

        public async Task<IActionResult> OnGetDatosMedicamento(int id)
        {
            var datos = await _qMedicamentos.GetMedicamentoByIdAsync(id);
            
            if(datos != null)
            {
                return new JsonResult(datos);
            }
            return new JsonResult(null);
        }

        public async Task<IActionResult> OnPostAsync(int moduloId, int submoduloId, int opcionId)
        {
            string usuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Permisos = await _permisos.GetPermisosByModuloUsuario(usuario, moduloId);
            Modulo = await _modulo.GetModuloByIdAsync(moduloId);
            Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
            Opcion = await _modulo.GetOpcionById(opcionId);

            await CargarCombosAsync();

            if (Form.TipoInsumoId == 0)
            {
                Error = "Seleccione el tipo de insumo.";
                return Page();
            }

            if (Form.TipoMovimientoId == 0)
            {
                Error = "Seleccione el tipo de ingreso.";
                return Page();
            }

            if (Form.ConsultorioId <= 0)
            {
                Error = "Seleccione el consultorio.";
                return Page();
            }

            if (Form.MedicamentoId <= 0)
            {
                Error = "Seleccione el medicamento.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Form.Lote))
            {
                Error = "Capture el lote.";
                return Page();
            }

            if (Form.FormaFarmaceuticaId == 0)
            {
                Error = "Seleccione la forma farmacéutica.";
                return Page();
            }

            if (Form.FechaCaducidad == default)
            {
                Error = "Seleccione la fecha de caducidad.";
                return Page();
            }

            if (Form.Cantidad <= 0)
            {
                Error = "La cantidad debe ser mayor a 0.";
                return Page();
            }

            if (Form.CantidadEnvase <= 0)
            {
                Error = "La cantidad por unidad debe ser mayor a 0.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Form.Concentracion))
            {
                Error = "La concentración no puede ir vacía.";
                return Page();
            }

            if (Form.TipoEnvaseId == 0)
            {
                Error = "Seleccione el tipo de envase.";
                return Page();
            }

            if (Form.UnidadContenidoId == 0)
            {
                Error = "Seleccione la unidad de contenido.";
                return Page();
            }
           
            if (Form.Observaciones == null)
            {
                Error = "Favor de registrar las observaciones.";
                return Page();
            }

            try
            {
                var loteReq = new RegistrarLoteMedicamentoCommand
                {
                    UsuarioId = usuario,
                    ConsultorioId = Form.ConsultorioId,
                    MedicamentoId = Form.MedicamentoId,
                    TipoInsumoId = Form.TipoInsumoId,
                    TipoMovimientoId = Form.TipoMovimientoId,
                    Lote = Form.Lote.Trim(),
                    FechaCaducidad = Form.FechaCaducidad,
                    FormaFarmaceuticaId = Form.FormaFarmaceuticaId,
                    TipoEnvaseId = Form.TipoEnvaseId,
                    UnidadContenidoId = Form.UnidadContenidoId,
                    Cantidad = Form.Cantidad,
                    CantidadEnvase = Form.CantidadEnvase,
                    CantidadTotal = (Form.Cantidad * Form.CantidadEnvase),
                    Concentracion = Form.Concentracion.Trim(),
                    Observaciones = Form.Observaciones
                };

                var loteResp = await _iInventarios.RegistrarLoteAsync(loteReq);

                if (loteResp == null)
                {
                    Error = "No se obtuvo respuesta del servidor al registrar la entrada.";
                    return Page();
                }

                Ok = "La entrada del medicamento se registró correctamente.";
                return RedirectToPage(new
                {
                    moduloId,
                    submoduloId,
                    opcionId
                });
            }
            catch (Exception ex)
            {
                Error = $"Ocurrió un error al registrar la entrada: {ex.Message}";
                return Page();
            }
        }
    }
}
