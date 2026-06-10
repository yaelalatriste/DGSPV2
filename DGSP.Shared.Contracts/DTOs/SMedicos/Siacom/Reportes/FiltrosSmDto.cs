namespace DGSP.Shared.Contracts.DTOs.SMedicos.Siacom.Reportes
{
    public class FiltrosSmDto
    {
        public Nullable<int> Anios { get; set; } 
        public List<int> Meses { get; set; } = new List<int>();
    }
}
