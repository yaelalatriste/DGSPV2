namespace DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora
{
    public class PrimaPotenciadaDto
    {
        public string SumaAsegurada { get; set; } = string.Empty;

        public decimal Titular019 { get; set; }
        public decimal Titular2059 { get; set; }
        public decimal Titular6069 { get; set; }
        public decimal Titular7089 { get; set; }

        public decimal Conyuge019 { get; set; }
        public decimal Conyuge2059 { get; set; }
        public decimal Conyuge6069 { get; set; }
        public decimal Conyuge7089 { get; set; }

        public decimal Hijo019 { get; set; }
        public decimal Hijo2059 { get; set; }

        public decimal HijoM25 { get; set; }
        public decimal Ascendentes { get; set; }
    }
}
