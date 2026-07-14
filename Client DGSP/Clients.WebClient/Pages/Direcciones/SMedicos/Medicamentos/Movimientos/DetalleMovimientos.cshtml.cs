using DGSP.Gateway.Proxy.Queries.Catalogos.CTConsultorios;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMedicamentos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposInsumos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposMovimientos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesMedicas;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Entradas;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Movimientos;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Salidas;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Movimientos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.SMedicos.Medicamentos.Movimientos
{
    public class DetalleMovimientosModel : PageModel
    {
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IUsuarioProxy _usuario;
        private readonly IQMovimientosProxy _movimientos;
        private readonly IQCTConsultoriosProxy _qCTConsultorio;
        private readonly IQCTMedicamentosProxy _qCTMedicamentos;
        private readonly IQCTTipoInsumoProxy _qCTTiposInsumos;
        private readonly IQCTTipoMovimientoProxy _qCTTipoMovimientos;
        private readonly IQInventariosProxy _qInventarios;
        private readonly IQSalidaMedicamentoDetalleProxy _salidaDetalle;
        private readonly IQSalidaMedicamentoProxy _salidas;
        private readonly IQCTVariablesMedicasProxy _variables;

        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public LoteDto Lote { get; set; }
        public List<MovimientoInventarioDto> DetalleMovimientos { get; set; }

        public DetalleMovimientosModel(IModuloProxy modulo, IPermisoProxy permisos, IUsuarioProxy usuario,
            IQMovimientosProxy movimientos, IQCTConsultoriosProxy qCTConsultorio, IQCTMedicamentosProxy qCTMedicamentos,
            IQCTTipoInsumoProxy qCTTiposInsumos, IQCTTipoMovimientoProxy qCTTipoMovimientos, IQInventariosProxy qInventarios,
            IQCTVariablesMedicasProxy variables, IQSalidaMedicamentoDetalleProxy salidaDetalle, IQSalidaMedicamentoProxy salidas)
        {
            _modulo = modulo;
            _permisos = permisos;
            _usuario = usuario;
            _movimientos = movimientos;
            _salidaDetalle = salidaDetalle;
            _salidas = salidas;
            _qCTConsultorio = qCTConsultorio;
            _qCTMedicamentos = qCTMedicamentos;
            _qCTTiposInsumos = qCTTiposInsumos;
            _qCTTipoMovimientos = qCTTipoMovimientos;
            _qInventarios = qInventarios;
            _variables = variables;
        }

        public async Task OnGet(int moduloId, int submoduloId, int opcionId, string loteId)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Permisos = await _permisos.GetPermisosByModuloUsuario(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, moduloId);
            if (Permisos.Where(p => p.Permiso.Nombre.Equals("Crear")).Count() != 0)
            {
                Modulo = await _modulo.GetModuloByIdAsync(moduloId);
                Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
                Opcion = await _modulo.GetOpcionById(opcionId);
                Lote = await GetLote(loteId);
                DetalleMovimientos = await GetMovimientosByLote(loteId);
            }
            else
            {
                Response.Redirect("/error/denegado");
            }
        }

        private async Task<List<MovimientoInventarioDto>> GetMovimientosByLote(string lote)
        {
            var lotes = (await _qInventarios.GetLotesAsync()).Where(l => l.Lote.Equals(lote)).Select(l => l.Id).ToList();
            var movimientos = await _movimientos.GetAllMovimientosInventariosAsync();

            var detalleMovimientos = movimientos.Where(m => lotes.Contains(m.LoteId)).ToList();

            foreach (var m in detalleMovimientos)
            {
                m.Usuario = await _usuario.GetUsuarioById(m.UsuarioId);
                m.Lote = await _qInventarios.GetLoteByIdAsync(m.LoteId);
                m.Lote.Consultorio = await _qCTConsultorio.GetConsultorioByIdAsync(m.Lote.ConsultorioId);
                m.Lote.FormaFarmaceutica = await _variables.GEtVariableByIdAsync(m.Lote.FormaFarmaceuticaId);
                m.Lote.TipoEnvase = await _variables.GEtVariableByIdAsync(m.Lote.TipoEnvaseId);
                m.Lote.Medicamento = await _qCTMedicamentos.GetMedicamentoByIdAsync(m.Lote.MedicamentoId);
                m.Lote.Medicamento.TipoInsumo = await _qCTTiposInsumos.GetTipoInsumoByIdAsync(m.Lote.Medicamento.TipoInsumoId);
                m.Lote.Medicamento.TipoEnvase = await _variables.GEtVariableByIdAsync(m.Lote.Medicamento.TipoEnvaseId);
                if (m.SalidaDetalleId != null && m.SalidaDetalleId != 0)
                {
                    m.SalidaDetalle = await _salidaDetalle.GetDetallesById((int)m.SalidaDetalleId);
                    m.SalidaDetalle.Salida = await _salidas.GetSalidaMedicamentoByIdAsync(m.SalidaDetalle.SalidaId);
                    m.SalidaDetalle.Consultorio = await _qCTConsultorio.GetConsultorioByIdAsync(m.SalidaDetalle.ConsultorioDestinoId);
                }
            }

            return detalleMovimientos
                .OrderBy(x => x.FechaMovimiento)
                .ToList();
        }

        private async Task<LoteDto> GetLote(string lote)
        {
            var lotes = await _qInventarios.GetDatosByLoteAsync(lote);
            lotes.FormaFarmaceutica = await _variables.GEtVariableByIdAsync(lotes.FormaFarmaceuticaId);
            lotes.TipoEnvase = await _variables.GEtVariableByIdAsync(lotes.TipoEnvaseId);
            lotes.Medicamento = await _qCTMedicamentos.GetMedicamentoByIdAsync(lotes.MedicamentoId);
            lotes.TipoInsumo = await _qCTTiposInsumos.GetTipoInsumoByIdAsync(lotes.TipoInsumoId);

            return lotes;
        }
    }
}
