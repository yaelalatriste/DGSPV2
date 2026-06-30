namespace DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;

public class CTVigenciaOpMMS
{
    public short FiIdRegVig { get; set; }
    public byte? FiIdTpoPol { get; set; }
    public byte FiIdRegAseg { get; set; }
    public string FcCveAsegVig { get; set; } = null!;
    public DateTime FdFchIniVig { get; set; }
    public DateTime FdFchFinVig { get; set; }
    public DateTime FdFchAltaVig { get; set; }
    public int FiUsrAltaVig { get; set; }
    public short FiIdRegVigAnt { get; set; }
    public string FcVigencia { get; set; } = null!;
}
