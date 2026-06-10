namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Correos
{
    public class CorreoOrdenesPagoDto
    {
        public int Id { get; set; }
        public int Expediente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string NombreArchivo { get; set; } = string.Empty;
        public string Puesto { get; set; } = string.Empty;
        public string Adscripcion { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Ind_Empleado { get; set; } = string.Empty;
        public string? Fecha_Baja { get; set; } = string.Empty;
        public bool Enviado { get; set; }
    }
}
