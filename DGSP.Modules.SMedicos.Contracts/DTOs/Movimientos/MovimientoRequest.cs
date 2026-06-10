namespace DGSP.Modules.SMedicos.Contract.DTOs.Movimientos
{
    public record MovimientoRequest(
        int ConsultorioId,
        int ConsultaId,
        int LoteId,
        int TipoInsumoId,
        int Cantidad,
        int CantidadTotal,
        char Movimiento,                 // 'E' o 'S'
        string FormaFarmaceutica,
        string? Observaciones,
        DateTime FechaCreacion
    );
}
