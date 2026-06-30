namespace DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Calculadora;

public class PrimasOpMMS
{
    public int FiIdPrim { get; set; }
    public short FiAnio { get; set; }
    public byte? FiIdParent { get; set; }
    public byte? FiIdSAOrigen { get; set; }
    public byte? FiIdSAPotenciada { get; set; }
    public decimal FnMtoPrim { get; set; }
    public DateTime? FdFchIniVigPrim { get; set; }
    public DateTime? FdFchFinVigPrim { get; set; }
    public DateTime FdFchAltaPrim { get; set; }
    public int FiUsrAltaPrim { get; set; }
    public DateTime? FdFchModPrim { get; set; }
    public int? FiUsrModPrim { get; set; }
    public bool FlStatusPrim { get; set; }
    public bool FlIndBasicaPrim { get; set; }
    public short? FiIdIQ { get; set; }
    public int? FiTpoExtraPrima { get; set; }
    public int? FiTpoCAdicional { get; set; }
    public short? FiIdRegVig { get; set; }
    public int? FiCveDed { get; set; }
}
