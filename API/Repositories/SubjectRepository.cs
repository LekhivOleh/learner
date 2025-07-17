using learner.API.Interfaces.Repositories;
using learner.Models;
using Microsoft.EntityFrameworkCore;

namespace learner.API.Repositories;

public class SubjectRepository(LearnerDbContext context) : ISubjectRepository
{
    public async Task<Subject?> GetByIdAsync(Guid id)
    {
        return await context.Subjects
            .Include(s => s.User)
            .Include(s => s.Entries)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Subject?>> GetAllAsync()
    {
        return await context.Subjects
            .AsNoTracking()
            .Include(s => s.User)
            .Include(s => s.Entries)
            .ToListAsync();
    }

    public async Task CreateAsync(Subject subject)
    {
        await context.Subjects.AddAsync(subject);
    }

    public void Update(Subject subject)
    {
        context.Subjects.Update(subject);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var subject = await GetByIdAsync(id);
        if (subject == null)
        {
            return false;
        }

        context.Subjects.Remove(subject);
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
