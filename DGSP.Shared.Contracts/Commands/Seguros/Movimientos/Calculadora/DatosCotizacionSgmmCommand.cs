using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora;

namespace DGSP.Shared.Contracts.Commands.Seguros.Movimientos.Calculadora
{
    public class DatosCotizacionSgmmCommand
    {
        public string Correo { get; set; } = string.Empty;
        public FiltroSGMMDto Filtros { get; set; } = new();
        public string EdadTitular { get; set; } = string.Empty;
        public int CantidadTitular { get; set; }
        public string EdadConyuge { get; set; } = string.Empty;
        public int CantidadConyuge { get; set; }
        public int CantidadHijo019 { get; set; }
        public int CantidadHijo2024 { get; set; }
        public int CantidadHijoMayor25 { get; set; }
        public bool AplicaHijoMayor25 { get; set; }
        public int CantidadAscendientes { get; set; }
        public bool AplicaAscendientes { get; set; }
    }
}
