using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;

namespace DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.Calculadora
{

    public interface ICatalogosSgmmRepository
    {
        Task<List<CTTpoPoliza>> ObtenerTiposPolizaAsync();
        Task<List<CTVigenciaOpMMS>> ObtenerVigenciasAsync(short anio);
        Task<List<CTSumaAseg>> ObtenerSumasBasicasAsync();
        Task<CTSumaAseg> ObtenerSumaBasicaByIdAsync(int id);
        Task<List<CTSumaAseg>> ObtenerSumasAseguradasAsync();
        Task<List<CTSumaAseg>> ObtenerExtraPrimasAsync();
        Task<List<CTIQ>> ObtenerIQAsync();
        Task<List<CTParentesco>> ObtenerParentescosAsync();
        Task<List<CTEdad>> ObtenerRangosEdadAsync();
    }
}