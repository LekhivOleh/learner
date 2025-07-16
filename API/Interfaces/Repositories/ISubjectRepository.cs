using learner.Models;

namespace learner.API.Interfaces.Repositories;

public interface ISubjectRepository
{
    Task<Subject?> GetByIdAsync(Guid id);
    Task<IEnumerable<Subject?>> GetAllAsync();
    Task CreateAsync(Subject subject);
    void Update(Subject subject);
    Task<bool> DeleteAsync(Guid id);
    Task SaveChangesAsync();
}