namespace DGSP.Shared.Contracts.DTOs.SMedicos.TiposInsumos
{
    public record TipoInsumoDto
    (
        int Id,
        string Nombre,
        bool Estatus,
        DateTime FechaCreacion
    );    
}
