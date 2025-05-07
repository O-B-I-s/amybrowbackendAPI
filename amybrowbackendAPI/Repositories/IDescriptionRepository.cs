using amybrowbackendAPI.Models;

namespace amybrowbackendAPI.Repositories
{
    public interface IDescriptionRepository
    {
        Task<IEnumerable<ServiceDescription>> GetAllAsync();
        Task<ServiceDescription> GetByIdAsync(int id);
        Task AddAsync(ServiceDescription desc);
        Task UpdateAsync(ServiceDescription desc);
        Task DeleteAsync(int id);
    }
}
