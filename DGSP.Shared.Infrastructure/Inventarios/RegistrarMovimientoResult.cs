namespace DGSP.Shared.Infrastructure.Inventarios;

public enum RegistrarMovimientoError
{
    Validation,
    NotFound
}

public sealed record RegistrarMovimientoResult(
    bool Ok,
    string? Error = null,
    RegistrarMovimientoError? ErrorType = null)
{
    public static RegistrarMovimientoResult Success() => new(true);

    public static RegistrarMovimientoResult BadRequest(string msg) =>
        new(false, msg, RegistrarMovimientoError.Validation);

    public static RegistrarMovimientoResult NotFound(string msg) =>
        new(false, msg, RegistrarMovimientoError.NotFound);
}
