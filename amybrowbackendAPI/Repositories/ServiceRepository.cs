using amybrowbackendAPI.Data;
using amybrowbackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace amybrowbackendAPI.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;
        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
            => await _context.Services
                             .Include(s => s.Descriptions)
                             .ToListAsync();

        public async Task<Service> GetByIdAsync(int id)
            => await _context.Services
                             .Include(s => s.Descriptions)
                             .FirstOrDefaultAsync(s => s.Id == id);

        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var svc = await _context.Services.FindAsync(id);
            if (svc == null) return;
            _context.Services.Remove(svc);
            await _context.SaveChangesAsync();
        }
    }
}
