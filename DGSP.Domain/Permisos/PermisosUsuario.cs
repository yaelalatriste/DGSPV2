namespace DGSP.Domain.Permisos
{
    public class PermisoUsuario
    {
        public string UsuarioId { get; set; }
        public int ModuloId { get; set; }
        public int SubmoduloId { get; set; }
        public int PermisoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
