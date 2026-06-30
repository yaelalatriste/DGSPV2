using DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.Calculadora;
using DGSP.Module.Seguros.Application.Services.CJFBDRHDF.Calculadora;
using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Catalogos;

namespace DGSP.Module.Seguros.Persistence.Services.CJFBDRHDF.Calculadora
{
    public class CatalogosSgmmService : ICatalogosSgmmService
    {
        private readonly ICatalogosSgmmRepository _repository;

        public CatalogosSgmmService(ICatalogosSgmmRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatalogosSgmmDto> ObtenerCatalogosSgmm(ObtenerCatalogosSgmmDto query)
        {
            var tipos = await _repository.ObtenerTiposPolizaAsync();
            var vigencias = await _repository.ObtenerVigenciasAsync(query.Anio);
            var sumasBasicas = await _repository.ObtenerSumasBasicasAsync();
            var sumas = await _repository.ObtenerSumasAseguradasAsync();
            var extraPrimas = await _repository.ObtenerExtraPrimasAsync();
            var iq = await _repository.ObtenerIQAsync();
            var parentescos = await _repository.ObtenerParentescosAsync();
            var edades = await _repository.ObtenerRangosEdadAsync();

            return new CatalogosSgmmDto
            {
                TiposPoliza = tipos.Select(x => new CatalogoSimpleDto<byte>(x.FiIdTpoPol, x.FcDescTpoPol)).ToList(),
                Vigencias = vigencias.Select(x => new CatalogoSimpleDto<short>(x.FiIdRegVig, x.FcCveAsegVig)).ToList(),
                SumasBasicas = sumasBasicas.Select(x => new CatalogoSimpleDto<byte>(x.FiIdRegSA, x.FcDescSumAseg)).ToList(),
                SumasAseguradas = sumas.Select(x => new CatalogoSimpleDto<byte>(x.FiIdRegSA, x.FcDescSumAseg)).ToList(),
                ExtraPrimas = extraPrimas.Select(x => new CatalogoSimpleDto<byte>(x.FiIdRegSA, x.FcDescSumAseg)).ToList(),
                IQ = iq.Select(x => new CatalogoSimpleDto<short>(x.FiIdIQ, x.FcDescIQ ?? string.Empty)).ToList(),
                Parentescos = parentescos.Select(x => new CatalogoSimpleDto<byte>(x.FiIdParent, x.FcDescParent)).ToList(),
                RangosEdad = edades.Select(x => new RangoEdadDto { Id = x.fiIdRegEdad, LimiteInferior = x.fiLimInfEdad, LimiteSuperior = x.fiLimSupEdad }).ToList()
            };
        }
    }
}
