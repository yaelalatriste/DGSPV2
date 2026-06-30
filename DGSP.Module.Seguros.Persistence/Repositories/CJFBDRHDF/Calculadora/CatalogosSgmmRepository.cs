using DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.Calculadora;
using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Catalogos;
using DGSP.Module.Seguros.Persistence;
using DGSP.Shared.Contracts.Enums.Seguros.Calculadora;
using Microsoft.EntityFrameworkCore;

namespace SISSGMM.Infrastructure.Repositories;

public class CatalogosSgmmRepository : ICatalogosSgmmRepository
{
    private readonly SegurosSGMMContext _context;
    public CatalogosSgmmRepository(SegurosSGMMContext context) => _context = context;

    public Task<List<CTTpoPoliza>> ObtenerTiposPolizaAsync()
    {
        var ids = Enum.GetValues<PolizasSgmm>().Select(x => (byte)x).ToArray();
        var tiposPoliza = _context.CTTpoPoliza.Where(x => ids.Contains(x.FiIdTpoPol))
                        .OrderBy(x => x.FiIdTpoPol).ToListAsync();

        return tiposPoliza;
    }

    public Task<List<CTVigenciaOpMMS>> ObtenerVigenciasAsync(short anio)
    {
        var query = _context.CTVigenciaOpMMS.AsQueryable().Where(x => x.FcVigencia.EndsWith(anio.ToString().Substring(2)));

        return query.OrderByDescending(x => x.FiIdRegVig).ToListAsync();
    }

    public async Task<CTSumaAseg> ObtenerSumaBasicaByIdAsync(int id)
    {
        var sumasBasicas = await _context.CTSumaAseg.Where(x => x.FiIdRegSA == id).OrderBy(x => x.FiIdRegSA).FirstOrDefaultAsync();

        return sumasBasicas ?? new CTSumaAseg();
    }
   
    public async Task<List<CTSumaAseg>> ObtenerSumasBasicasAsync()
    {
        var ids = Enum.GetValues<SumaBasica>().Select(x => (byte)x).ToArray();
        var sumasBasicas = await _context.CTSumaAseg.Where(x => ids.Contains(x.FiIdRegSA)).OrderBy(x => x.FiIdRegSA).ToListAsync();

        return sumasBasicas;
    }

    public async Task<List<CTSumaAseg>> ObtenerSumasAseguradasAsync()
    {
        var sumas = await _context.CTSumaAseg.Where(x => x.FlStatusSumAseg == true && x.FiIdRegSA >0 && x.FiIdRegSA<11).OrderBy(x => x.FiIdRegSA).ToListAsync();

        return sumas;
    }
    
    public async Task<List<CTSumaAseg>> ObtenerExtraPrimasAsync()
    {
        var sumas = await _context.CTSumaAseg.Where(x => x.FlStatusSumAseg == true && x.FiIdRegSA>12).OrderBy(x => x.FiIdRegSA).ToListAsync();

        return sumas;
    }

    public Task<List<CTIQ>> ObtenerIQAsync() => _context.CTIQ.Where(x => x.FlStatus).OrderBy(x => x.FiIdIQ).ToListAsync();

    public Task<List<CTParentesco>> ObtenerParentescosAsync() => _context.CTParentesco.Where(x => x.FlStatusParent == true && x.FlEstatusSGMM == true).OrderBy(x => x.FiIdParent).ToListAsync();

    public async Task<List<CTEdad>> ObtenerRangosEdadAsync()
    {
        var ids = Enum.GetValues<RangoEdad>().Select(x => (byte)x).ToArray();
        var rangosEdad = await _context.CTEdad.Where(x => ids.Contains(x.fiIdRegEdad)).OrderBy(x => x.fiIdRegEdad).ToListAsync();

        return rangosEdad;
    }

}
