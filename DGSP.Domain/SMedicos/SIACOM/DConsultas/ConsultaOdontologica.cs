namespace DGSP.Domain.SMedicos.SIACOM.DConsultas;

public class ConsultaOdontologica
{
    public int FiIdConsultaOdontologica { get; set; }

    public int FiIdConsultaOdontologicaPadre { get; set; }

    public int FiExpMedico { get; set; }

    public int FiIdConsultorio { get; set; }

    public int FiExpPaciente { get; set; }

    public int CveArea { get; set; }

    public int FiIdTipoConsulta { get; set; }

    public DateTime FdFchConsulta { get; set; }

    public DateTime FdFchAlta { get; set; }

    public string FcPlan { get; set; } = null!;

    public string FcReceta { get; set; } = null!;

    public int? FiUsrAct { get; set; }

    public DateTime? FdFchAct { get; set; }

    public string FcEvolucion { get; set; } = null!;

    public int? FiEdad { get; set; }

    public string FcSexo { get; set; } = null!;

    public string FcPresion { get; set; } = null!;

    public short? FiOxigeno { get; set; } 

    public string FcFrecuenca { get; set; } = null!;

    public int? FiExpEnfermera { get; set; }
}
