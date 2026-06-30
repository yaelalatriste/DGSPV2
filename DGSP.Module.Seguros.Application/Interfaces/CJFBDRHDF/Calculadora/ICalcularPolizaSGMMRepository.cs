using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.Calculadora;

namespace DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.Calculadora
{
    public interface ICalcularPolizaSGMMRepository
    {
        Task<List<PrimaOpMMSBase>> ObtenerPrimasPotenciadasAsync(short anio, short tipoPoliza, short iq, short sumaBasica);
    }
}
