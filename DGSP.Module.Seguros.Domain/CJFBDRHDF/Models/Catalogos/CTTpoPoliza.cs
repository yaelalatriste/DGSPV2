namespace DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;

public class CTTpoPoliza
{
    public byte FiIdTpoPol { get; set; }
    public string FcDescTpoPol { get; set; } = null!;
    public string? FcNomCortoTpoPol { get; set; }
    public DateTime FdFchAltaTpoPol { get; set; }
    public int FiUsrAltaTpoPol { get; set; }
    public DateTime? FdFchModTpoPol { get; set; }
    public int? FiUsrModTpoPol { get; set; }
    public bool FlStatusTpoPol { get; set; }
}
