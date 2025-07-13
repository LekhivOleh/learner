using learner.DTOs;

namespace learner.API.Interfaces.Services;

public interface ISubjectService
{
    Task<SubjectDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<SubjectDto?>> GetAllAsync();
    Task<SubjectDto> AddAsync(CreateSubjectDto subject);
    Task<SubjectDto> UpdateAsync(Guid id, UpdateSubjectDto subject);
    Task<bool> DeleteAsync(Guid id);
}