namespace DGSP.Domain.SMedicos.SIACOM.DConsultas;

public class RevisionEnfermeria
{
    public int FiIdRevisionEnfermeria { get; set; }

    public int FiIdRevisionEnfermeriaPadre { get; set; }

    public int FiTpoPaciente { get; set; }

    public int FiExpEnfermera { get; set; }

    public int FiIdConsultorio { get; set; }

    public int FiExpPaciente { get; set; }

    public int CveArea { get; set; }

    public int FiIdTipoServicio { get; set; }

    public DateTime FdFchConsulta { get; set; }

    public DateTime FdFchAlta { get; set; }

    public string FcPaciente { get; set; } = null!;

    public string FcNombre { get; set; } = null!;

    public int? FiEdad { get; set; }

    public string FcSexo { get; set; } = null!;

    public int? FiUsrAct { get; set; }

    public DateTime? FdFchAct { get; set; }

    public string FcSintomatologia { get; set; } = null!;

    public string FcDiagnostico { get; set; } = null!;

    public string FcTratamiento { get; set; } = null!;

    public string FcPresion { get; set; } = null!;

    public short? FiOxigeno { get; set; }

    public string FcFrecuencia { get; set; } = null!;
}
