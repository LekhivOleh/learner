using learner.API.Interfaces.Repositories;
using learner.Models;
using Microsoft.EntityFrameworkCore;

namespace learner.API.Repositories
{
    public class UserRepository(LearnerDbContext context) : IUserRepository
    {
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            context.Users.Add(user);
            await SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            context.Users.Update(user);
            await SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user == null) return false;

            context.Users.Remove(user);
            await SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}