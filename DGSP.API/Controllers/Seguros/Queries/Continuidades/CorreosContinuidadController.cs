using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Module.DGRH.Application.Queries.Empleado;
using DGSP.Module.Estatus.Application.Services.Continuidades;
using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Shared.Contracts.DTOs.ExternalServices.Email;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Continuidades.Continuidad;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;
using Usuarios.Service.Queries.Queries;

namespace DGSP.API.Controllers.Seguros.Queries.Continuidades
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/[controller]")]
    public class CorreosContinuidadController : ControllerBase
    {
        private readonly ICTEntregableService _ctEntregable;
        private readonly ICTVariableMedicaService _variables;
        private readonly IEstatusContinuidadesService _estatusContinuidades;
        private readonly IContinuidadService _continuidad;
        private readonly IContactoContinuidadService _contacto;
        private readonly IEntregableContinuidadService _entregables;
        private readonly IEmpleadoQueryService _empleado;
        private readonly IWebHostEnvironment _environment;

        public CorreosContinuidadController(IContinuidadService continuidad, IContactoContinuidadService contacto, 
            IEntregableContinuidadService entregables, IEmpleadoQueryService empleado, ICTEntregableService ctEntregable, 
            IEstatusContinuidadesService estatusContinuidades, IWebHostEnvironment environment, ICTVariableMedicaService variables)
        {
            _continuidad = continuidad;
            _contacto = contacto;
            _entregables = entregables;
            _variables = variables;
            _estatusContinuidades = estatusContinuidades;
            _empleado = empleado;
            _ctEntregable = ctEntregable;
            _environment = environment;
        }

        [HttpGet]
        [Route("enviarCorreoPoliza/{continuidadId}")]
        public async Task<IActionResult> EnviarCorreoPoliza(int continuidadId)
        {
            try
            {
                var continuidad = await _continuidad.GetContinuidadByIdAsync(continuidadId);
                var tipoContactoId = (await _variables.GetVariablesByCategoria("TipoContacto")).Where(v => v.Abreviacion.Equals("CorreoElectronico")).First().Id;
                var contactos = (await _contacto.GetContactosByContinuidad(continuidadId)).Where(c => c.TipoId == tipoContactoId).ToList();
                var iniciales = await GetInicialesSP(continuidad);
                var entregables = await _entregables.GetEntregablesByContinuidad(continuidadId);
                var caratula = (await _ctEntregable.GetAllEntregablesAsync()).Where(e => e.Abreviacion.Equals("CaratulaPoliza")).First();

                var entregablesCaratula = entregables.Where(e => e.EntregableId == caratula.Id && !e.FechaEliminacion.HasValue).ToList();               

                List<string> correosOcultos = new List<string>();
                correosOcultos.Add("jymiranda@oaj.gob.mx");
                correosOcultos.Add("oregenera@oaj.gob.mx");
                correosOcultos.Add("dkhernandez@oaj.gob.mx");
                correosOcultos.Add("epueblac@oaj.gob.mx");

                List<string> to = new List<string>();
                foreach (var cn in contactos)
                {
                    to.Add(cn.Descripcion);
                }

                string folderPath = Path.Combine(_environment.ContentRootPath,"Entregables","Seguros","Continuidades",
                    continuidad.Expediente.ToString(),caratula.Abreviacion);

                var attachments = new List<EmailAttachmentDto>();

                foreach (var entregable in entregablesCaratula)
                {
                    string poliza = Path.Combine(folderPath, entregable.Archivo);

                    if (System.IO.File.Exists(poliza))
                    {
                        attachments.Add(await CrearAdjuntoAsync(poliza, "application/pdf"));
                    }
                }

                var emailRequest = new EmailRequestDto
                {
                    Provider = 0,
                    From = "notificacionsgmm@oaj.gob.mx",
                    FromDisplayName = "Notificación de Continuidad del SGMM",
                    To = to,
                    Subject = "Póliza Continuidad SGMM " + iniciales + ". " + continuidad.Expediente,
                    cc = new List<string>(),
                    bcc = correosOcultos,
                    Body = GetMensajeContinuidadSGMMPoliza(),
                    IsBodyHtml = true,
                    importance = 1,
                    attachments = attachments
                };

                return Ok(emailRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpGet]
        [Route("enviarCorreoReferencia/{continuidadId}")]
        public async Task<IActionResult> EnviarCorreoReferencia(int continuidadId)
        {
            try
            {
                var tipoContacto = (await _variables.GetVariablesByCategoria("TipoContacto")).Where(v => v.Abreviacion.Equals("CorreoElectronico")).First().Id;
                var continuidad = await _continuidad.GetContinuidadByIdAsync(continuidadId);
                var contactos = (await _contacto.GetContactosByContinuidad(continuidadId)).Where(c => c.TipoId == tipoContacto).ToList();
                var iniciales = await GetInicialesSP(continuidad);
                var entregables = await _entregables.GetEntregablesByContinuidad(continuidadId);
                var caratula = (await _ctEntregable.GetAllEntregablesAsync()).Where(e => e.Abreviacion.Equals("ReferenciaPago")).First();
                var puesto = (await _empleado.GetMovimientosEmpleado(continuidad.Expediente)).First();
                var entregable = entregables.Where(e => e.EntregableId == caratula.Id && !e.FechaEliminacion.HasValue).First();
                string folderPath = Path.Combine(_environment.ContentRootPath, "Entregables", "Seguros", "Continuidades", continuidad.Expediente.ToString(), caratula.Abreviacion);
                string referencia = Path.Combine(folderPath, entregable.Archivo);


                List<string> correosOcultos = new List<string>();
                correosOcultos.Add("jymiranda@oaj.gob.mx");
                correosOcultos.Add("oregenera@oaj.gob.mx");
                correosOcultos.Add("dkhernandez@oaj.gob.mx");
                correosOcultos.Add("epueblac@oaj.gob.mx");
                List<string> to = new List<string>();
                foreach (var cn in contactos)
                {
                    to.Add(cn.Descripcion);
                }

                string transferencia = @"D:\DGSP\Documentacion Seguros\Pago Vía Transferencia Electrónica_Banorte 2025.pdf";
                string condiciones = @"D:\DGSP\Documentacion Seguros\1.2_Condicionaes Generales_Mandos Medios y Funcionarios Superiores.pdf";
                string condicionesO = @"D:\DGSP\Documentacion Seguros\1.1_Condicionaes Generales_Operativos.pdf";
                
                var attachments = new List<EmailAttachmentDto>();

                if (System.IO.File.Exists(referencia))
                {
                    attachments.Add(await CrearAdjuntoAsync(referencia, "application/pdf"));
                }

                if (System.IO.File.Exists(transferencia))
                {
                    attachments.Add(await CrearAdjuntoAsync(transferencia, "application/pdf"));
                }

                if (System.IO.File.Exists(condiciones))
                {
                    attachments.Add(await CrearAdjuntoAsync(condiciones, "application/pdf"));
                }
                
                if (System.IO.File.Exists(condicionesO))
                {
                    attachments.Add(await CrearAdjuntoAsync(condicionesO, "application/pdf"));
                }

                var emailRequest = new EmailRequestDto
                {
                    Provider = 0,
                    From = "notificacionsgmm@oaj.gob.mx",
                    FromDisplayName = "Notificación de Continuidad del SGMM",
                    To = to,
                    Subject = "Continuidad SGMM " + iniciales + ". " + continuidad.Expediente,
                    cc = new List<string>(),
                    bcc = correosOcultos,
                    Body = await GetMensajeContinuidadSGMMReferencia(continuidadId),
                    IsBodyHtml = true,
                    importance = 1,
                    attachments = attachments
                };

                return Ok(emailRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("enviarCorreoMovimientos")]
        public async Task<IActionResult> EnviarCorreoMovimientos()
        {
            try
            {
                List<string> correosOcultos = new List<string>();
                correosOcultos.Add("jymiranda@oaj.gob.mx");
                correosOcultos.Add("oregenera@oaj.gob.mx");
                correosOcultos.Add("dkhernandez@oaj.gob.mx");
                correosOcultos.Add("epueblac@oaj.gob.mx");
                correosOcultos.Add("jcvazquezt@oaj.gob.mx");
                List<string> correoCopia = new List<string>();
                correoCopia.Add("mehernandezc@oaj.gob.mx");
                correoCopia.Add("eetrujillo@oaj.gob.mx");
                List<string> to = new List<string>();
                to.Add("aavalosf@oaj.gob.mx");

                var emailRequest = new EmailRequestDto
                {
                    Provider = 0,
                    From = "notificacionsgmm@oaj.gob.mx",
                    FromDisplayName = "Notificación de Continuidad del SGMM",
                    To = to,
                    Subject = "Aplicación de Baja/Solicitud de Continuidades",
                    cc = correoCopia,
                    bcc = correosOcultos,
                    Body = await GetMensajeContinuidadSGMMMovimientos(),
                    IsBodyHtml = true,
                    importance = 1,
                };

                return Ok(emailRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private async Task<string> GetMensajeContinuidadSGMMMovimientos()
        {
            var estatusId = (await _estatusContinuidades.GetAllEstatus()).Where(e => e.Abreviacion.Equals("EnMovimientos")).First().Id;
            var continuidades = await _continuidad.GetContinuidadesByEstatus(estatusId);
            int sp = 0;

            string filas = string.Empty;

            foreach (var cn in continuidades)
            {
                sp++;
                var usuario = await _empleado.GetEmpleado(cn.Expediente);
                var puesto = (await _empleado.GetMovimientosEmpleado(cn.Expediente)).First().Puesto;

                filas += "<tr>" +
                    "<td>"+sp+"</td><td>" + cn.Expediente + "</td>" +
                    "<td>" + (usuario.Nombre + " " + usuario.Paterno + " " + usuario.Materno) + "</td>" +
                    "<td>" + cn.FechaBaja.GetValueOrDefault().ToString("dd/MM/yyyy") + "</td>" +
                    "<td>Continuidad</td><td>" + puesto + "</td>" +
                "</tr>";
            }

            string htmlBody =
                "<p style='text-align: right; font-family: arial; font-size: 14px; font-weight: bold;'>Ciudad de México a " +
                DateTime.Now.Day + " de " + DateTime.Now.ToString("MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("es")) +
                " de " + DateTime.Now.Year + "</p>" +

                "<b>Esta cuenta de correo es generada automáticamente, por favor no responder.</b><br><br/>" +
                "<p style='font-family: arial; font-size: 14px;'>Estimada Andrea Avalos,</p>" +
                "<p style='font-family: arial; font-size: 14px;'>Presente.</p>" +
                "<p style='text-align: justify; font-family: arial; font-size: 14px;'>" +
                "Por medio del presente, se solicita su valioso apoyo para la aplicación del movimiento correspondiente a la siguiente persona servidora pública:" +
                "</p>" +
                "<table style='width:100%; border-collapse: collapse; font-family: arial; font-size: 14px;' border='1'>" +
                "<tr style='background-color:#BBEAFC;'>" +
                "<th>Cvo.</th><th>Exp.</th><th>Nombre del S.P.</th><th>Fecha de Baja</th><th>Tipo de Solicitud</th><th>Puesto</th>" +
                "</tr>" +filas+
                "</table><br/>" +

                "<p style='text-align: justify; font-family: arial; font-size: 14px;'>" +
                "Sin más por el momento, se envía un cordial saludo." +
                "</p>" +
                "<p style='text-align: center'><b>Atentamente</b></p>" +
                "<p style='text-align: center; font-family: arial; font-size: 16px;'><b>Dirección de Seguros</b></p>";
            return htmlBody;
        }

        private async Task<string> GetInicialesSP(ContinuidadDto continuidad)
        {
            var empleado = await _empleado.GetEmpleado(continuidad.Expediente);
            return empleado.Nombre.Substring(0, 1) + empleado.Paterno.Substring(0, 1) + empleado.Materno.Substring(0, 1) + "";
        }

        private async Task<EmailAttachmentDto> CrearAdjuntoAsync(string filePath, string contentType)
        {
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            string base64 = Convert.ToBase64String(fileBytes);

            return new EmailAttachmentDto
            {
                fileName = Path.GetFileName(filePath),
                contentType = contentType,
                contentBase64 = base64
            };
        }

        private string GetMensajeContinuidadSGMMPoliza()
        {
            string htmlBody =
        "<p style='text-align: right; font-family: arial; font-size: 14px; font-weight: bold;'>Ciudad de México a " +
        DateTime.Now.Day + " de " +
        DateTime.Now.ToString("MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("es")) +
        " de " + DateTime.Now.Year + "</p>" +

        "<b style='background: yellow;'>Esta cuenta de correo solo tiene fines de difusión, por favor no lo responda.</b><br>" +
        "<b style='background: yellow;'>Para dudas o comentarios, favor de ponerse en contacto con la Lic. Diana Karina Hernández Salgado al Tel. 55-54-49-95-00 ext. 2027 o al correo: " +
        "<a style='background: yellow;' href='mailto:dkhernandez@oaj.gob.mx'>dkhernandez@oaj.gob.mx</a>.</b><br><br/>" +

        "<b>Estimada Persona Servidora Pública:</b><br><br/>" +

        "<p style='text-align: justify; font-family: arial; font-size: 14px;'>" +
        "En atención a su requerimiento, se remite la <b>carátula de la póliza de continuidad</b> correspondiente al periodo del " +
        "<b>31 de diciembre de 2025 al 31 de diciembre de 2026</b>, contratada con <b>Seguros Banorte, S.A. de C.V., Grupo Financiero Banorte</b>." +
        "</p>" +

        "<p style='text-align: justify; font-family: arial; font-size: 14px;'>" +
        "Asimismo, se adjunta en formato PDF la siguiente documentación:" +
        "</p>" +

        "<ul style='font-family: arial; font-size: 14px;'>" +
        "<li>Póliza básica y potenciada (en caso de haber sido contratada).</li>" +
        "<li>Tarjeta de aseguramiento digital.</li>" +
        "</ul>" +

        "<p style='text-align: justify; font-family: arial; font-size: 14px;'>Sin más por el momento, se envía un cordial saludo.</p>" +

        "<p style='text-align: center'><b>Atentamente</b></p>" +
        "<p style='text-align: center; font-family: arial; font-size: 16px;'><b>Dirección de Seguros</b></p>";

            return htmlBody;
        }

        private async Task<string> GetMensajeContinuidadSGMMReferencia(int continuidadId)
        {
            var continuidad = await _continuidad.GetContinuidadByIdAsync(continuidadId);
            string htmlBody =
        "<p style='text-align: right; font-family: arial; font-size: 14px; font-weight: bold;'>Ciudad de México a " +
        DateTime.Now.Day + " de " +
        DateTime.Now.ToString("MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("es")) +
        " de " + DateTime.Now.Year + "</p>" +

        "<b style='background: yellow;'>Esta cuenta de correo solo tiene fines de difusión, por favor no lo responda.</b><br>" +
        "<b style='background: yellow;'>Para dudas o comentarios, favor de ponerse en contacto con la Lic. Diana Karina Hernández Salgado al Tel. 55-54-49-95-00 ext. 2027 o al correo: " +
        "<a style='background: yellow;' href='mailto:dkhernandez@oaj.gob.mx'>dkhernandez@oaj.gob.mx</a>.</b><br><br/>" +

        "<b>Estimada Persona Servidora Pública:</b><br><br/>" +

        "<p style='text-align: justify; font-family: arial; font-size: 14px;'>" +
        "En atención a su requerimiento, mediante el cual solicita la <b>continuidad del Seguro de Gastos Médicos Mayores</b> " +
        "de la póliza <b>1002</b>, contratada para la vigencia del <b>31 de diciembre de 2025 al 31 de diciembre de 2026</b>, " +
        "se remite la <b>referencia bancaria</b> con el costo de la prima que deberá cubrir. " +
        "Asimismo, la <b>fecha límite de pago</b> se encuentra indicada en el archivo anexo, en la cuenta y banco señalados en dicha referencia." +
        "</p>" +

        "<p style='text-align: justify; font-family: arial; font-size: 14px;'>" +
        "No omito mencionar que <b style='color: red;'>su referencia de pago vence el " + continuidad.FechaLimitePago.GetValueOrDefault().Day+" de "+ continuidad.FechaLimitePago.GetValueOrDefault().ToString("MMMM", new CultureInfo("es-ES")) + " de "+continuidad.FechaLimitePago.GetValueOrDefault().Year+"</b>, <b>sin opción a prórroga</b>." +
        "</p>" +

        "<p style='text-align: justify; font-family: arial; font-size: 14px;'>" +
        "Una vez realizado el pago, deberá enviar su <b>comprobante</b> a través de este mismo historial de correo electrónico, " +
        "para su registro correspondiente." +
        "</p>" +

        "<p style='text-align: justify; font-family: arial; font-size: 14px;'>" +
        "Es importante considerar que, en caso de no realizar el pago antes de la <b>fecha límite establecida</b>, la póliza será " +
        "<b>cancelada</b>, con efectos retroactivos a su fecha de baja." +
        "</p>" +

        "<p style='text-align: justify; font-family: arial; font-size: 14px;'>" +
        "Asimismo, se comparten las <b>Condiciones Generales de la póliza</b> (archivo anexo), vigentes hasta el " +
        "<b>31 de diciembre del presente año</b>." +
        "</p>" +

        "<p style='text-align: justify; font-family: arial; font-size: 14px;'>" +
        "<b>Contacto Banorte:</b><br/>" +
        "Lic. Luis Alberto Vázquez González<br/>" +
        "Teléfonos: 55 5661-0128, 55 5661-0234<br/>" +
        "Correo: <a href='mailto:luis.vazquez@banorte.com'>luis.vazquez@banorte.com</a>" +
        "</p>" +

        "<p style='text-align: justify; font-family: arial; font-size: 14px;'>Sin más por el momento, le envío un cordial saludo.</p>" +

        "<p style='text-align: center'><b>Atentamente</b></p>" +
        "<p style='text-align: center; font-family: arial; font-size: 16px;'><b>Dirección de Seguros</b></p>";

            return htmlBody;
        }
    }
}
