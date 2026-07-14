namespace DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora
{
    public class PrimaOpMMSExtraBaseDto
    {
        public short? FiIdSAPotenciada { get; set; }
        public string Parentesco { get; set; } = string.Empty;
        public string SumaPotenciada { get; set; } = string.Empty;
        public decimal MontoPrima { get; set; }
        public int? FiTpoExtraPrima { get; set; }
        public int? FiTpoCAdicional { get; set; }
    }
}
