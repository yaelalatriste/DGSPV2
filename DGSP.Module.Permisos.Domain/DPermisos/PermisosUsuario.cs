namespace DGSP.Module.Permisos.Domain.DPermisos
{
    public class PermisoUsuario
    {
        public string UsuarioId { get; set; } = string.Empty;
        public int ModuloId { get; set; }
        public int SubmoduloId { get; set; }
        public int PermisoId { get; set; }
        public int OpcionId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
