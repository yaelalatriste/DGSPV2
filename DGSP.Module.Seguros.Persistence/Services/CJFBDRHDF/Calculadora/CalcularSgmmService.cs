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

        public async Task<List<PrimaPotenciadaDto>> CalcularPolizaSgmmAsync(FiltroSGMMDto query)
        {
            var dataDb = await _calcularPolizaSGMM.ObtenerPrimasPotenciadasAsync((short)query.Anio, (short)query.TipoPoliza, (short)query.IQ, (short)query.SumaBasica);

            var baseData = dataDb
                .Select(x => new
                {
                    FiIdSAPotenciada = x.FiIdSAPotenciada!.Value,
                    x.Parentesco,
                    x.SumaPotenciada,
                    MontoDefinido = int.TryParse(x.SumaPotenciada, out var monto) ? monto : 0,
                    MontoPrima = (x.MontoPrima != 0 ? x.MontoPrima : 0)
                }).ToList();

            var ascendientes = new[] { "PADRE", "MADRE", "SUEGRO(A)" };

            var primaTopeAscendiente592 = baseData
                .Where(x =>
                    x.MontoDefinido == 592 &&
                    ascendientes.Contains(x.Parentesco))
                .Sum(x => x.MontoPrima);

            var primaTopeHijoMayor1000 = baseData
                .Where(x =>
                    x.MontoDefinido == 1000 &&
                    x.Parentesco == "HIJO (A) > de 25")
                .Sum(x => x.MontoPrima);

            var resultado = baseData
                .GroupBy(x => new
                {
                    x.FiIdSAPotenciada,
                    x.SumaPotenciada
                })
                .OrderBy(g => g.Key.FiIdSAPotenciada)
                .Select(g =>
                {
                    var montoMaximo = g.Max(x => x.MontoDefinido);

                    return new PrimaPotenciadaDto
                    {
                        SumaAsegurada = g.Key.SumaPotenciada,

                        Titular = g
                            .Where(x => x.Parentesco == "TITULAR")
                            .Sum(x => x.MontoPrima),

                        Conyuge = g
                            .Where(x => x.Parentesco == "CÓNYUGE")
                            .Sum(x => x.MontoPrima),

                        Hijo = g
                            .Where(x => x.Parentesco == "HIJO(A)")
                            .Sum(x => x.MontoPrima),

                        HijoMayor25 = montoMaximo > 1000
                            ? primaTopeHijoMayor1000
                            : g.Where(x => x.Parentesco == "HIJO (A) > de 25")
                                .Sum(x => x.MontoPrima),

                        Ascendientes = montoMaximo > 592
                            ? primaTopeAscendiente592
                            : g.Where(x => ascendientes.Contains(x.Parentesco))
                                .Sum(x => x.MontoPrima)
                    };
                })
                .ToList();

            return resultado;
        }
    }
}
