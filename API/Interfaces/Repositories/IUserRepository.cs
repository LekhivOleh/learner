using learner.Models;

namespace learner.API.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task CreateAsync(User user);
    void Update(User user);
    Task<bool> DeleteAsync(Guid id);
    Task SaveChangesAsync();
}
