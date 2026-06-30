using DGSP.Module.Seguros.Domain.CJFBDRHDF.Models.SGMM;

namespace DGSP.Module.Seguros.Application.Interfaces.CJFBDRHDF.SGMM
{
    public interface IServidorPublicoOpMMSRepository
    {
        Task<ServidorPublicoOpMMS> GetServidorPublicoOpMMS(int expediente);
    }
}
