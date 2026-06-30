namespace DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;

public class CTParentesco
{
    public byte FiIdParent { get; set; }
    public string FcDescParent { get; set; } = null!;
    public string? FcCveParentAnt { get; set; }
    public DateTime FdFchAltaParent { get; set; }
    public int FiUsrAltaParent { get; set; }
    public DateTime? FdFchModParent { get; set; }
    public int? FiUsrModParent { get; set; }
    public bool? FlStatusParent { get; set; }
    public bool? FlEstatusSGMM { get; set; }
    public bool? FlEstatusSVeh { get; set; }
}
