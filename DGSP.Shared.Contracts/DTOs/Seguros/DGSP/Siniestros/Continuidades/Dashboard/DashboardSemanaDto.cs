namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Dashboard
{
    public class DashboardSemanaDto
    {
        public int Numero { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public int IngresadasOperativo { get; set; }

        public int IngresadasMandoMedio { get; set; }
        public int IngresadasMandoSuperior { get; set; }

        public int TotalIngresadas => IngresadasOperativo + IngresadasMandoMedio+ IngresadasMandoSuperior;

        public List<DashboardEstatusDto> Estatus { get; set; } = [];
    }
}
