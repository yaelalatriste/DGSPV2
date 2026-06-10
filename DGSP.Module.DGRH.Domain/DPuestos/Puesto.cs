namespace DGSP.Module.DGRH.Domain
{

    public partial class Puesto
    {
        public string cve_puesto { get; set; } = null!;

        public string nivel { get; set; } = null!;

        public string ind_jerarquia { get; set; } = null!;

        public string nom_puesto { get; set; } = null!;

        public string? nom_puesto_fem { get; set; }

        public string dct_pue { get; set; } = null!;

        public string ind_tpo_puesto { get; set; } = null!;

        public DateTime fech_alt_puesto { get; set; }

        public DateTime? fech_baj_puesto { get; set; }

        public string ind_puesto { get; set; } = null!;

        public int? num_puesto { get; set; }
    }
}