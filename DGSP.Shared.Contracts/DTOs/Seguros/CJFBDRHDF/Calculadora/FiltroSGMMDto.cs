namespace DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora
{
    public class FiltroSGMMDto
    {
        public int Anio { get; set; }
        public int TipoPoliza { get; set; }
        public int Quincenas { get; set; }
        public int SumaBasica { get; set; }
        public int IQ { get; set; }
        public bool Nivel { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}
