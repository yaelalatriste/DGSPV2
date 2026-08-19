namespace DGSP.Shared.Contracts.Helpers;

public static class TipoPersonalHelper
{
    public const string Operativo = "OP";
    public const string MandoMedio = "MM";
    public const string MandoSuperior= "MS";
    public const string NoDeterminado = "ND";

    public static string ObtenerTipo(string? nivel)
    {
        if (string.IsNullOrWhiteSpace(nivel))
            return NoDeterminado;

        var nivelNormalizado = nivel.Trim();

        var parteNumerica = new string(
            nivelNormalizado
                .TakeWhile(char.IsDigit)
                .ToArray());

        if (!int.TryParse(parteNumerica, out var numeroNivel))
            return NoDeterminado;

        if (numeroNivel>24)
        {
            return Operativo;
        }
        else if(numeroNivel<= 24 && numeroNivel >= 11)
        {
            return MandoMedio;
        }
        else
        {
            return MandoSuperior;
        }
    }
}