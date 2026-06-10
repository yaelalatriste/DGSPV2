namespace DGSP.Domain.Modulos
{
    public class Submodulo
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }
        public int ModuloId { get; set; }
        public string Nombre { get; set; }
        public string URL { get; set; }
        public string Icono { get; set; }
    }
}
