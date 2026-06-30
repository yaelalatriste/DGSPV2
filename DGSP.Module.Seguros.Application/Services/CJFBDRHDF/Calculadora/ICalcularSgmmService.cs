using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora;

namespace DGSP.Module.Seguros.Application.Services.CJFBDRHDF.Calculadora
{
    public interface ICalcularPolizaSgmmService
    {
        Task<List<PrimaPotenciadaDto>> CalcularPolizaSgmmAsync(FiltroSGMMDto query);
    }
}
