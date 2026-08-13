namespace DGSP.Module.Seguros.Domain.DGSP.Movimientos.Calculadora
{
    public class CalendarioNomina
    {
        public int Id { get; set; }
        public int Ejercicio { get; set; }
        public int NumeroQuincena { get; set; }
        public int QuincenaRestante { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaEntregaNomina { get; set; }
        public string Quincena { get; set; } = string.Empty;
    }
}
