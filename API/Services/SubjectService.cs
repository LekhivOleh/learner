using learner.API.Interfaces.Repositories;
using learner.API.Interfaces.Services;
using learner.DTOs;
using learner.Models;
using Microsoft.AspNetCore.Mvc;

namespace learner.API.Services;

public class SubjectService(ISubjectRepository subjectRepository) : ISubjectService
{
    public async Task<SubjectDto?> GetByIdAsync(Guid id)
    {
        var subject = await subjectRepository.GetSubjectByIdAsync(id);
        return subject == null ? null : new SubjectDto
        {
            Id = subject.Id,
            Title = subject.Title,
            CreatedAt = subject.CreatedAt,
            UserId = subject.UserId
        };
    }

    public async Task<IEnumerable<SubjectDto?>> GetAllAsync()
    {
        var subjects = await subjectRepository.GetAllSubjectsAsync();
        return subjects.Select(subject =>
        subject == null ? null : new SubjectDto
        {
            Id = subject.Id,
            Title = subject.Title,
            CreatedAt = subject.CreatedAt,
            UserId = subject.UserId
        });
    }

    public async Task<SubjectDto> AddAsync(CreateSubjectDto createSubjectDto)
    {
        var subject = new Subject
        {
            Id = Guid.NewGuid(),
            Title = createSubjectDto.Title,
            CreatedAt = DateTime.UtcNow,
            UserId = createSubjectDto.UserId
        };

        await subjectRepository.AddSubjectAsync(subject);
        await subjectRepository.SaveChangesAsync();

        return new SubjectDto
        {
            Id = subject.Id,
            Title = subject.Title,
            CreatedAt = subject.CreatedAt,
            UserId = subject.UserId,
            EntryCount = 0
        };
    }

    public async Task<SubjectDto> UpdateAsync(Guid id, UpdateSubjectDto subject)
    {
        var existingSubject = await subjectRepository.GetSubjectByIdAsync(id);
        if (existingSubject == null)
        {
            throw new KeyNotFoundException("Subject not found");
        }

        if (subject.Title != null)
        {
            existingSubject.Title = subject.Title;
        }

        await subjectRepository.UpdateSubjectAsync(existingSubject);
        await subjectRepository.SaveChangesAsync();

        return new SubjectDto
        {
            Id = existingSubject.Id,
            Title = existingSubject.Title,
            CreatedAt = existingSubject.CreatedAt,
            UserId = existingSubject.UserId,
            EntryCount = existingSubject.Entries.Count
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            await subjectRepository.DeleteSubjectAsync(id);
            await subjectRepository.SaveChangesAsync();
            return true;
        }
        catch (KeyNotFoundException)
        {
            return false;
        }
    }
}