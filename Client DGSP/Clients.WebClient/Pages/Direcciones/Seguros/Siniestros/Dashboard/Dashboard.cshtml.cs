using DGSP.Gateway.Proxy.Queries.Estatus.Continuidades;
using DGSP.Gateway.Proxy.Queries.Modulos;
using DGSP.Gateway.Proxy.Queries.Permisos;
using DGSP.Gateway.Proxy.Queries.Usuarios;
using DGSP.Gateway.Proxy.Services.Dashboards;
using DGSP.Shared.Contracts.DTOs.Estatus.Continuidades;
using DGSP.Shared.Contracts.DTOs.Modulos;
using DGSP.Shared.Contracts.DTOs.Permisos;
using DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Dashboard;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

public class DashboardModel : PageModel
{

    private readonly IUsuarioProxy _usuarios;
    private readonly IModuloProxy _modulo;
    private readonly IPermisoProxy _permisos;
    private readonly IDashboardContinuidadesService _dashboardService;
    private readonly IQEstatusContinuidadProxy _estatusContinuidad;
    private readonly IWebHostEnvironment _environment;

    public DashboardModel(IUsuarioProxy usuarios, IModuloProxy modulo, IPermisoProxy permisos, IDashboardContinuidadesService dashboardService, 
        IQEstatusContinuidadProxy estatusContinuidad, IWebHostEnvironment environment)
    {
        _usuarios = usuarios;
        _modulo = modulo;
        _permisos = permisos;
        _dashboardService = dashboardService;
        _estatusContinuidad = estatusContinuidad;
        _environment = environment;
    }

    public int AnioActual { get; private set; }
    public int MesActual { get; private set; }
    public List<EstatusContinuidadDto> Estatus { get; set; }
    public ModuloDto Modulo { get; set; }
    public SubmoduloDto Submodulo { get; set; }
    public OpcionDto Opcion { get; set; }
    public List<PermisoUsuarioDto> Permisos { get; set; }

    public async Task OnGet(int moduloId, int submoduloId, int opcionId)
    {
        string usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        Permisos = await _permisos.GetPermisosByModuloUsuario(usuario, moduloId);
        if (Permisos.Where(p => p.Permiso.Nombre.Equals("Ver")).Count() != 0)
        {
            var hoy = DateTime.Today;

            AnioActual = hoy.Year;
            MesActual = hoy.Month;
            Estatus = await _estatusContinuidad.GetAllEstatus();
        }
    }

