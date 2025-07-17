using learner.API.Interfaces.Repositories;
using learner.API.Interfaces.Services;
using learner.DTOs;
using learner.Models;

namespace learner.API.Services;

public class SubjectService(ISubjectRepository subjectRepository) : ISubjectService
{
    public async Task<SubjectDto?> GetByIdAsync(Guid id)
    {
        var subject = await subjectRepository.GetByIdAsync(id);
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
        var subjects = await subjectRepository.GetAllAsync();
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

        await subjectRepository.CreateAsync(subject);
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
        var existingSubject = await subjectRepository.GetByIdAsync(id);
        if (existingSubject == null)
        {
            throw new KeyNotFoundException("Subject not found");
        }

        if (subject.Title != null)
        {
            existingSubject.Title = subject.Title;
        }

        subjectRepository.Update(existingSubject);
        await subjectRepository.SaveChangesAsync();

        return new SubjectDto
        {
            Id = existingSubject.Id,
            Title = existingSubject.Title,
            CreatedAt = existingSubject.CreatedAt,
            UserId = existingSubject.UserId,
            EntryCount = 0
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var subject = await subjectRepository.DeleteAsync(id);
        if (subject)
        {
            await subjectRepository.SaveChangesAsync();
        }
        return subject;
    }
}