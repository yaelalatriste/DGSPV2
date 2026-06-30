namespace DGSP.Shared.Contracts.DTOs.DGRH.Seguros
{
    public class PrimaOPMMSDto
    {
        /// <summary>
        /// Identificador de la prima de seguro definida en la poliza del servidor público
        /// </summary>
        public int FiIdPrim { get; set; }

        /// <summary>
        /// Ejercicio fiscal del proceso de cobranza para la identificacion de priomas de mandos medios y operativos
        /// </summary>
        public short FiAnio { get; set; }

        /// <summary>
        /// Identificador de parentesco entre el servidor público y el beneficiario
        /// </summary>
        public byte? FiIdParent { get; set; }

        /// <summary>
        /// Identificador de registro de origen de cobertura
        /// </summary>
        public byte? FiIdSaorigen { get; set; }

        /// <summary>
        /// Identificador de registro asociado al incremeto de la poliza del servidor público
        /// </summary>
        public byte? FiIdSapotenciada { get; set; }

        /// <summary>
        /// Monto toal del detalle de cobertura de la póliza
        /// </summary>
        public decimal FnMtoPrim { get; set; }

        /// <summary>
        /// Fecha inicial del periodo de vigencia de la prima
        /// </summary>
        public DateTime? FdFchIniVigPrim { get; set; }

        /// <summary>
        /// Fecha final del periodo de vigencia de la prima
        /// </summary>
        public DateTime? FdFchFinVigPrim { get; set; }

        /// <summary>
        /// Fecha de alta del registro de la prima correspondiente a mandos medios u operativos
        /// </summary>
        public DateTime FdFchAltaPrim { get; set; }

        /// <summary>
        /// Número de expediente del usuario del sistema que generó el registro
        /// </summary>
        public int FiUsrAltaPrim { get; set; }

        /// <summary>
        /// Fecha en la que se realizó la modificación del registro
        /// </summary>
        public DateTime? FdFchModPrim { get; set; }

        /// <summary>
        /// Número de expediente del usuario del sistema que modificó el registro
        /// </summary>
        public int? FiUsrModPrim { get; set; }

        /// <summary>
        /// Indica si el registro esta activo para usarse en el sistema
        /// </summary>
        public bool FlStatusPrim { get; set; }

        /// <summary>
        /// Indica si el registro es de tipo pirma básica
        /// </summary>
        public bool FlIndBasicaPrim { get; set; }

        /// <summary>
        /// Identificador único correspondiente al reistro de intervenciones quirúrgicas
        /// </summary>
        public short? FiIdIq { get; set; }

        /// <summary>
        /// Identificador de tipo de extraprima contratada por el servidor público
        /// </summary>
        public int? FiTpoExtraPrima { get; set; }

        /// <summary>
        /// Identificador de cobertura adicional
        /// </summary>
        public int? FiTpoCadicional { get; set; }

        /// <summary>
        /// Identificador de registro vigente
        /// </summary>
        public short? FiIdRegVig { get; set; }

        /// <summary>
        /// Clave de deduccion de detalle de cobertura de poliza
        /// </summary>
        public int? FiCveDed { get; set; }
    }
}
