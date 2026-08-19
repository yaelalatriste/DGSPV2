namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Continuidades.Dashboard
{
    public class DashboardTotalesDto
    {
        public int Ingresadas { get; set; }
        public int Curso{ get; set; }
        public int Movimientos { get; set; }
        public int Solicitud{ get; set; }
        public int PendientePago { get; set; }
        public int Pagadas { get; set; }
        public int Concluidas { get; set; }
        public int EnCorreccion { get; set; }
        public int Canceladas { get; set; }
        public int Improcedentes { get; set; }

    }
}
