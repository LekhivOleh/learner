using learner.Models;

namespace learner.API.Interfaces.Repositories;

public interface IEntryRepository
{
    Task<IEnumerable<Entry>> GetAllAsync();
    Task<Entry?> GetByIdAsync(Guid id);
    Task CreateAsync(Entry entry);
    void Update(Entry entry);
    Task<bool> DeleteAsync(Guid id);
    Task SaveChangesAsync();
    Task<IEnumerable<Entry>> GetBySubjectIdAsync(Guid subjectId);
}