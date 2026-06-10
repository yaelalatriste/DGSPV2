namespace DGSP.Domain.Permisos
{
    public class Permiso
    {
        public int Id { get; set; }
        public string Abreviacion { get; set; }  
        public string Nombre { get; set; }  
        public DateTime? FechaCreacion { get;set; }
        public DateTime? FechaActualizacion { get;set; }
        public DateTime? FechaEliminacion { get;set; }
    }
}
