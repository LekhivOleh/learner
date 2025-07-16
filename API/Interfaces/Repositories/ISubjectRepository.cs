using learner.Models;

namespace learner.API.Interfaces.Repositories;

public interface ISubjectRepository
{
    Task<Subject?> GetSubjectByIdAsync(Guid id);
    Task<IEnumerable<Subject?>> GetAllSubjectsAsync();
    Task AddSubjectAsync(Subject subject);
    Task UpdateSubjectAsync(Guid id, Subject subject);
    Task<bool> DeleteSubjectAsync(Guid id);
    Task SaveChangesAsync();
}