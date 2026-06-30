using DGSP.Shared.Contracts.DTOs.DGRH.RH.Empleados;

namespace DGSP.Shared.Contracts.DTOs.Catalogos.SMedicos
{
    public class CTConsultorioDto
    {
        public int Id { get; set; }
        public int Clave{ get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int Poblacion { get; set; }
        public string Extension { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int ExpedienteResponsable { get; set; }

        public EmpleadoDto ServidorPublico { get; set; } = new EmpleadoDto();
    }
}
