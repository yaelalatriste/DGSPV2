using DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.Entradas;
using DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Gateway.Proxy.Commands.SMedicos.Medicamentos.Salidas;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTConsultorios;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTMedicamentos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposInsumos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTTiposMovimientos;
using DGSP.Gateway.Proxy.Queries.Catalogos.CTVariablesMedicas;
using DGSP.Gateway.Proxy.Queries.DGRH.Empleados;
using DGSP.Gateway.Proxy.Queries.Estatus.NotasTraspaso;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.Entradas;
using DGSP.Gateway.Proxy.Queries.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Logs;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.NotasTraspaso;
using DGSP.Shared.Contracts.Commands.SMedicos.Medicamentos.Salidas;
using DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos;
using DGSP.Shared.Contracts.DTOs.Estatus.NotasTraspaso;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Entradas;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.Logs;
using DGSP.Shared.Contracts.DTOs.SMedicos.Medicamentos.NotasTraspaso;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clients.WebClient.Pages.Direcciones.SMedicos.Medicamentos.NotasTraspaso
{
    public class DetalleNotaModel : PageModel
    {
        private readonly IModuloProxy _modulo;
        private readonly IPermisoProxy _permisos;
        private readonly IUsuarioProxy _usuario;
        private readonly IQEmpleadoProxy _empleado;
        private readonly IQNotasTraspasoProxy _qNotasTraspaso;
        private readonly ICNotasTraspasoProxy _cNotasTraspaso;
        private readonly IQDetalleNotasTraspasoProxy _qDetalleNotas;
        private readonly ICDetalleNotasTraspasoProxy _cDetalleNotas;
        private readonly IQCTConsultoriosProxy _qCTConsultorio;
        private readonly IQCTENotaTraspasoProxy _qEstatusNotas;
        private readonly IQCTMedicamentosProxy _qCTMedicamentos;
        private readonly IQCTTipoInsumoProxy _qCTTiposInsumos;
        private readonly IQCTTipoMovimientoProxy _qCTTipoMovimientos;
        private readonly IQInventariosProxy _qInventarios;
        private readonly ICInventariosProxy _cInventarios;
        private readonly IQFlujoNotasProxy _flujo;
        private readonly ICSalidaMedicamentoProxy _cSalidaMedicamentoProxy;
        private readonly IQCTVariablesMedicasProxy _variables;
        private readonly ICSalidaMedicamentoDetalleProxy _cSalidaMedicamentoDetalleProxy;

        public ModuloDto Modulo { get; set; }
        public SubmoduloDto Submodulo { get; set; }
        public OpcionDto Opcion { get; set; }
        public List<FlujoNotaTraspasoDto> Flujo { get; set; }
        public List<PermisoUsuarioDto> Permisos { get; set; }
        public List<CTMedicamentoDto> Medicamentos { get; set; }
        public List<CTTipoInsumoDto> TiposInsumos { get; set; }
        public List<CTTipoMovimientoDto> TiposMovimientos { get; set; }
        public List<LoteDto> Lotes { get; set; }
        public NotaTraspasoDto NotaTraspaso { get; set; }
        public List<DetalleNotaTraspasoDto> DetalleNota { get; set; }
        public List<LogNotaTraspasoDto> Logs { get; set; }

        public DetalleNotaModel(IModuloProxy modulo, IPermisoProxy permisos, IQNotasTraspasoProxy qNotasTraspaso, IQCTConsultoriosProxy qCTConsultorio, 
            IQCTENotaTraspasoProxy qEstatusNotas, IQCTMedicamentosProxy qCTMedicamentos, IQCTTipoInsumoProxy qCTTiposInsumos, ICNotasTraspasoProxy cNotasTraspaso,
            IQCTTipoMovimientoProxy qCTTipoMovimientos, IQInventariosProxy inventarios, ICDetalleNotasTraspasoProxy cDetalleNotas, IQDetalleNotasTraspasoProxy qDetalleNotas,
            ICInventariosProxy cInventarios, ICSalidaMedicamentoProxy cSalidaMedicamentoProxy, ICSalidaMedicamentoDetalleProxy cSalidaMedicamentoDetalleProxy,
            IQFlujoNotasProxy flujo, IQCTVariablesMedicasProxy variables, IUsuarioProxy usuario, IQEmpleadoProxy empleado)
        {
            _modulo = modulo;
            _permisos = permisos;
            _usuario = usuario;
            _empleado = empleado;
            _flujo = flujo;
            _variables = variables;
            _qNotasTraspaso = qNotasTraspaso;
            _cNotasTraspaso = cNotasTraspaso;
            _qCTConsultorio = qCTConsultorio;
            _qEstatusNotas = qEstatusNotas;
            _qCTMedicamentos = qCTMedicamentos;
            _qCTTiposInsumos = qCTTiposInsumos;
            _qCTTipoMovimientos = qCTTipoMovimientos;
            _qInventarios = inventarios;
            _cInventarios = cInventarios;
            _qDetalleNotas = qDetalleNotas;
            _cDetalleNotas = cDetalleNotas;
            _cSalidaMedicamentoProxy = cSalidaMedicamentoProxy;
            _cSalidaMedicamentoDetalleProxy = cSalidaMedicamentoDetalleProxy;
        }

        public async Task OnGet(int moduloId, int submoduloId, int opcionId, int notaId)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Permisos = await _permisos.GetPermisosByModuloUsuario(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, moduloId);
            if (Permisos.Where(p => p.Permiso.Nombre.Equals("Crear")).Count() != 0)
            {
                Modulo = await _modulo.GetModuloByIdAsync(moduloId);
                Submodulo = await _modulo.GetSubmoduloByIdAsync(submoduloId);
                Opcion = await _modulo.GetOpcionById(opcionId);
                NotaTraspaso = await GetNotaTraspasoById(notaId);
                DetalleNota = await GetDetallesNotaTraspasoById(notaId);
                Flujo = await _flujo.GetEstatusConsecutivoNota(NotaTraspaso.EstatusId);
                Logs = await GetLogsNotaTraspaso(notaId);
                await CargarCombosAsync();
            }
            else
            {
                Response.Redirect("/error/denegado");
            }
        }

        private async Task CargarCombosAsync()
        {
            TiposInsumos = await _qCTTiposInsumos.GetAllTiposInsumosAsync() ?? new();
            TiposMovimientos = await _qCTTipoMovimientos.GetMovimientosSalidaAsync() ?? new();
        }

        private async Task<NotaTraspasoDto> GetNotaTraspasoById(int id)
        {
            var nota = await _qNotasTraspaso.GetNotaTraspasoByIdAsync(id);
            nota.ConsultorioOrigen = await _qCTConsultorio.GetConsultorioByIdAsync(nota.ConsultorioId);
            nota.ConsultorioDestino = await _qCTConsultorio.GetConsultorioByIdAsync(nota.ConsultorioDestinoId);
            nota.Estatus = await _qEstatusNotas.GetEstatusByIdAsync(nota.EstatusId);

            return nota;
        }

        private async Task<List<LogNotaTraspasoDto>> GetLogsNotaTraspaso(int notaId)
        {
            var logs = await _qNotasTraspaso.GetLogsNotaTraspasoByIdAsync(notaId);
            foreach (var l in logs)
            {
                l.Usuario = await _usuario.GetUsuarioById(l.UsuarioId);
                l.Estatus = await _qEstatusNotas.GetEstatusByIdAsync(l.EstatusId);
            }

            return logs;
        }
        
        public async Task<IActionResult> OnGetDetalleById(int detalleId)
        {
            var detalle = await _qDetalleNotas.GetDetalleNotaTraspasoByIdAsync(detalleId);
            if (detalle != null)
            {
                return new JsonResult(detalle);
            }
            return new JsonResult(new DetalleNotaTraspasoDto());
        }
        
        public async Task<IActionResult> OnGetLoteById(int loteId)
        {
            var lote = await _qInventarios.GetLoteByIdAsync(loteId);
            if (lote!= null)
            {
                return new JsonResult(lote);
            }
            return new JsonResult(new LoteDto());
        }
        
        private async Task<List<DetalleNotaTraspasoDto>> GetDetallesNotaTraspasoById(int nota)
        {
            var detallesNota = await _qDetalleNotas.GetDetallesNotaTraspasoByNotaAsync(nota);
            foreach (var dt in detallesNota)
            {
                dt.Lote = await _qInventarios.GetLoteByIdAsync(dt.LoteId);
                dt.TipoMovimiento = await _qCTTipoMovimientos.GetTipoMovimientoByIdAsync(dt.TipoMovimientoId);
                dt.Lote.TipoInsumo = await _qCTTiposInsumos.GetTipoInsumoByIdAsync(dt.Lote.TipoInsumoId);
                dt.Lote.Medicamento = await _qCTMedicamentos.GetMedicamentoByIdAsync(dt.Lote.MedicamentoId);
                dt.Lote.FormaFarmaceutica = await _variables.GEtVariableByIdAsync(dt.Lote.FormaFarmaceuticaId);
                dt.Lote.TipoEnvase = await _variables.GEtVariableByIdAsync(dt.Lote.TipoEnvaseId);
            }

            return detallesNota;
        }

        public async Task<IActionResult> OnGetDatosLote(string lote, int consultorio)
        {
            var datos = await _qInventarios.GetDatosByLoteConsultorioAsync(lote,consultorio);
            datos.FormaFarmaceutica = await _variables.GEtVariableByIdAsync(datos.FormaFarmaceuticaId);
            datos.TipoEnvase = await _variables.GEtVariableByIdAsync(datos.TipoEnvaseId);
            if(datos != null)
            {
                return new JsonResult(datos);
            }
            return new JsonResult(new LoteDto());
        }
        
        public async Task<IActionResult> OnGetDatosLoteMedicamento(string lote, int consultorio)
        {
            var obtenerLote = await _qInventarios.GetDatosByLoteConsultorioAsync(lote, consultorio);
            var datos = await _qInventarios.GetDatosByLoteConsultorioMedicamentoAsync(lote,consultorio, obtenerLote.MedicamentoId);
            datos.FormaFarmaceutica = await _variables.GEtVariableByIdAsync(datos.FormaFarmaceuticaId);
            datos.TipoEnvase = await _variables.GEtVariableByIdAsync(datos.TipoEnvaseId);
            if(datos != null)
            {
                return new JsonResult(datos);
            }
            return new JsonResult(new LoteDto());
        }

        public async Task<JsonResult> OnGetMedicamentosPorLote(string lote,int consultorio)
        {
            var medicamentos = await _qInventarios.GetMedicamentosPorLote(lote, consultorio);
            if(medicamentos != null)
            {
                foreach (var datos in medicamentos)
                {
                    datos.Medicamento = await _qCTMedicamentos.GetMedicamentoByIdAsync(datos.MedicamentoId);
                    datos.FormaFarmaceutica = await _variables.GEtVariableByIdAsync(datos.FormaFarmaceuticaId);
                    datos.TipoEnvase = await _variables.GEtVariableByIdAsync(datos.TipoEnvaseId);
                }

                return new JsonResult(medicamentos);
            }
            return new JsonResult(new List<LoteDto>());
        }

        public async Task<IActionResult> OnGetLoteDetalleNota(int notaId, int loteId)
         {
            var detalle = await _qDetalleNotas.GetDetallesNotaTraspasoByNotaAsync(notaId);
            var existLote = detalle.Where(dt => dt.LoteId == loteId).Count();
            return new JsonResult(existLote);
        }
        
        public async Task<IActionResult> OnGetDatosMedicamento(int id)
        {
            var datos = await _qCTMedicamentos.GetMedicamentoByIdAsync(id);
            
            if(datos != null)
            {
                return new JsonResult(datos);
            }
            return new JsonResult(null);
        }

        public async Task<IActionResult> OnPostCreateDetalleNotaTraspaso([FromBody] RegistrarDetalleNotaTraspasoCommand command)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = Usuario;
            var detalle = await _cDetalleNotas.CreateDetalleNotaTraspasoAsync(command);

            if (detalle != null)
            {
                return new JsonResult(detalle);
            }
            return BadRequest();
        }
        
        public async Task<IActionResult> OnPutUpdateDetalleNotaTraspaso([FromBody] ActualizarDetalleNotaTraspasoCommand command)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = Usuario;
            var detalle = await _cDetalleNotas.UpdateDetalleNotaTraspasoAsync(command);

            if (detalle != null)
            {
                return new JsonResult(detalle);
            }
            return BadRequest();
        }
        
        public async Task<IActionResult> OnPutDeleteDetalleNotaTraspaso([FromBody] EliminarDetalleNotaTraspasoCommand command)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId= Usuario;
            var detalle = await _cDetalleNotas.DeleteDetalleNotaTraspasoAsync(command);

            if (detalle != null)
            {
                return new JsonResult(detalle);
            }
            return BadRequest();
        }

        public async Task<IActionResult> OnPutUpdateNotaTraspaso([FromBody] ActualizarNotaTraspasoCommand command)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = Usuario;

            var update = await _cNotasTraspaso.ActualizarNotaTraspasoAsync(command);
            if (update != null && command.Procesar)
            {
                await ProcesarEntradasSalidas(command);
            }

            RegistrarLogNotaTraspasoCommand log = new RegistrarLogNotaTraspasoCommand();
            log.UsuarioId = Usuario;
            log.NotaId = command.Id;
            log.EstatusId = command.EstatusId;
            log.Observaciones = command.Observaciones;
            var createLog = await _cNotasTraspaso.CreateLogNotaTraspaso(log);
            
            if (createLog != null)
            {
                return new JsonResult(update);
            }

            return BadRequest();
        }

        private async Task<bool> ProcesarEntradasSalidas(ActualizarNotaTraspasoCommand command)
        {
            var nota = await GetNotaTraspasoById(command.Id);
            var finish = false;

            RegistrarSalidaMedicamentoCommand commandSalida = new RegistrarSalidaMedicamentoCommand();
            commandSalida.UsuarioId = command.UsuarioId;
            commandSalida.ConsultorioId = nota.ConsultorioId;
            commandSalida.FechaSalida = DateTime.Now;
            var salida = await _cSalidaMedicamentoProxy.RegistrarSalidaMedicamentoAsync(commandSalida);
            if (salida != null)
            {
                var detallesNota = await _qDetalleNotas.GetDetallesNotaTraspasoByNotaAsync(command.Id);
                foreach (var dt in detallesNota)
                {
                    finish = false;
                    var queryLote = await _qInventarios.GetLoteByIdAsync(dt.LoteId);
                    RegistrarSalidaMedicamentoDetalleCommand commandSd = new RegistrarSalidaMedicamentoDetalleCommand();
                    commandSd.SalidaId = salida.Id;
                    commandSd.ConsultorioDestinoId = nota.ConsultorioDestinoId;
                    commandSd.UsuarioId = command.UsuarioId;
                    commandSd.LoteId = dt.LoteId;
                    commandSd.TipoInsumoId = queryLote.TipoInsumoId;
                    commandSd.TipoMovimientoId = dt.TipoMovimientoId;
                    commandSd.Cantidad = dt.Cantidad;
                    commandSd.CantidadEnvase = (int)queryLote.CantidadEnvase;
                    commandSd.FormaFarmaceuticaId = queryLote.FormaFarmaceuticaId;
                    commandSd.TipoEnvaseId = queryLote.TipoEnvaseId;
                    commandSd.Observaciones = "Salida de Medicamento";

                    var salidaDetalle = await _cSalidaMedicamentoDetalleProxy.RegistrarDetalleSalidaAsync(commandSd);
                    if (salidaDetalle != null)
                    {
                        RegistrarLoteMedicamentoCommand commandLote = new RegistrarLoteMedicamentoCommand();
                        commandLote.UsuarioId = command.UsuarioId;
                        commandLote.ConsultorioId = nota.ConsultorioDestinoId;
                        commandLote.TipoInsumoId = queryLote.TipoInsumoId;
                        commandLote.TipoMovimientoId = dt.TipoMovimientoId;
                        commandLote.MedicamentoId = queryLote.MedicamentoId;
                        commandLote.FormaFarmaceuticaId = queryLote.FormaFarmaceuticaId;
                        commandLote.UnidadContenidoId = queryLote.UnidadContenidoId;
                        commandLote.TipoEnvaseId = queryLote.TipoEnvaseId;
                        commandLote.Lote = queryLote.Lote;
                        commandLote.FechaCaducidad = queryLote.FechaCaducidad;
                        commandLote.Cantidad = dt.Cantidad;
                        commandLote.CantidadEnvase = queryLote.CantidadEnvase;
                        commandLote.CantidadTotal = (dt.Cantidad * queryLote.CantidadEnvase);
                        commandLote.Concentracion = queryLote.Concentracion;
                        commandLote.Observaciones = "Ingreso de Medicamento";

                        var lote = await _cInventarios.RegistrarLoteAsync(commandLote);
                        if (lote != null)
                        {
                            finish = true;
                        }
                    }
                }
            }

            return finish;
        }

        public async Task<IActionResult> OnGetMemorandumByNota(int notaId)
        {
            try
            {
                var notaTraspaso = await _qNotasTraspaso.GetNotaTraspasoByIdAsync(notaId);
                var detallesNota = await _qDetalleNotas.GetDetallesNotaTraspasoByNotaAsync(notaId);
                var consultorio = await _qCTConsultorio.GetConsultorioByIdAsync(notaTraspaso.ConsultorioDestinoId);
                var responsableConsultorio = await _empleado.GetEmpleadoByExpediente(consultorio.ExpedienteResponsable);
                var puestoResponsable = (await _empleado.GetMovimientosEmpleado(consultorio.ExpedienteResponsable)).First();

                var responsable = responsableConsultorio.Nombre.ToLower()+" "+responsableConsultorio.Paterno.ToLower()+" "+responsableConsultorio.Materno.ToLower();
                var puesto = puestoResponsable.Puesto.ToLower();

                int f = 0;
                int i = 0;

                Document document = new Document();

                var path = Path.Combine(Directory.GetCurrentDirectory(),"Plantillas", "Formato Memo.docx");
                document.LoadFromFile(path);

                // Reemplazos
                document.Replace("|día|", (DateTime.Now.Day < 10 ? "0"+ DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString()), false, true);
                document.Replace("|mes|", DateTime.Now.ToString("MMMM", new CultureInfo("es-ES")), false, true);
                document.Replace("|anio|", DateTime.Now.Year.ToString(), false, true);
                document.Replace("|memo|", notaTraspaso.NumeroTraspaso, false, true);
                document.Replace("|ResponsableConsultorio|", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(responsable), false, true);
                document.Replace("|PuestoResponsable|", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(puesto), false, true);
                document.Replace("|ServicioMedico|", "Servicio Médico en "+CultureInfo.CurrentCulture.TextInfo.ToTitleCase(consultorio.Nombre.ToLower()), false, true);

                // Buscar el texto |Tabla|
                TextSelection selection = document.FindString("|Tabla|", true, true);

                if (selection != null)
                {
                    TextRange textRange = selection.GetAsOneRange();
                    textRange.CharacterFormat.Bold = true;
                    textRange.CharacterFormat.FontSize = 14;
                    textRange.CharacterFormat.FontName = "Arial";
                    textRange.CharacterFormat.TextColor = Color.Black;

                    Paragraph paragraph = textRange.OwnerParagraph;
                    Body body = paragraph.OwnerTextBody;

                    int index = body.ChildObjects.IndexOf(paragraph);

                    // Crear tabla (ejemplo 4 columnas)
                    Table table = new Table(document, true);

                    int filas = detallesNota.Count + 1; // +1 encabezado
                    int columnas = 6;

                    table.ResetCells(filas, columnas);

                    // Encabezados
                    table[0, 0].AddParagraph().AppendText("#");
                    table[0, 1].AddParagraph().AppendText("Medicamento");
                    table[0, 2].AddParagraph().AppendText("Lote");
                    table[0, 3].AddParagraph().AppendText("Caducidad");
                    table[0, 4].AddParagraph().AppendText("Presentación");
                    table[0, 5].AddParagraph().AppendText("Cantidad");

                    // Datos dinámicos (ejemplo)
                    foreach(var dt in detallesNota)
                    {
                        f++;
                        var lote = await _qInventarios.GetLoteByIdAsync(dt.LoteId);
                        var medicamento = await _qCTMedicamentos.GetMedicamentoByIdAsync(lote.MedicamentoId);

                        table[i + 1, 0].AddParagraph().AppendText(f+"");
                        table[i + 1, 1].AddParagraph().AppendText(medicamento.Nombre);
                        table[i + 1, 2].AddParagraph().AppendText(lote.Lote);
                        table[i + 1, 3].AddParagraph().AppendText(lote.FechaCaducidad.ToString("dd/MM/yyyy"));
                        table[i + 1, 4].AddParagraph().AppendText(medicamento.Presentacion);
                        table[i + 1, 5].AddParagraph().AppendText(dt.Cantidad.ToString());
                        i++;
                    }

                    // Insertar tabla en el documento
                    body.ChildObjects.Insert(index + 1, table);

                    // Eliminar el placeholder
                    body.ChildObjects.Remove(paragraph);

                    for (int j = 0; j < columnas; j++)
                    {
                        var cell = table[0, j];
                        var parag = cell.Paragraphs[0];
                        var textR = parag.ChildObjects[0] as TextRange;

                        textR.CharacterFormat.Bold = true;
                        textR.CharacterFormat.FontName = "Arial";
                        textR.CharacterFormat.FontSize = 10;
                        parag.Format.HorizontalAlignment = HorizontalAlignment.Center;
                        cell.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        cell.CellFormat.BackColor = Color.LightGray;
                    }

                    for (int a = 1; a < filas; a++)
                    {
                        for (int j = 0; j < columnas; j++)
                        {
                            var cell = table[a, j];
                            var parag = cell.Paragraphs[0];
                            var textR = parag.ChildObjects[0] as TextRange;

                            textR.CharacterFormat.FontName = "Arial";
                            textR.CharacterFormat.FontSize = 10;

                            parag.Format.HorizontalAlignment = HorizontalAlignment.Center;
                            cell.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        }
                    }
                }

                byte[] toArray = null;
                using (MemoryStream ms1 = new MemoryStream())
                {
                    document.SaveToStream(ms1, Spire.Doc.FileFormat.Docx2013);
                    toArray = ms1.ToArray();
                }

                return File(toArray, "application/ms-word", "Memorandum.docx");
            }
            catch (Exception ex)
            {
                string path = Directory.GetCurrentDirectory() + "\\Plantillas";
                var nombre = new { ex.Message, ex.StackTrace, path };
                string json = JsonSerializer.Serialize(nombre, new JsonSerializerOptions
                {
                    WriteIndented = true // Para que el JSON tenga formato legible
                });

                return new JsonResult(json);
            }
        }

        public async Task<IActionResult> OnPutConcluirNotaTraspaso([FromForm] ConcluirNotaTraspasoCommand command)
        {
            string Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.UsuarioId = Usuario;
            var update = await _cNotasTraspaso.ConcluirNotaTraspasoAsync(command);
            if (update != null)
            {
                ActualizarNotaTraspasoCommand procesar = new ActualizarNotaTraspasoCommand();
                procesar.Id = command.Id;
                procesar.UsuarioId = command.UsuarioId;
                await ProcesarEntradasSalidas(procesar);

                RegistrarLogNotaTraspasoCommand log = new RegistrarLogNotaTraspasoCommand();
                log.UsuarioId = Usuario;
                log.NotaId = command.Id;
                log.EstatusId = command.EstatusId;
                log.Observaciones = command.Observaciones;
                var createLog = await _cNotasTraspaso.CreateLogNotaTraspaso(log);
                if (createLog != null)
                {
                    return new JsonResult(update);
                }
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnGetVisualizarEntregable(int notaId)
        {
            string path = await _qNotasTraspaso.VisualizarEntregable(notaId);
            Stream stream = System.IO.File.Open(path, FileMode.Open);
            return File(stream, "application/pdf");
        }
    }
}
