using System.ComponentModel.DataAnnotations.Schema;

namespace DGSP.Module.DGRH.Domain.RH.DEstado
{

    public partial class Estado
    {
        [Column("cve_edo")]
        public string CveEdo { get; set; } = null!;
        [Column("nom_edo")]
        public string NomEdo { get; set; } = null!;
        [Column("dct_edo")]
        public string DctEdo { get; set; } = null!;
        [Column("fch_ini_edo")]
        public DateTime FchIniEdo { get; set; }
        [Column("fch_fin_edo")]
        public DateTime FchFinEdo { get; set; }
        [Column("ind_edo")]
        public string IndEdo { get; set; } = null!;
        [Column("ent_fed")]
        public string? EntFed { get; set; }
    }
}