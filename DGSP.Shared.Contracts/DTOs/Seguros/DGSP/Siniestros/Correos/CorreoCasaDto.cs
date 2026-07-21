namespace DGSP.Shared.Contracts.DTOs.Seguros.DGSP.Siniestros.Correos
{
    public class CorreoCasaDto
    {
        public int Id { get; set; }
        public string Abreviacion { get; set; } = string.Empty;
        public string Dependencia { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public int Expediente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Zona_TEV { get; set; } = string.Empty;
        public string Zona_RH { get; set; } = string.Empty;
        public decimal Edificio { get; set; }
        public decimal PrimaNeta { get; set; }
        public bool Enviado { get; set; }

    }
}
