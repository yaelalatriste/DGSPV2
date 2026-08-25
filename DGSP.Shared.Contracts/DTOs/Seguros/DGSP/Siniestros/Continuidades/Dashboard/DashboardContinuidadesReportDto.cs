using System.Text.Json.Serialization;

namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Dashboard;

public sealed class DashboardContinuidadesReportDto
{
    [JsonPropertyName("periodo")]
    public string? Periodo { get; set; }

    [JsonPropertyName("estatus")]
    public List<EstatusContinuidadReportDto> Estatus { get; set; } = new();

    [JsonPropertyName("semanas")]
    public List<SemanaContinuidadReportDto> Semanas { get; set; } = new();
}

public sealed class EstatusContinuidadReportDto
{
    [JsonPropertyName("abreviacion")] public string? Abreviacion { get; set; }
    [JsonPropertyName("nombre")] public string Nombre { get; set; } = string.Empty;
    [JsonPropertyName("total")] public int Total { get; set; }
    [JsonPropertyName("operativo")] public int Operativo { get; set; }
    [JsonPropertyName("mandoMedio")] public int MandoMedio { get; set; }
    [JsonPropertyName("mandoSuperior")] public int MandoSuperior { get; set; }
    [JsonPropertyName("fondoHexadecimal")] public string? FondoHexadecimal { get; set; }
}

public sealed class SemanaContinuidadReportDto
{
    [JsonPropertyName("numero")] public int Numero { get; set; }
    [JsonPropertyName("descripcion")] public string Descripcion { get; set; } = string.Empty;
    [JsonPropertyName("ingresadasOperativo")] public int IngresadasOperativo { get; set; }
    [JsonPropertyName("ingresadasMandoMedio")] public int IngresadasMandoMedio { get; set; }
    [JsonPropertyName("ingresadasMandoSuperior")] public int IngresadasMandoSuperior { get; set; }
}

public sealed class ResumenEstatusRdlcRow
{
    public string Abreviacion { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public int Total { get; set; }
    public int Operativo { get; set; }
    public int MandoMedio { get; set; }
    public int MandoSuperior { get; set; }
    public decimal Porcentaje { get; set; }
    public decimal PorcentajeOperativo { get; set; }
    public decimal PorcentajeMandoMedio { get; set; }
    public decimal PorcentajeMandoSuperior { get; set; }
}

public sealed class SemanaRdlcRow
{
    public int Numero { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public int Operativo { get; set; }
    public int MandoMedio { get; set; }
    public int MandoSuperior { get; set; }
    public int Total { get; set; }
    public decimal PorcentajeOperativo { get; set; }
    public decimal PorcentajeMandoMedio { get; set; }
    public decimal PorcentajeMandoSuperior { get; set; }
}

public sealed class ImagenesRdlcRow
{
    public byte[]? GraficaSemanal { get; set; }
    public byte[]? GraficaEstatus { get; set; }
    public byte[]? GraficaComparativo { get; set; }
}
