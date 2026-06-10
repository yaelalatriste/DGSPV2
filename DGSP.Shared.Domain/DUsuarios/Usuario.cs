namespace Usuarios.Domain
{
    public class Usuario
    {
        public string Id { get; set; }
        public int Expediente { get; set; }
        public string ? Username { get; set; }
        public string ? Password { get; set; }
        public string ? NombreEmp { get; set; }
        public string ? PaternoEmp { get; set; }
        public string ? MaternoEmp { get; set; }
        public string ? RFC { get; set; }
        public string ? Curp { get; set; }
        public string ? Puesto { get; set; }
        public string ? ClavePuesto { get; set; }
        public string ? Email { get; set; }
    }
}
