using learner.DTOs;

namespace learner.API.Interfaces.Services;

public interface IEntryService
{
    Task<IEnumerable<EntryDto?>> GetAllEntriesAsync();
    Task<EntryDto?> GetEntryByIdAsync(Guid id);
    Task<EntryDto> CreateEntryAsync(CreateEntryDto entry);
    Task<EntryDto> UpdateEntryAsync(Guid id, UpdateEntryDto entry);
    Task<bool> DeleteEntryAsync(Guid id);
    Task<IEnumerable<EntryDto?>> GetEntriesBySubjectIdAsync(Guid subjectId);
}