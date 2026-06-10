namespace DGSP.Shared.Contracts.DTOs.Estatus.Continuidades
{
    public class EstatusContinuidadDto
    {
        public int Id { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Icono { get; set; } = string.Empty;
        public string Fondo { get; set; } = string.Empty;
        public string FondoHexadecimal { get; set; } = string.Empty;
        public int Orden { get;set; }
        public DateTime FechaCreacion { get;set; }
    }
}
