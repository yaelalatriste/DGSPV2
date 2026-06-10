namespace DGSP.Modules.SMedicos.Contract.DTOs.Lotes
{
    public record ExistenciaPorLoteDto(
        int ConsultorioId,
        string Consultorio,
        int MedicamentoId,
        string Medicamento,
        int LoteId,
        string Lote, 
        int TipoInsumoId,
        string Insumo,
        DateTime FechaCaducidad,
        int Existencia,
        int CantidadEnvase,
        int CantidadTotal,
        string FormaFarmaceutica,
        string Concentracion
    );
}
