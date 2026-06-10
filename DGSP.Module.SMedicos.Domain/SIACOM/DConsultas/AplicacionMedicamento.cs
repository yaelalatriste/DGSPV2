namespace DGSP.Module.SMedicos.Domain.SIACOM.DConsultas;

public class AplicacionMedicamento
{
    public int FiIdAplicaMedicamentos { get; set; }

    public int FiIdAplicaMedicamentosPadre { get; set; }

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

    public string FcNombreMedico { get; set; } = null!;

    public string FcCedulaMedico { get; set; } = null!;

    public string FcMedicamento { get; set; } = null!;

    public short FiViaAplicacion { get; set; }
}
