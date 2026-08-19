namespace DGSP.Shared.Contracts.Helpers;

public static class PeriodoSemanaHelper
{
    public static IReadOnlyList<RangoSemana> ObtenerSemanas(int anio,int mes)
    {
        if (mes is < 1 or > 12)
            throw new ArgumentOutOfRangeException(nameof(mes));

        var inicioMes = new DateTime(anio, mes, 1);

        var finMes = inicioMes.AddMonths(1).AddDays(-1);

        var resultado = new List<RangoSemana>();

        var inicioSemana = inicioMes;
        var numeroSemana = 1;

        while (inicioSemana <= finMes)
        {
            var diasHastaDomingo = ((int)DayOfWeek.Sunday - (int)inicioSemana.DayOfWeek + 7) % 7;
            var finSemana = inicioSemana.AddDays(diasHastaDomingo);

            if (finSemana > finMes)
                finSemana = finMes;

            resultado.Add(new RangoSemana
            {
                Numero = numeroSemana++,
                FechaInicio = inicioSemana,
                FechaFin = finSemana
            });

            inicioSemana = finSemana.AddDays(1);
        }

        return resultado;
    }
}

public sealed class RangoSemana
{
    public int Numero { get; init; }

    public DateTime FechaInicio { get; init; }

    public DateTime FechaFin { get; init; }
}
