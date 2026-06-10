using System.ComponentModel.DataAnnotations;

namespace DGSP.Shared.Contracts.DTOs.Usuarios
{
    public class UserDto
    {
        public string cve_usuario { get; set; }
        public string cve_puesto { get; set; }
        public string nom_cat { get; set; }
        public string nombre_NPM { get; set; }
        public string cve_area { get; set; }
        public string cve_adscripcion { get; set; }
        public string rfc_emp { get; set; }
        public string curp_emp { get; set; }
        public string nom_area { get; set; }
        public string nom_edo { get; set; }
        public string nom_cd { get; set; }
        public string nom_pue { get; set; }
    }
}
