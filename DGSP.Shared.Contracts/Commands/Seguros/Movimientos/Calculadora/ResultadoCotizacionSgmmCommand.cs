namespace DGSP.Shared.Contracts.Commands.Seguros.Movimientos.Calculadora
{
    public class ResultadoCotizacionSgmmCommand
    {
        public string Correo { get; set; } = string.Empty;

        public DatosCotizacionSgmmCommand Datos { get; set; } = new();

        public List<DetalleCotizacionSgmmCommand> Detalle { get; set; } = new();
    }
}
