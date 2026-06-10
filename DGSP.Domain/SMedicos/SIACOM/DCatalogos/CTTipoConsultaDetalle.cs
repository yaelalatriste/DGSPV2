namespace DGSP.Domain.SMedicos.SIACOM.DCatalogos;

public class CTTipoConsultaDetalle
{
    public int FiIdTipoConsultaDetalle { get; set; }

    public int FiIdTipoConsulta { get; set; }

    public string FcTipoConsultaDetalle { get; set; } = null!;

    public bool FlEstatus { get; set; }

    public int FiExpAlta { get; set; }

    public DateTime FdFchAlta { get; set; }

    public int? FiExpAct { get; set; }

    public DateTime? FdFchAct { get; set; }
}
