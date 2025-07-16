using learner.API.Interfaces.Repositories;
using learner.Models;
using Microsoft.EntityFrameworkCore;

namespace learner.API.Repositories;

public class SubjectRepository(LearnerDbContext context) : ISubjectRepository
{
    public async Task<Subject?> GetSubjectByIdAsync(Guid id)
    {
        return await context.Subjects
            .Include(s => s.User)
            .Include(s => s.Entries)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Subject?>> GetAllSubjectsAsync()
    {
        return await context.Subjects
            .Include(s => s.User)
            .Include(s => s.Entries)
            .ToListAsync();
    }

    public async Task AddSubjectAsync(Subject subject)
    {
        await context.Subjects.AddAsync(subject);
    }

    public async Task UpdateSubjectAsync(Guid id, Subject subject)
    {
        context.Subjects.Update(subject);
        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteSubjectAsync(Guid id)
    {
        var subject = await GetSubjectByIdAsync(id);
        if (subject == null)
        {
            return false;
        }

        context.Subjects.Remove(subject);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
