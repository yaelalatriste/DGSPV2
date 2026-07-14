using DGSP.Shared.Contracts.DTOs.Seguros.CJFBDRHDF.Calculadora;

namespace DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.Calculadora
{
    public interface ICalcularPolizaSGMMRepository
    {
        Task<List<PrimaOpMMSExtraBaseDto>> ObtenerPrimasPotenciadasExtraAsync(short anio, short tipoPoliza, short iq, short sumaBasica);
    }
}
