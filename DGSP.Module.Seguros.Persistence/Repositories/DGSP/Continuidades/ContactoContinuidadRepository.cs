using DGSP.Module.Seguros.Application.Interfaces.DGSP.Continuidades;
using DGSP.Module.Seguros.Domain.DGSP.Continuidades;
using Microsoft.EntityFrameworkCore;

namespace DGSP.Module.Seguros.Persistence.Repositories.DGSP.Continuidades
{
    public class ContactoContinuidadRepository : IContactoContinuidadRepository
    {
        private readonly SegurosDbContext _context;

        public ContactoContinuidadRepository(SegurosDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContactoContinuidad>> GetAllContactoContinuidades()
        {
            var contactos = await _context.ContactosContinuidades.AsNoTracking().ToListAsync();

            return contactos;
        }

        public async Task<List<ContactoContinuidad>> GetContactosByContinuidad(int continuidadId)
        {
            var contactos = await _context.ContactosContinuidades.Where(o => o.ContinuidadId == continuidadId).ToListAsync();

            return contactos;
        }
        
        public async Task<ContactoContinuidad> GetContactoById(int id)
        {
            var contacto = await _context.ContactosContinuidades.Where(o => o.Id == id).FirstOrDefaultAsync() ?? new ContactoContinuidad();

            return contacto;
        }

        public async Task RegistrarContactoContinuidadAsync(ContactoContinuidad continuidad)
        {
            await _context.AddAsync(continuidad);
        }
        
        public async Task ActualizarContactoContinuidadAsync(ContactoContinuidad continuidad)
        {
            _context.Update(continuidad);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
