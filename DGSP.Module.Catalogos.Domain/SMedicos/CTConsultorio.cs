namespace DGSP.Module.Catalogos.Domain.SMedicos
{
    public class CTConsultorio
    {
        public int Id { get; set; }
        public int Clave { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int Poblacion { get; set; }
        public string Extension { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int ExpedienteResponsable { get; set; }
    }
}
