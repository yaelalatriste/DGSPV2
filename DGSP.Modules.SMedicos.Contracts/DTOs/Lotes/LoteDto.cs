namespace DGSP.Modules.SMedicos.Contract.DTOs.Lotes
{
    public sealed record LoteDto(
        int LoteId,
        int ConsultorioId,
        int MedicamentoId,
        string Lote,
        DateTime FechaCaducidad,
        string FormaFarmaceutica,
        string TipoEnvase,
        int Cantidad,
        int CantidadEnvase,
        int CantidadTotal,
        string Concentracion,
        string UnidadContenido,
        string Notas
    );
}
