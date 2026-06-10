namespace DGSP.Module.SMedicos.Domain.SIACOM.DCatalogos;

public class CTTipoConsulta
{
    public int FiIdTipoConsulta { get; set; }

    public string FcTipoConsulta { get; set; } = null!;

    public short FiEspecialidad { get; set; }

    public bool FlEstatus { get; set; }

    public int FiExpAlta { get; set; }

    public DateTime FdFchAlta { get; set; }

    public int? FiExpAct { get; set; }

    public DateTime? FdFchAct { get; set; }
}
