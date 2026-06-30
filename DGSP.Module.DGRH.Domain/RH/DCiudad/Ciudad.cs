namespace DGSP.Module.DGRH.Domain.RH.DCiudad
{
    public class Ciudad
    {
        public Nullable<char> cve_edo { get; set; }
        public Nullable<char> cve_cd { get; set; }
        public short circuito { get; set; }
        public Nullable<char> ind_ze { get; set; }
        public Nullable<char> ind_ag { get; set; }
        public string? nom_cd { get; set; }
        public string? dct_cd{ get; set; }
        public Nullable<DateTime> fch_ini_cd { get; set; }
        public Nullable<DateTime> fch_fin_cd { get; set; }
        public Nullable<char> ind_cd { get; set; }
    }
}