    public async Task<IActionResult> OnGetDatosAsync(int anio,int mes,string? tipoPersonal,int? estatusId)
    {
        try
        {
            var filtro = new DashboardFiltroDto
            {
                Anio = anio,
                Mes = mes,
                TipoPersonal = string.IsNullOrWhiteSpace(tipoPersonal) ? null : tipoPersonal,
                EstatusId = estatusId
            };
            
            var resultado = await _dashboardService.ObtenerDashboardAsync(filtro);

            return new JsonResult(resultado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(
                new
                {
                    mensaje = ex.Message
                });
        }
    }

    public IActionResult OnPostExportarPdf(string dashboardJson,string periodo,string tipoPersonal,string? graficaSemanalBase64,
        string? graficaEstatusBase64,string? graficaComparativoBase64)
    {
        if (string.IsNullOrWhiteSpace(dashboardJson))
            return BadRequest("No se recibió información para generar el reporte.");

        var dashboard = JsonSerializer.Deserialize<DashboardContinuidadesReportDto>(dashboardJson,new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (dashboard is null)
            return BadRequest("No fue posible interpretar la información del dashboard.");

        var pdf = GenerarPdf(dashboard,periodo,tipoPersonal);

        var nombrePeriodo = string.Join("_",(periodo ?? "Periodo").Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries)).Replace(' ', '_');
        var nombreArchivo = $"Dashboard_Continuidades_SGMM_{nombrePeriodo}.pdf";

        Response.Headers["Content-Disposition"] = $"inline; filename=\"{nombreArchivo}\"";

        return File(pdf,"application/pdf");
    }

    public byte[] GenerarPdf(DashboardContinuidadesReportDto dashboard,string periodo,string tipoPersonal)
    {
        var estatusValidos = dashboard.Estatus.Where(x => x.Total > 0).ToList();

        var ingresadas = estatusValidos.FirstOrDefault(x => string.Equals(x.Abreviacion, "ING", StringComparison.OrdinalIgnoreCase) || 
                                                            x.Nombre.Contains("ingres", StringComparison.OrdinalIgnoreCase));

        var sumaEstatus = estatusValidos.Sum(x => x.Total);
        var totalBase = ingresadas?.Total > 0 ? ingresadas.Total : sumaEstatus;

        static decimal Pct(int valor, int total) => total > 0 ? Math.Round((decimal)valor / total * 100m, 1) : 0m;

        var resumen = estatusValidos.Select(x => new ResumenEstatusRdlcRow
        {
            Abreviacion = x.Abreviacion ?? string.Empty,
            Nombre = x.Nombre,
            Total = x.Total,
            Operativo = x.Operativo,
            MandoMedio = x.MandoMedio,
            MandoSuperior = x.MandoSuperior,
            Porcentaje = Pct(x.Total, totalBase),
            PorcentajeOperativo = Pct(x.Operativo, x.Total),
            PorcentajeMandoMedio = Pct(x.MandoMedio, x.Total),
            PorcentajeMandoSuperior = Pct(x.MandoSuperior, x.Total)
        }).ToList();

        var semanas = dashboard.Semanas.Select(x =>
        {
            var total = x.IngresadasOperativo + x.IngresadasMandoMedio + x.IngresadasMandoSuperior;
            return new SemanaRdlcRow
            {
                Numero = x.Numero,
                Descripcion = x.Descripcion,
                Operativo = x.IngresadasOperativo,
                MandoMedio = x.IngresadasMandoMedio,
                MandoSuperior = x.IngresadasMandoSuperior,
                Total = total,
                PorcentajeOperativo = Pct(x.IngresadasOperativo, total),
                PorcentajeMandoMedio = Pct(x.IngresadasMandoMedio, total),
                PorcentajeMandoSuperior = Pct(x.IngresadasMandoSuperior, total)
            };
        }).Where(x => x.Total > 0).ToList();

        // Las gráficas del "dashboard PDF" se generan aquí, en servidor.
        DashboardContinuidadesChartRenderer _chartRenderer = new DashboardContinuidadesChartRenderer();
        var imagenes = new List<ImagenesRdlcRow>
        {
            new()
            {
                GraficaSemanal = _chartRenderer.GenerarSemanal(semanas),
                GraficaEstatus = _chartRenderer.GenerarEstatus(resumen),
                GraficaComparativo = _chartRenderer.GenerarComparativo(resumen)
            }
        };

        var rutaReporte = Path.Combine(
            _environment.ContentRootPath,
            "Reportes",
            "DashboardContinuidades.rdlc");

        if (!System.IO.File.Exists(rutaReporte))
            throw new FileNotFoundException("No se encontró el archivo RDLC del dashboard.", rutaReporte);

        using var report = new LocalReport { ReportPath = rutaReporte };
        report.DataSources.Clear();
        report.DataSources.Add(new ReportDataSource("ResumenEstatus", resumen));
        report.DataSources.Add(new ReportDataSource("Semanas", semanas));
        report.DataSources.Add(new ReportDataSource("Imagenes", imagenes));

        report.SetParameters(new[]
        {
            new ReportParameter("TituloInstitucional", "SECRETARÍA EJECUTIVA DE ADMINISTRACIÓN"),
            new ReportParameter("AreaInstitucional", "DIRECCIÓN GENERAL DE SERVICIOS AL PERSONAL"),
            new ReportParameter("TituloReporte", "Reporte de Continuidades del Seguro de Gastos Médicos Mayores"),
            new ReportParameter("Periodo", periodo ?? string.Empty),
            new ReportParameter("TipoPersonal", string.IsNullOrWhiteSpace(tipoPersonal) ? "Todos" : tipoPersonal)
        });

        return report.Render("PDF");
    }

    private static byte[]? ConvertirDataUrlABytes(string? dataUrl)
    {
        if (string.IsNullOrWhiteSpace(dataUrl))
            return null;

        var coma = dataUrl.IndexOf(',');
        var base64 = coma >= 0 ? dataUrl[(coma + 1)..] : dataUrl;

        try
        {
            return Convert.FromBase64String(base64);
        }
        catch (FormatException)
        {
            return null;
        }
    }
}

public sealed class DashboardContinuidadesChartRenderer
{
    private static readonly Color Azul = ColorTranslator.FromHtml("#007BFF");
    private static readonly Color Morado = ColorTranslator.FromHtml("#6F42C1");
    private static readonly Color Verde = ColorTranslator.FromHtml("#008000");
    private static readonly Color GrisTexto = ColorTranslator.FromHtml("#343A40");
    private static readonly Color GrisLinea = ColorTranslator.FromHtml("#E9ECEF");

