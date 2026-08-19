namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Dashboard
{
    public class DashboardEstatusDto
    {
        public int EstatusId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Abreviacion { get; set; } = string.Empty;

        public string? FondoHexadecimal { get; set; }

        public int Total { get; set; }

        public int Operativo { get; set; }

        public int MandoMedio { get; set; }
        public int MandoSuperior{ get; set; }

        public int NoDeterminado { get; set; }
    }
}
