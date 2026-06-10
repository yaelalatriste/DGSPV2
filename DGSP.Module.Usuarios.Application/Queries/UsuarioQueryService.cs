using DGSP.Shared.Contracts.DTOs.Usuarios;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using Usuarios.Persistence.Database;
using Usuarios.Service.Queries.Mapping;

namespace Usuarios.Service.Queries.Queries
{
    public interface IUsuarioQueryService
    {
        Task<List<UsuarioDto>> GetAllUsersAsync();
        Task<UsuarioDto> GetUserByIdAsync(string id);
        //Task<UserDto> GetUserByExpediente(int expediente);
    }

    public class UsuarioQueryService : IUsuarioQueryService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioQueryService(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioDto>> GetAllUsersAsync()
        {
            var collection = await _context.Usuarios.OrderBy(x => x.NombreEmp).ToListAsync();

            return collection.MapTo<List<UsuarioDto>>();
        }

        public async Task<UsuarioDto> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _context.Usuarios.SingleOrDefaultAsync(x => x.Id.Equals(id));

                return user.MapTo<UsuarioDto>();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }
        
        //public async Task<UserDto> GetUserByExpediente(int expediente)
        //{
        //    try
        //    {
        //        var client = new ServicioSeguridadClient();

        //        ConsultaUsuarioResponse validacionC = await client.ConsultaUsuarioAsync(expediente);
        //        var nodo = validacionC.ConsultaUsuarioResult.Nodes[1].ToString();

        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(nodo);

        //        XmlNodeList list_dg = doc.SelectNodes("//Table1");

        //        string jsonDatosGen = JsonConvert.SerializeXmlNode(list_dg[0], Newtonsoft.Json.Formatting.None, true);
                
        //        var user = JsonConvert.DeserializeObject<UserDto>(jsonDatosGen);

        //        return user.MapTo<UserDto>();
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.Message;
        //        return null;
        //    }
        //}
    }
}
