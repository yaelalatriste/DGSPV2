using System;

namespace DGSP.Shared.Contracts.Commands.Cendis.JUsuarios
{
    public class JUsuarioCreateCommand
    {
        public int Expediente { get; set; }
        public string ClaveUsuario { get; set; }
        public string ClavePuesto { get; set; }
        public string Puesto { get; set; }
        public string NombreCat { get; set; }
        public string NombreCompleto { get; set; }
        public string ClaveArea { get; set; }
        public string ClaveAdscripcion { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string Area { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
