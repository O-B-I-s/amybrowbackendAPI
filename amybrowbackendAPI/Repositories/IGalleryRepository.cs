using amybrowbackendAPI.Models;

namespace amybrowbackendAPI.Repositories
{
    public interface IGalleryRepository
    {
        Task<IEnumerable<GalleryItem>> GetAllAsync();
        Task<GalleryItem> GetByIdAsync(int id);
        Task AddAsync(GalleryItem item);
        Task UpdateAsync(GalleryItem item);
        Task DeleteAsync(int id);
    }
}