    public byte[] GenerarSemanal(IReadOnlyList<SemanaRdlcRow> semanas)
    {
        var semana = 1;
        var filas = semanas.Where(x => x.Total >= 0).ToList();
        return GenerarBarrasHorizontales(
            1900,
            Math.Max(720, 180 + filas.Count * 118),
            "Comportamiento semanal",
            filas.Select(x => new GrupoBarras(
                $"Semana {semana++}  |  \n{x.Descripcion}",
                new[]
                {
                    new Barra("", x.Operativo, x.PorcentajeOperativo, Azul),
                    new Barra("", x.MandoMedio, x.PorcentajeMandoMedio, Morado),
                    new Barra("", x.MandoSuperior, x.PorcentajeMandoSuperior, Verde)
                })).ToList());
    }

    public byte[] GenerarComparativo(IReadOnlyList<ResumenEstatusRdlcRow> estatus)
    {
        var filas = estatus.Where(x => x.Total > 0).ToList();
        return GenerarBarrasHorizontales(
            1900,
            Math.Max(900, 190 + filas.Count * 118),
            "Operativo vs Mando Medio vs Mando Superior",
            filas.Select(x => new GrupoBarras(
                x.Nombre,
                new[]
                {
                    new Barra("", x.Operativo, x.PorcentajeOperativo, Azul),
                    new Barra("", x.MandoMedio, x.PorcentajeMandoMedio, Morado),
                    new Barra("", x.MandoSuperior, x.PorcentajeMandoSuperior, Verde)
                })).ToList());
    }

    public byte[] GenerarEstatus(IReadOnlyList<ResumenEstatusRdlcRow> estatus)
    {
        var datos = estatus.Where(x => x.Total > 0).ToList();
        const int width = 1500;
        const int height = 900;

        using var bitmap = new Bitmap(width, height);
        bitmap.SetResolution(144, 144);
        using var g = Graphics.FromImage(bitmap);
        Preparar(g);
        g.Clear(Color.White);

        using var titulo = new Font("Arial", 22, FontStyle.Bold);
        using var normal = new Font("Arial", 15, FontStyle.Regular);
        using var negrita = new Font("Arial", 15, FontStyle.Bold);
        using var porcentajeFont = new Font("Arial", 14, FontStyle.Bold);
        using var tituloBrush = new SolidBrush(GrisTexto);

        g.DrawString("Distribución actual por estatus", titulo, tituloBrush, 45, 35);

        if (datos.Count == 0)
        {
            g.DrawString("Sin datos para el periodo seleccionado.", normal, Brushes.DimGray, 45, 100);
            return ABytes(bitmap);
        }

        var total = datos.Sum(x => x.Total);
        var pie = new Rectangle(70, 135, 610, 610);
        var colores = ObtenerPaleta(datos.Count);
        float inicio = -90f;

        for (var i = 0; i < datos.Count; i++)
        {
            var item = datos[i];
            var pct = total > 0 ? (float)item.Total / total * 100f : 0f;
            var angulo = total > 0 ? (float)item.Total / total * 360f : 0f;

            using var brush = new SolidBrush(colores[i]);
            g.FillPie(brush, pie, inicio, angulo);

            // Sólo escribe dentro de la dona porcentajes suficientemente grandes.
            if (pct >= (datos.Count > 5 ? 7f : 4f))
            {
                var centro = inicio + angulo / 2f;
                var rad = centro * Math.PI / 180d;
                var x = pie.X + pie.Width / 2f + (float)Math.Cos(rad) * 225f;
                var y = pie.Y + pie.Height / 2f + (float)Math.Sin(rad) * 225f;
                var texto = $"{pct:0.0}%";
                var medida = g.MeasureString(texto, porcentajeFont);
                g.DrawString(texto, porcentajeFont, Brushes.White, x - medida.Width / 2f, y - medida.Height / 2f);
            }

            inicio += angulo;
        }

        // Hueco de dona.
        g.FillEllipse(Brushes.White, 220, 285, 310, 310);

        // Leyenda amplia a la derecha para evitar amontonamiento.
        var yLegend = 150f;
        for (var i = 0; i < datos.Count; i++)
        {
            var item = datos[i];
            var pct = total > 0 ? (decimal)item.Total / total * 100m : 0m;
            using var brush = new SolidBrush(colores[i]);
            g.FillRectangle(brush, 760, yLegend + 5, 24, 24);
            g.DrawString(item.Nombre, negrita, tituloBrush, 800, yLegend);
            g.DrawString($"{item.Total:N0} {(item.Total == 1 ? "continuidad" : "continuidades")} ({pct:0.0}%)",
                normal, Brushes.DimGray, 800, yLegend + 31);
            yLegend += 82f;
        }

        return ABytes(bitmap);
    }

