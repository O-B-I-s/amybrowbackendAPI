using amybrowbackendAPI.Data;
using amybrowbackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace amybrowbackendAPI.Repositories
{
    public class DescriptionRepository : IDescriptionRepository
    {
        private readonly AppDbContext _ctx;
        public DescriptionRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<ServiceDescription>> GetAllAsync() =>
            await _ctx.ServiceDescriptions.ToListAsync();

        public async Task<ServiceDescription> GetByIdAsync(int id) =>
            await _ctx.ServiceDescriptions.FindAsync(id);

        public async Task AddAsync(ServiceDescription desc)
        {
            await _ctx.ServiceDescriptions.AddAsync(desc);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceDescription desc)
        {
            _ctx.ServiceDescriptions.Update(desc);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var d = await _ctx.ServiceDescriptions.FindAsync(id);
            if (d == null) return;
            _ctx.ServiceDescriptions.Remove(d);
            await _ctx.SaveChangesAsync();
        }
    }
}
