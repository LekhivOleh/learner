using learner.API.Interfaces.Repositories;
using learner.API.Interfaces.Services;
using learner.DTOs;
using learner.Models;

namespace learner.API.Services;

public class EntryService(IEntryRepository entryRepository) : IEntryService
{
    public async Task<IEnumerable<EntryDto?>> GetAllEntriesAsync()
    {
        var entries = await entryRepository.GetAllAsync();
        return entries.Select(e => new EntryDto
        {
            Id = e.Id,
            Title = e.Title,
            Content = e.Content,
            Type = e.Type,
            IsCompleted = e.IsCompleted,
            CreatedAt = e.CreatedAt,
            SubjectId = e.SubjectId,
            SubjectTitle = e.Subject?.Title ?? string.Empty
        });
    }

    public async Task<IEnumerable<EntryDto?>> GetEntriesBySubjectIdAsync(Guid subjectId)
    {
        var entries = await entryRepository.GetBySubjectIdAsync(subjectId);
        return entries.Select(e => new EntryDto
        {
            Id = e.Id,
            Title = e.Title,
            Content = e.Content,
            Type = e.Type,
            IsCompleted = e.IsCompleted,
            CreatedAt = e.CreatedAt,
            SubjectId = e.SubjectId,
            SubjectTitle = e.Subject?.Title ?? string.Empty
        });
    }

    public async Task<EntryDto?> GetEntryByIdAsync(Guid id)
    {
        var entry = await entryRepository.GetByIdAsync(id);
        if (entry == null)
        {
            return null;
        }

        return new EntryDto
        {
            Id = entry.Id,
            Title = entry.Title,
            Content = entry.Content,
            Type = entry.Type,
            IsCompleted = entry.IsCompleted,
            CreatedAt = entry.CreatedAt,
            SubjectId = entry.SubjectId,
            SubjectTitle = entry.Subject.Title
        };
    }

    public async Task<EntryDto> CreateEntryAsync(CreateEntryDto entry)
    {
        var newEntry = new Entry
        {
            Id = Guid.NewGuid(),
            Title = entry.Title,
            Content = entry.Content,
            Type = entry.Type,
            IsCompleted = entry.IsCompleted,
            CreatedAt = DateTime.UtcNow,
            SubjectId = entry.SubjectId
        };

        await entryRepository.CreateAsync(newEntry);
        await entryRepository.SaveChangesAsync();

        var createdEntry = await entryRepository.GetByIdAsync(newEntry.Id) ?? throw new InvalidOperationException("Created entry could not be found.");

        return new EntryDto
        {
            Id = createdEntry.Id,
            Title = createdEntry.Title,
            Content = createdEntry.Content,
            Type = createdEntry.Type,
            IsCompleted = createdEntry.IsCompleted,
            CreatedAt = createdEntry.CreatedAt,
            SubjectId = createdEntry.SubjectId,
            SubjectTitle = createdEntry.Subject.Title
        };
    }

    public async Task<EntryDto> UpdateEntryAsync(Guid id, UpdateEntryDto entry)
    {
        var existingEntry = await entryRepository.GetByIdAsync(id);
        if (existingEntry == null)
        {
            throw new KeyNotFoundException("Entry not found");
        }

        if (entry.Title != null)
        {
            existingEntry.Title = entry.Title;
        }
        if (entry.Content != null)
        {
            existingEntry.Content = entry.Content;
        }
        if (entry.IsCompleted.HasValue)
        {
            existingEntry.IsCompleted = entry.IsCompleted.Value;
        }

        entryRepository.Update(existingEntry);
        await entryRepository.SaveChangesAsync();

        return new EntryDto
        {
            Id = existingEntry.Id,
            Title = existingEntry.Title,
            Content = existingEntry.Content,
            Type = existingEntry.Type,
            IsCompleted = existingEntry.IsCompleted,
            CreatedAt = existingEntry.CreatedAt,
            SubjectId = existingEntry.SubjectId,
            SubjectTitle = existingEntry.Subject.Title
        };
    }

    public async Task<bool> DeleteEntryAsync(Guid id)
    {
        var entry = await entryRepository.DeleteAsync(id);
        if (entry)
        {
            await entryRepository.SaveChangesAsync();
        }
        return entry;
    }
}