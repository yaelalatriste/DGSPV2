namespace DGSP.Domain.SMedicos.SIACOM.DConsultas;

public partial class TomaEstudio
{
    public int FiIdTomaEstudios { get; set; }

    public int FiIdTomaEstudiosPadre { get; set; }

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

    public bool? FbGlucosa { get; set; }

    public string FcGlucosa { get; set; } = null!;

    public bool? FbColesterol { get; set; }

    public string FcColesterol { get; set; } = null!;

    public bool? FbTrigliceridos { get; set; }

    public string FcTrligliceridos { get; set; } = null!;
}
