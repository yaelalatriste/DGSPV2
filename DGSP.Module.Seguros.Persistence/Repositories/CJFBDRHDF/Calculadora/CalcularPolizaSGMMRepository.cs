using DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.Calculadora;
using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Calculadora;
using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora;
using DGSP.Shared.Contracts.Enums.Seguros.Calculadora;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence.Repositories.CJFBDRHDF.Calculadora
{
    public class CalcularPolizaSGMMRepository : ICalcularPolizaSGMMRepository
    {
        private readonly SegurosSGMMContext _context;

        public CalcularPolizaSGMMRepository(SegurosSGMMContext context)
        {
            _context = context;
        }

        public async Task<List<PrimaOpMMSExtraBaseDto>> ObtenerPrimasPotenciadasExtraAsync(short anio,short tipoPoliza,short iq,short sumaBasica)
        {
            var parentescoIds = Enum
                .GetValues<ParentescoSgmm>()
                .Select(x => (short)x)
                .ToArray();

            var resultado = await _context.PrimasOpMMS
                .AsNoTracking()
                .Where(p =>
                    p.FiAnio == anio &&
                    p.FiIdRegVig == tipoPoliza &&
                    p.FiIdIQ == iq &&
                    p.FiIdSAOrigen == sumaBasica &&
                    p.FiIdParent.HasValue &&
                    parentescoIds.Contains(p.FiIdParent.Value))
                .Join(
                    _context.CTParentesco.AsNoTracking(),
                    p => p.FiIdParent,
                    c => c.FiIdParent,
                    (p, c) => new { p, c }
                )
                .Join(
                    _context.CTSumaAseg.AsNoTracking(),
                    pc => pc.p.FiIdSAPotenciada,
                    sp => sp.FiIdRegSA,
                    (pc, sp) => new PrimaOpMMSExtraBaseDto
                    {
                        FiIdSAPotenciada = pc.p.FiIdSAPotenciada,
                        Parentesco = pc.c.FcDescParent,
                        SumaPotenciada = sp.FcDescSumAseg,
                        MontoPrima = pc.p.FnMtoPrim,
                        FiTpoExtraPrima = pc.p.FiTpoExtraPrima ?? 0,
                        FiTpoCAdicional = pc.p.FiTpoCAdicional ?? 0
                    }
                )
                .ToListAsync();

            return resultado;
        }
    }
}
