using learner.API.Interfaces.Repositories;
using learner.Models;
using Microsoft.EntityFrameworkCore;

namespace learner.API.Repositories;

public class EntryRepository(LearnerDbContext context) : IEntryRepository
{
    public async Task<IEnumerable<Entry>> GetAllAsync()
    {
        return await context.Entries
        .Include(e => e.Subject)
        .ToListAsync();
    }

    public async Task<Entry?> GetByIdAsync(Guid id)
    {
        return await context.Entries
        .Include(e => e.Subject)
        .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task CreateAsync(Entry entry)
    {
        context.Entries.Add(entry);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(Entry entry)
    {
        context.Entries.Update(entry);
        await SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entry = await GetByIdAsync(id);
        if (entry == null)
        {
            return false;
        }

        context.Entries.Remove(entry);
        await SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Entry>> GetBySubjectIdAsync(Guid subjectId)
    {
        return await context.Entries
        .Where(e => e.SubjectId == subjectId)
        .ToListAsync();
    }
}