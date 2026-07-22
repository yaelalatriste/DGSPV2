using DGSP.Module.Catalogos.Application.Services.Generales;
using DGSP.Module.Catalogos.Application.Services.SMedicos;
using DGSP.Module.DGRH.Application.Services.RH;
using DGSP.Module.Estatus.Application.Services.Continuidades;
using DGSP.Module.Seguros.Application.Services.DGSP.Continuidades;
using DGSP.Shared.Contracts.Commands.Seguros.Movimientos.Calculadora;
using DGSP.Shared.Contracts.DTOs.ExternalServices.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;
using System.Text;

namespace DGSP.API.Controllers.Seguros.Queries.DGSP.Movimientos.Calculadora
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/seguros/movimientos/[controller]")]
    public class CorreosCalculadoraController : ControllerBase
    {
        private readonly ICTEntregableService _ctEntregable;
        private readonly ICTVariableMedicaService _variables;
        private readonly IEstatusContinuidadesService _estatusContinuidades;
        private readonly IContinuidadService _continuidad;
        private readonly IContactoContinuidadService _contacto;
        private readonly IEntregableContinuidadService _entregables;
        private readonly IEmpleadoService _empleado;
        private readonly IWebHostEnvironment _environment;

        public CorreosCalculadoraController(IContinuidadService continuidad,IContactoContinuidadService contacto,
            IEntregableContinuidadService entregables,IEmpleadoService empleado,ICTEntregableService ctEntregable,
            IEstatusContinuidadesService estatusContinuidades,IWebHostEnvironment environment,ICTVariableMedicaService variables)
        {
            _continuidad = continuidad;
            _contacto = contacto;
            _entregables = entregables;
            _empleado = empleado;
            _ctEntregable = ctEntregable;
            _estatusContinuidades = estatusContinuidades;
            _environment = environment;
            _variables = variables;
        }

        [HttpPost]
        [Route("enviarCotizacionCorreo")]
        public async Task<IActionResult> EnviarCorreoPoliza([FromBody] ResultadoCotizacionSgmmCommand command)
        {
            try
            {
                if (command == null)
                {
                    return BadRequest(new
                    {
                        mensaje = "No se recibió la información de la cotización."
                    });
                }

                if (string.IsNullOrWhiteSpace(command.Correo))
                {
                    return BadRequest(new
                    {
                        mensaje = "Debe proporcionar el correo del destinatario."
                    });
                }

                if (command.Detalle == null || !command.Detalle.Any())
                {
                    return BadRequest(new
                    {
                        mensaje = "La cotización no contiene detalles para enviar."
                    });
                }

                var correosOcultos = new List<string>
                {
                    "jymiranda@oaj.gob.mx",
                    "aavalosf@oaj.gob.mx",
                    "mehernandezc@oaj.gob.mx",
                    "eetrujillo@oaj.gob.mx",
                };

                var destinatarios = new List<string>
                {
                    command.Correo.Trim()
                };

                string cuerpoCorreo = GetMensajeCotizacionSGMMPoliza(command);

                var emailRequest = new EmailRequestDto
                {
                    Provider = 0,
                    From = "notificacionsgmm@oaj.gob.mx",
                    FromDisplayName = "Cotización de Póliza SGMM",
                    To = destinatarios,
                    Subject = "Cotización de opciones de Suma Asegurada – Seguro de Gastos Médicos Mayores",
                    cc = new List<string>(),
                    bcc = correosOcultos,
                    Body = cuerpoCorreo,
                    IsBodyHtml = true,
                    importance = 1
                };

                return Ok(emailRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = "Ocurrió un error al generar la cotización.",
                    detalle = ex.Message
                });
            }
        }

        private string GetMensajeCotizacionSGMMPoliza(ResultadoCotizacionSgmmCommand command)
        {
            var detalle = command.Detalle ?? new();

            bool mostrarTitular = detalle.Any(x => x.Titular != 0);

            bool mostrarConyuge = detalle.Any(x => x.Conyuge != 0);

            bool mostrarHijo019 = detalle.Any(x => x.Hijo019 != 0 && command.Datos.CantidadHijo019 != 0);

            bool mostrarHijo2024 = detalle.Any(x => x.Hijo2024 != 0 && command.Datos.CantidadHijo2024 != 0);

            bool mostrarHijoMayor25 = detalle.Any(x => x.HijoMayor25 != 0 && command.Datos.CantidadHijoMayor25 != 0);

            bool mostrarAscendientes = detalle.Any(x => x.Ascendientes != 0 && command.Datos.CantidadAscendientes != 0);

            string firmaHtml =
                "<table role='presentation' cellpadding='0' cellspacing='0' border='0' " +
                    "style='border-collapse:collapse;width:100%;max-width:620px;font-family:Arial,sans-serif;margin:10px auto 0 auto;'>" +
                "<tr>" +
                    "<td style='width:120px;border-right:3px solid #5d6474;padding:0 10px 0 0;vertical-align:middle;white-space:nowrap;'>" +
                        "<span style='font-size:16px;'>🗨</span> " +
                        "<span style='font-size:18px;font-weight:bold;line-height:18px;'>Informes</span>" +
                    "</td>" +
                    "<td style='padding-left:10px;vertical-align:top;'>" +
                        "<div style='font-size:15px;font-weight:bold;line-height:12px;margin:0;padding:0;'>CD. México y zona metropolitana</div>" +
                        "<div style='font-size:12px;line-height:12px;margin:3px 0 0 0;padding:0;'>Dirección de Seguros ubicada en el Edificio Picacho Ajusco No. 170, 3er piso ala “A”,<br>"+
                            "55 5449 9500 exts. 2673, 2076, 2075, 2003 y 2670, Red #318<br>" +
                            "o bien mediante: " +
                            "<a href='mailto:sea_dgsp_seguros@oaj.gob.mx' style='color:#0066cc;text-decoration:none;'>sea_dgsp_seguros@oaj.gob.mx</a>" +
                        "</div>" +
                        "<div style='font-size:15px;font-weight:bold;line-height:12px;margin:8px 0 0 0;padding:0;'>" +
                            "Entidades federativas" +
                        "</div>" +
                        "<div style='font-size:12px;line-height:12px;margin:2px 0 0 0;padding:0;'>" +
                            "Administraciones Regionales o Delegaciones Administrativas de tu localidad." +
                        "</div>" +
                    "</td>" +
                "</tr>" +
                "</table>";

            string estiloEncabezado =
                "border:1px solid #8c8c8c;" +
                "padding:8px 6px;" +
                "background-color:#1f4e78;" +
                "color:#ffffff;" +
                "font-family:Arial, sans-serif;" +
                "font-size:11px;" +
                "font-weight:bold;" +
                "text-align:center;" +
                "vertical-align:middle;" +
                "white-space:nowrap;";

            string estiloCelda =
                "border:1px solid #b7b7b7;" +
                "padding:7px 6px;" +
                "font-family:Arial, sans-serif;" +
                "font-size:12px;" +
                "text-align:right;" +
                "vertical-align:middle;" +
                "white-space:nowrap;";

            string estiloCeldaTexto =
                "border:1px solid #b7b7b7;" +
                "padding:7px 6px;" +
                "font-family:Arial, sans-serif;" +
                "font-size:12px;" +
                "text-align:center;" +
                "vertical-align:middle;" +
                "white-space:nowrap;";

            string encabezados = ConstruirEncabezados(command.Datos ,estiloEncabezado,mostrarTitular,mostrarConyuge,mostrarHijo019,mostrarHijo2024,
                mostrarHijoMayor25,mostrarAscendientes);

            string filas = ConstruirFilas(detalle,estiloCelda,estiloCeldaTexto,mostrarTitular,mostrarConyuge,mostrarHijo019,
                mostrarHijo2024,mostrarHijoMayor25,mostrarAscendientes);

            string resumenPersonas = ConstruirResumenPersonas(command.Datos);

            string htmlBody =
                "<div style='" +
                    "font-family:Arial, sans-serif;" +
                    "font-size:14px;" +
                    "color:#000000;" +
                "'>" +

                    "<p style='" +
                        "text-align:right;" +
                        "font-size:14px;" +
                        "font-weight:bold;" +
                    "'>" +
                        "Ciudad de México a " +
                        DateTime.Now.Day + " de " +
                        DateTime.Now.ToString(
                            "MMMM",
                            CultureInfo.CreateSpecificCulture("es-MX")
                        ) +
                        " de " +
                        DateTime.Now.Year +
                    "</p>" +

                    "<p style='margin-bottom:22px;'>" +
                        "<span style='" +
                            "background-color:#ffff00;" +
                            "font-weight:bold;" +
                        "'>" +
                            "Esta cuenta de correo es unicamente para envío de información: favor de no responder este mensaje. " +
                        "</span>" +
                    "</p>" +

                    "<p style='font-weight:bold;'>" +
                        "Estimada Persona Servidora Pública:" +
                    "</p>" +

                    "<p style='" +
                        "text-align:justify;" +
                        "line-height:1.5;" +
                    "'>" +
                        "En atención a su solicitud, se remite la cotización del <b>Seguro de Gastos Médicos Mayores (SGMM)</b>, " +
                        "elaborada con base en la información proporcionada.<br><br>" +
                        "En la tabla encontrará las diferentes opciones de <b>Suma Asegurada</b>, así como el <b>costo total de la póliza</b>" +
                        " y el <b>descuento quincenal</b> que corresponde en cada caso, para que pueda elegir la alternativa " +
                        "que mejor se adapte a sus necesidades:" +
                    "</p>" +

                    resumenPersonas +

                    "<div style='" +
                        "width:100%;" +
                        "overflow-x:auto;" +
                        "margin:20px 0;" +
                    "'>" +

                        "<table role='presentation' " +
                            "cellpadding='0' " +
                            "cellspacing='0' " +
                            "style='" +
                                "width:100%;" +
                                "border-collapse:collapse;" +
                                "border:1px solid #8c8c8c;" +
                                "font-family:Arial, sans-serif;" +
                            "'>" +

                            "<thead>" +
                                "<tr>" +
                                    encabezados +
                                "</tr>" +
                            "</thead>" +

                            "<tbody>" +
                                filas +
                            "</tbody>" +

                        "</table>" +

                    "</div>" +

                    "<p style='" +
                        "text-align:justify;" +
                        "line-height:1.5;" +
                    "'>" +
                        "Si decide <b>incrementar su Suma Asegurada</b>, podrá solicitarlo en cualquier momento del año, con base a las disposiciones " +
                        "vigentes. En caso de <b>disminuir su Suma Asegurada</b>, dicha modificación únicamente podrá realizarse durante los " +
                        "periodos establecidos, de conformidad con las Condiciones de la Póliza." +
                    "</p>" +
                    "<p style='" +
                        "text-align:justify;" +
                        "line-height:1.5;" +
                        "margin-bottom:10px;" +
                    "'>" +
                        "Para cualquier duda o asesoría, comuniquese con el personal de la Dirección de Seguros. " +
                    "</p>" +

                    "<p style='" +
                        "text-align:justify;" +
                        "line-height:1.5;" +
                    "'>" +
                        "Sin más por el momento, se envía un cordial saludo." +
                    "</p>" +

                    "<p style='" +
                        "font-size:16px;" +
                        "text-align:left;" +
                        "margin-top:30px;" +
                        "margin-bottom:4px;" +
                    "'>" +
                        "<b>Atentamente</b>" +
                    "</p>" +

                    "<p style='" +
                        "text-align:left;" +
                        "font-size:16px;" +
                        "margin-top:0;" +
                    "'>" +
                        "<b>Dirección de Seguros</b>" +
                    "</p>" +
                    firmaHtml+
                "</div>";

            return htmlBody;
        }

        private static string ConstruirEncabezados(
            DatosCotizacionSgmmCommand datos,
            string estiloEncabezado,
            bool mostrarTitular,
            bool mostrarConyuge,
            bool mostrarHijo019,
            bool mostrarHijo2024,
            bool mostrarHijoMayor25,
            bool mostrarAscendientes)
        {
            var encabezados = new StringBuilder();

            encabezados.Append(
                $"<th style='{estiloEncabezado}'>UMA</th>");

            encabezados.Append(
                $"<th style='{estiloEncabezado}'>" +
                "Suma<br>Asegurada" +
                "</th>");

            if (mostrarTitular)
            {
                encabezados.Append(
                    $"<th style='{estiloEncabezado}'>" +
                    "Titular<br>" + 
                    datos.EdadTitular+" años <br>" + 
                    datos.CantidadTitular+" persona(s)"+
                    "</th>");
            }

            if (mostrarConyuge)
            {
                encabezados.Append(
                    $"<th style='{estiloEncabezado}'>" +
                    "Cónyuge<br>" +
                    datos.EdadConyuge + " años <br>" +
                    datos.CantidadConyuge + " persona(s)" +
                    "</th>");
            }

            if (mostrarHijo019)
            {
                encabezados.Append(
                    $"<th style='{estiloEncabezado}'>" +
                    "Hijo(a)<br>0-19 años<br>" + 
                    datos.CantidadHijo019 + " persona(s)" +
                    "</th>");
            }

            if (mostrarHijo2024)
            {
                encabezados.Append(
                    $"<th style='{estiloEncabezado}'>" +
                    "Hijo(a)<br>20-24 años<br>" +
                    datos.CantidadHijo2024 + " persona(s)" +
                    "</th>");
            }

            if (mostrarHijoMayor25)
            {
                encabezados.Append(
                    $"<th style='{estiloEncabezado}'>" +
                    "Hijo(a)<br>mayor de 25 años<br>" + 
                    datos.CantidadHijoMayor25 + " persona(s)" +
                    "</th>");
            }

            if (mostrarAscendientes)
            {
                encabezados.Append(
                    $"<th style='{estiloEncabezado}'>" +
                    "Ascendente(s)<br>" + "(" + datos.CantidadAscendientes + " persona(s))" +
                    "</th>");
            }

            encabezados.Append(
                $"<th style='{estiloEncabezado}'>" +
                "Costo Anual de la <br>Póliza" +
                "</th>");

            encabezados.Append(
                $"<th style='{estiloEncabezado}'>" +
                "Descuento<br>Quincenal" +
                "</th>");

            return encabezados.ToString();
        }

        private static string ConstruirFilas(
            IEnumerable<DetalleCotizacionSgmmCommand> detalle,
            string estiloCelda,
            string estiloCeldaTexto,
            bool mostrarTitular,
            bool mostrarConyuge,
            bool mostrarHijo019,
            bool mostrarHijo2024,
            bool mostrarHijoMayor25,
            bool mostrarAscendientes)
        {
            var filas = new StringBuilder();

            foreach (var item in detalle)
            {
                filas.Append(
                    "<tr style='background-color:#ffffff;'>");

                /*
                 * SumaAsegurada representa las UMA,
                 * por lo que no se formatea como moneda.
                 */
                filas.Append(
                    ConstruirCeldaTexto(
                        item.SumaAsegurada,
                        estiloCeldaTexto
                    )
                );

                filas.Append(
                    ConstruirCeldaMoneda(
                        item.MontoSumaAsegurada,
                        estiloCelda
                    )
                );

                if (mostrarTitular)
                {
                    filas.Append(
                        ConstruirCeldaMoneda(
                            item.Titular,
                            estiloCelda
                        )
                    );
                }

                if (mostrarConyuge)
                {
                    filas.Append(
                        ConstruirCeldaMoneda(
                            item.Conyuge,
                            estiloCelda
                        )
                    );
                }

                if (mostrarHijo019)
                {
                    filas.Append(
                        ConstruirCeldaMoneda(
                            item.Hijo019,
                            estiloCelda
                        )
                    );
                }

                if (mostrarHijo2024)
                {
                    filas.Append(
                        ConstruirCeldaMoneda(
                            item.Hijo2024,
                            estiloCelda
                        )
                    );
                }

                if (mostrarHijoMayor25)
                {
                    filas.Append(
                        ConstruirCeldaMoneda(
                            item.HijoMayor25,
                            estiloCelda
                        )
                    );
                }

                if (mostrarAscendientes)
                {
                    filas.Append(
                        ConstruirCeldaMoneda(
                            item.Ascendientes,
                            estiloCelda
                        )
                    );
                }

                filas.Append(
                    ConstruirCeldaMoneda(
                        item.TotalPoliza,
                        estiloCelda,
                        negrita: true
                    )
                );

                filas.Append(
                    ConstruirCeldaMoneda(
                        item.DescuentoQuincenal,
                        estiloCelda,
                        negrita: true
                    )
                );

                filas.Append("</tr>");
            }

            return filas.ToString();
        }

        private static string ConstruirCeldaMoneda(
            decimal valor,
            string estilo,
            bool negrita = false)
        {
            string estiloFinal = estilo;

            if (negrita)
            {
                estiloFinal += "font-weight:bold;";
            }

            string valorFormateado = valor.ToString(
                "C2",
                CultureInfo.GetCultureInfo("es-MX")
            );

            return
                $"<td style='{estiloFinal}'>" +
                    WebUtility.HtmlEncode(valorFormateado) +
                "</td>";
        }

        private static string ConstruirCeldaTexto(
            string? valor,
            string estilo)
        {
            return
                $"<td style='{estilo}'>" +
                    WebUtility.HtmlEncode(valor ?? string.Empty) +
                "</td>";
        }

        private static string ConstruirResumenPersonas(
            DatosCotizacionSgmmCommand? datos)
        {
            if (datos == null)
            {
                return string.Empty;
            }

            /*
             * Ajusta los nombres de las propiedades de acuerdo
             * con tu DatosCotizacionSgmmCommand real.
             *
             * Si todavía no quieres mostrar este resumen,
             * puedes retornar string.Empty.
             */

            return string.Empty;
        }
    }
}