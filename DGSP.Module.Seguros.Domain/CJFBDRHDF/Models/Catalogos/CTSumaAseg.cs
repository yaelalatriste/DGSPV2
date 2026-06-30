namespace DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;

public class CTSumaAseg
{
    public byte FiIdRegSA { get; set; }
    public string FcDescSumAseg { get; set; } = null!;
    public DateTime FdFchAltaSumAseg { get; set; }
    public int FiUsrAltaSumAseg { get; set; }
    public DateTime? FdFchModSumAseg { get; set; }
    public int? FiUsrModSumAseg { get; set; }
    public bool? FlStatusSumAseg { get; set; }
}