    private static byte[] GenerarBarrasHorizontales(
        int width,
        int height,
        string tituloTexto,
        IReadOnlyList<GrupoBarras> grupos)
    {
        using var bitmap = new Bitmap(width, height);
        bitmap.SetResolution(144, 144);
        using var g = Graphics.FromImage(bitmap);
        Preparar(g);
        g.Clear(Color.White);

        using var titulo = new Font("Arial", 22, FontStyle.Bold);
        using var grupoFont = new Font("Arial", 14, FontStyle.Bold);
        using var barraFont = new Font("Arial", 12, FontStyle.Regular);
        using var valorFont = new Font("Arial", 12, FontStyle.Bold);
        using var tituloBrush = new SolidBrush(GrisTexto);
        using var lineaPen = new Pen(GrisLinea, 1);

        g.DrawString(tituloTexto, titulo, tituloBrush, 45, 30);

        if (grupos.Count == 0)
        {
            g.DrawString("Sin datos para el periodo seleccionado.", barraFont, Brushes.DimGray, 45, 95);
            return ABytes(bitmap);
        }

        var maximo = Math.Max(1, grupos.SelectMany(x => x.Barras).Max(x => x.Valor));
        const float labelX = 55;
        const float plotX = 390;
        var plotWidth = width - plotX - 400;
        var y = 105f;

        foreach (var grupo in grupos)
        {
            g.DrawString(grupo.Etiqueta, grupoFont, tituloBrush, labelX, y + 22);

            foreach (var barra in grupo.Barras)
            {
                if (barra.Valor <= 0)
                {
                    y += 31;
                    continue;
                }

                var ancho = Math.Max(3f, plotWidth * barra.Valor / maximo);
                var rect = new RectangleF(plotX, y, ancho, 22);
                using var brush = new SolidBrush(barra.Color);
                g.FillRectangle(brush, rect);

                g.DrawString(barra.Nombre, barraFont, Brushes.DimGray, plotX - 125, y + 1);

                var texto = $"{barra.Valor:N0} {(barra.Valor == 1 ? "continuidad" : "continuidades")} ({barra.Porcentaje:0.0}%)";
                g.DrawString(texto, valorFont, tituloBrush, plotX + ancho + 12, y - 1);
                y += 31;
            }

            g.DrawLine(lineaPen, labelX, y + 5, width - 55, y + 5);
            y += 28;
        }

        // Leyenda inferior.
        var legendY = height - 55f;
        DibujarLeyenda(g, "Operativo", Azul, 520, legendY, barraFont);
        DibujarLeyenda(g, "Mando medio", Morado, 760, legendY, barraFont);
        DibujarLeyenda(g, "Mando superior", Verde, 1025, legendY, barraFont);

        return ABytes(bitmap);
    }

    private static void DibujarLeyenda(Graphics g, string texto, Color color, float x, float y, Font font)
    {
        using var b = new SolidBrush(color);
        g.FillRectangle(b, x, y + 2, 22, 18);
        g.DrawString(texto, font, Brushes.DimGray, x + 32, y);
    }

    private static void Preparar(Graphics g)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
    }

    private static byte[] ABytes(Bitmap bitmap)
    {
        using var ms = new MemoryStream();
        bitmap.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }

    private static Color[] ObtenerPaleta(int cantidad)
    {
        var baseColors = new[]
        {
            "#FFC107", "#007BFF", "#6C757D", "#09599A", "#28A745",
            "#6c757d", "#FF0000", "#EE848E", "#EE848E"
        }.Select(ColorTranslator.FromHtml).ToArray();

        return Enumerable.Range(0, cantidad)
            .Select(i => baseColors[i % baseColors.Length])
            .ToArray();
    }

    private sealed record GrupoBarras(string Etiqueta, IReadOnlyList<Barra> Barras);
    private sealed record Barra(string Nombre, int Valor, decimal Porcentaje, Color Color);
}
