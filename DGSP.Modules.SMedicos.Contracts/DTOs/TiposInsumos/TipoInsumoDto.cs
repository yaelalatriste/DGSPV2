namespace DGSP.Modules.SMedicos.Contract.DTOs.TiposInsumos
{
    public record TipoInsumoDto
    (
        int Id,
        string Nombre,
        bool Estatus,
        DateTime FechaCreacion
    );    
}
