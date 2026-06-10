namespace DGSP.Modules.SMedicos.Contract.DTOs.Lotes
{
    public sealed record LoteCreateOrGetRequestDto(
       int ConsultorioId,
       int MedicamentoId,
       int TipoInsumoId,
       string Lote,
       DateTime FechaCaducidad,
       string FormaFarmaceutica,
       string TipoEnvase,
       int Cantidad,
       int CantidadEnvase,
       int CantidadTotal,
       string Concentracion,
       string UnidadContenido,
       string Observaciones
   );
}
