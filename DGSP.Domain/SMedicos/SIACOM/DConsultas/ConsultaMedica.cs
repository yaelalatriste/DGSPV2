namespace DGSP.Domain.SMedicos.SIACOM.DConsultas;

public class ConsultaMedica
{
    public int FiIdConsultaMedica { get; set; }

    public int FiIdConsultaMedicaPadre { get; set; }

    public int FiTpoPaciente { get; set; }

    public int FiExpMedico { get; set; }

    public int FiIdConsultorio { get; set; }

    public int FiExpPaciente { get; set; }

    public int CveArea { get; set; }

    public int FiIdTipoConsulta { get; set; }

    public int? FiIdTipoConsultaDetalle { get; set; }

    public DateTime FdFchConsulta { get; set; }

    public DateTime FdFchAlta { get; set; }

    public string? FcPaciente { get; set; }

    public string FcSubjetivo { get; set; } = null!;

    public string FcObjetivo { get; set; } = null!;

    public string FcLaboratorio { get; set; } = null!;

    public string FcAtencion { get; set; } = null!;

    public string FcPlan { get; set; } = null!;

    public string FcReceta { get; set; } = null!;

    public string FcResultado { get; set; } = null!;

    public string FcEstadoPaciente { get; set; } = null!;

    public string FcNombre { get; set; } = null!;

    public int? FiEdad { get; set; }

    public string FcSexo { get; set; } = null!;

    public int? FiUsrAct { get; set; }

    public DateTime? FdFchAct { get; set; }

    public string FcEvolucion { get; set; } = null!;

    public int? FiIdTipoTraslado { get; set; }

    public bool? FlRevision { get; set; }

    public string FcPresion { get; set; } = null!;

    public short? FiOxigeno { get; set; }

    public string FcFrecuenca { get; set; } = null!;

    public int? FiExpEnfermera { get; set; }
}
