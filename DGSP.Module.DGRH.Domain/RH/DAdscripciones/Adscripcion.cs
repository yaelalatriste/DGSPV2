namespace DGSP.Module.DGRH.Domain.RH.DAdscripciones
{
    public class Adscripcion
    {
        public string? cve_adscripcion { get; set; }
        public string? nom_adscripcion { get; set; }
        public string? dct_area { get; set; }
        public string? ubic_area { get; set; }
        public Nullable<short> circuito { get; set; }
        public Nullable<short> cve_edi { get; set; }
        public Nullable<DateTime> fech_alt_adsc { get; set; }
        public Nullable<DateTime> fech_baj_adsc { get; set; }
        public Nullable<char> ind_adsc { get; set; }
        public Nullable<int> num_area { get; set; }
        public Nullable<char> ind_localfor { get; set; }
    }
}
