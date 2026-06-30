namespace DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora
{
    public class PrimaPotenciadaDto
    {
        public string SumaAsegurada { get; set; } = string.Empty;
        public decimal Titular { get; set; }
        public decimal Conyuge { get; set; }
        public decimal Hijo { get; set; }
        public decimal HijoMayor25 { get; set; }
        public decimal Ascendientes { get; set; }
    }
}
