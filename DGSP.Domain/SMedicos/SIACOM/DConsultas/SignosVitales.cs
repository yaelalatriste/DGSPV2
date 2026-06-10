namespace DGSP.Domain.SMedicos.SIACOM.DConsultas;
public class SignosVitales
{
    public int FiIdSignosVitales { get; set; }

    public int FiIdTomaSignosPadre { get; set; }

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
    public string FcPresion { get; set; } = null!;

    public short? FiOxigeno { get; set; }

    public string FcFrecuenca { get; set; } = null!;
}
