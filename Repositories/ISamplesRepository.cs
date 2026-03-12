using DnaLabApi.Entities;

namespace DnaLabApi.Repositories
{
    public interface ISampleRepository
    {
        Task<IEnumerable<Sample>> GetAllAsync();
        Task<Sample> GetByIdAsync(Guid id);
        Task CreateAsync(Sample sample);
        Task UpdateAsync(Sample sample);
        Task DeleteAsync(Guid id);
    }
}