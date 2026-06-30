namespace DGSP.Shared.Contracts.DTOs.DGRH.Seguros
{
    public class MovimientoSpDto
    {
        /// <summary>
        /// Identificador del movimiento propio de la tabla. Número entero consecutivo que se reiniciará cada año
        /// </summary>
        public int FiIdRegMovSp { get; set; }

        /// <summary>
        /// Año en que se registra el movimiento
        /// </summary>
        public short FiAnioMovSp { get; set; }

        /// <summary>
        /// Número de expediente del servidor público
        /// </summary>
        public int FiExpSp { get; set; }

        /// <summary>
        /// Identificador del movimiento que se está registrando en la póliza
        /// </summary>
        public byte FiIdMov { get; set; }

        /// <summary>
        /// Fecha de antigüedad del Servidor Público como asegurado en Gastos Médicos Mayores
        /// </summary>
        public DateTime FdFchAntigGmm { get; set; }

        /// <summary>
        /// Fecha a partir de la cual aplica el movimiento registrado
        /// </summary>
        public DateTime FdFchAplicMovSp { get; set; }

        /// <summary>
        /// Indicador de la suma basica asociada a movimiento del servidor públcio
        /// </summary>
        public byte? FiIdSumBasica { get; set; }

        /// <summary>
        /// Identificador de la suma a la que tiene potenciado el seguro el Servidor Público
        /// </summary>
        public byte? FiIdSumPotenciada { get; set; }

        /// <summary>
        /// Observaciones al movimiento capturadas por el actor correspondiente
        /// </summary>
        public string? FcObservMovSp { get; set; }

        /// <summary>
        /// Indica que la poliza no tiene limite en cuanto a cobertura
        /// </summary>
        public bool? FlSinLimite { get; set; }

        /// <summary>
        /// Identificador del registro de la correspondencia asociada al movimiento.
        /// </summary>
        public int? FiIdRegOfic { get; set; }

        /// <summary>
        /// Año del registro de la correspondencia asociada al movimiento
        /// </summary>
        public short? FiAnioRegOfic { get; set; }

        /// <summary>
        /// Clave de adscripción a la que pertenece el servidor público
        /// </summary>
        public string? FcCveAdscCjf { get; set; }

        /// <summary>
        /// Clave del puesto del servidor público
        /// </summary>
        public string? FcCvePueCjf { get; set; }

        /// <summary>
        /// Identificador de la prima de seguro definida en la poliza del servidor público
        /// </summary>
        public int? FiIdPrim { get; set; }

        /// <summary>
        /// Tipo de Validación del movimento. 0=sin validar ,1 = Validado sin papeleria de envio, 2 = Validado CON papeleria de envio  
        /// </summary>
        public byte? FiValidaProveedor { get; set; }

        /// <summary>
        /// Expediente del actor que está dando de alta el movimiento en la tabla
        /// </summary>
        public int FiUsrAltaMovSp { get; set; }

        /// <summary>
        /// Fecha en que se da de alta el movimiento en la tabla
        /// </summary>
        public DateTime FdFchAltaMovSp { get; set; }

        /// <summary>
        /// Expediente del actor que hace modificaciones al movimiento
        /// </summary>
        public int? FiUsrModMovSp { get; set; }

        /// <summary>
        /// Fecha en que se realizan modificaciones al movimiento. Este atributo será de utilidad en el módulo de administración de movimientos en póliza
        /// </summary>
        public DateTime? FdFchModMovSp { get; set; }

        /// <summary>
        /// Indica si el movimiento de la póliza registrado esta activo
        /// </summary>
        public bool FlMovActivo { get; set; }

        /// <summary>
        /// Número de oficio asociado a los movimientos del dependiente económico
        /// </summary>
        public string? FcOficioProv { get; set; }
    }
}
