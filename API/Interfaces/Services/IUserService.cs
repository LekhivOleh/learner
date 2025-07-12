using learner.DTOs;
using learner.Models;

namespace learner.API.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<UserDto> CreateUserAsync(CreateUserDto user);
        Task<UserDto> UpdateUserAsync(Guid id, UpdateUserDto user);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
