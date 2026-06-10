namespace DGSP.Modules.SMedicos.Contract.DTOs.Medicamentos
{
    public record MedicamentoDto(
        int Id,
        string? Clave,
        string Nombre,
        bool Activo
    );
}
