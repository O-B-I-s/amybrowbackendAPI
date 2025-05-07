using amybrowbackendAPI.Data;
using amybrowbackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace amybrowbackendAPI.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly AppDbContext _context;
        public GalleryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GalleryItem>> GetAllAsync()
            => await _context.GalleryItems.ToListAsync();

        public async Task<GalleryItem> GetByIdAsync(int id)
            => await _context.GalleryItems.FindAsync(id);

        public async Task AddAsync(GalleryItem item)
        {
            await _context.GalleryItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GalleryItem item)
        {
            _context.GalleryItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var gi = await _context.GalleryItems.FindAsync(id);
            if (gi == null) return;
            _context.GalleryItems.Remove(gi);
            await _context.SaveChangesAsync();
        }
    }
}
