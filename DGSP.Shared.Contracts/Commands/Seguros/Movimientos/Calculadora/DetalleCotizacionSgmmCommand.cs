namespace DGSP.Shared.Contracts.Commands.Seguros.Movimientos.Calculadora
{
    public class DetalleCotizacionSgmmCommand
    {
        public string SumaAsegurada { get; set; } = string.Empty;

        public decimal MontoSumaAsegurada { get; set; }

        public decimal Titular { get; set; }

        public decimal Conyuge { get; set; }

        public decimal Hijo019 { get; set; }

        public decimal Hijo2024 { get; set; }

        public decimal HijoMayor25 { get; set; }

        public decimal Ascendientes { get; set; }

        public decimal TotalPoliza { get; set; }

        public decimal DescuentoQuincenal { get; set; }
    }
}
