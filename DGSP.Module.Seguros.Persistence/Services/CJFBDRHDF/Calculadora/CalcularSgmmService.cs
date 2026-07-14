using DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.Calculadora;
using DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.SGMM;
using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.Calculadora;
using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora;

namespace DGSP.Module.Seguros.Persistence.Services.CJFBDRHDF.Calculadora
{
    public class CalcularPolizaSgmmService : ICalcularPolizaSgmmService
    {
        private readonly ICatalogosSgmmRepository _catalogosSgmmRepository;
        private readonly IServidorPublicoOpMMSRepository _servidorPublicoOpMMS;
        private readonly ICalcularPolizaSGMMRepository _calcularPolizaSGMM;

        public CalcularPolizaSgmmService(ICatalogosSgmmRepository catalogosSgmmRepository, IServidorPublicoOpMMSRepository servidorPublicoOpMMS,
                                         ICalcularPolizaSGMMRepository calcularPolizaSGMM)
        {
            _catalogosSgmmRepository = catalogosSgmmRepository;
            _servidorPublicoOpMMS = servidorPublicoOpMMS;
            _calcularPolizaSGMM = calcularPolizaSGMM;
        }

        public async Task<List<PrimaPotenciadaDto>> ObtenerPrimasPotenciadasAsync(FiltroSGMMDto query)
        {
            var dataDb = await _calcularPolizaSGMM.ObtenerPrimasPotenciadasExtraAsync((short)query.Anio, (short)query.TipoPoliza, (short)query.IQ, (short)query.SumaBasica);

            var sumaPotenciadaIds = new short[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13, 14 };

            var baseData = dataDb
                .Where(x =>
                    x.FiIdSAPotenciada.HasValue &&
                    sumaPotenciadaIds.Contains(x.FiIdSAPotenciada.Value))
                .Select(x => new
                {
                    FiIdSAPotenciada = x.FiIdSAPotenciada!.Value,
                    x.Parentesco,
                    x.SumaPotenciada,
                    MontoDefinido = int.TryParse(x.SumaPotenciada, out var monto) ? monto : 0,
                    x.MontoPrima,
                    x.FiTpoExtraPrima,
                    x.FiTpoCAdicional
                })
                .ToList();

            var ascendientes = new[] { "PADRE", "MADRE", "SUEGRO(A)" };

            decimal ObtenerPrimaTope1000(string parentesco)
            {
                return baseData
                    .Where(x =>
                        x.Parentesco == parentesco &&
                        x.MontoDefinido == 1000 &&
                        x.FiTpoExtraPrima == 0)
                    .Select(x => x.MontoPrima)
                    .DefaultIfEmpty(0)
                    .Max();
            }

            decimal ObtenerPrimaTope592(string parentesco)
            {
                return baseData
                    .Where(x =>
                        x.Parentesco == parentesco &&
                        x.MontoDefinido == 592)
                    .Select(x => x.MontoPrima)
                    .DefaultIfEmpty(0)
                    .Max();
            }

            var resultadoProcesado = baseData
                .Select(x =>
                {
                    var primaTope1000 = ObtenerPrimaTope1000(x.Parentesco);
                    var primaTope592 = ObtenerPrimaTope592(x.Parentesco);

                    var montoPrimaAplicada = x.MontoPrima;

                    if ((x.Parentesco == "TITULAR" ||
                         x.Parentesco == "CÓNYUGE" ||
                         x.Parentesco == "HIJO(A)") &&
                        x.MontoDefinido > 1000)
                    {
                        montoPrimaAplicada = x.MontoPrima + primaTope1000;
                    }
                    else if (x.Parentesco.Equals("HIJO (A) > de 25") &&
                             x.MontoDefinido >= 1000)
                    {
                        montoPrimaAplicada = primaTope1000;
                    }
                    else if (ascendientes.Contains(x.Parentesco) &&
                             x.MontoDefinido >= 592)
                    {
                        montoPrimaAplicada = primaTope592;
                    }

                    return new
                    {
                        x.FiIdSAPotenciada,
                        x.SumaPotenciada,
                        x.Parentesco,
                        x.FiTpoExtraPrima,
                        MontoPrimaAplicada = montoPrimaAplicada
                    };
                })
                .ToList();

            decimal Sumar(IEnumerable<dynamic> grupo, string parentesco, short extraPrima)
            {
                return grupo
                    .Where(x => x.Parentesco == parentesco && x.FiTpoExtraPrima == extraPrima)
                    .Sum(x => (decimal)x.MontoPrimaAplicada);
            }

            var resultado = resultadoProcesado
                .GroupBy(x => new
                {
                    x.FiIdSAPotenciada,
                    x.SumaPotenciada
                })
                .OrderBy(g => g.Key.FiIdSAPotenciada)
                .Select(g => new PrimaPotenciadaDto
                {
                    SumaAsegurada = g.Key.SumaPotenciada,

                    Titular019 = Sumar(g, "TITULAR", 1) + Sumar(g, "TITULAR", 0),
                    Titular2059 = Sumar(g, "TITULAR", 2) + Sumar(g, "TITULAR", 0),
                    Titular6069 = Sumar(g, "TITULAR", 3) + Sumar(g, "TITULAR", 0),
                    Titular7089 = Sumar(g, "TITULAR", 4) + Sumar(g, "TITULAR", 0),

                    Conyuge019 = Sumar(g, "CÓNYUGE", 1) + Sumar(g, "CÓNYUGE", 0),
                    Conyuge2059 = Sumar(g, "CÓNYUGE", 2) + Sumar(g, "CÓNYUGE", 0),
                    Conyuge6069 = Sumar(g, "CÓNYUGE", 3) + Sumar(g, "CÓNYUGE", 0),
                    Conyuge7089 = Sumar(g, "CÓNYUGE", 4) + Sumar(g, "CÓNYUGE", 0),

                    Hijo019 = Sumar(g, "HIJO(A)", 1) + Sumar(g, "HIJO(A)", 0),
                    Hijo2059 = Sumar(g, "HIJO(A)", 2) + Sumar(g, "HIJO(A)", 0),

                    HijoM25 = Convert.ToInt32(g.Key.SumaPotenciada) > 1000 ? ObtenerPrimaTope1000("HIJO (A) > de 25") : g
                        .Where(x => x.Parentesco == "HIJO (A) > de 25" && x.FiTpoExtraPrima == 0)
                        .Sum(x => x.MontoPrimaAplicada),

                    Ascendentes = Convert.ToInt32(g.Key.SumaPotenciada) > 1000 ? ObtenerPrimaTope592(ascendientes[0]) : g
                        .Where(x => ascendientes.Contains(x.Parentesco) && x.FiTpoExtraPrima == 0)
                        .Sum(x => x.MontoPrimaAplicada)
                })
                .ToList();

            return resultado;
        }
    }
}
