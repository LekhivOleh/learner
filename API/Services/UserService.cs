
using learner.API.Interfaces.Repositories;
using learner.API.Interfaces.Services;
using learner.DTOs;
using learner.Models;

namespace learner.API.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email
            });
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email is null or empty", nameof(email));
            }

            var user = await userRepository.GetByEmailAsync(email);
            return user == null ? null : new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID is empty", nameof(id));
            }

            var user = await userRepository.GetByIdAsync(id);
            return user == null ? null : new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = user.Username,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            await userRepository.CreateAsync(newUser);
            await userRepository.SaveChangesAsync();
            return new UserDto
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Email = newUser.Email
            };
        }

        public async Task<UserDto> UpdateUserAsync(Guid id, UpdateUserDto user)
        {
            var existingUser = await userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new ArgumentException("No user with such ID", nameof(id));
            }
            if (user.Username != null)
            {
                existingUser.Username = user.Username;
            }
            if (user.Email != null)
            {
                existingUser.Email = user.Email;
            }
            if (user.Password != null)
            {
                existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            userRepository.Update(existingUser);
            await userRepository.SaveChangesAsync();
            return new UserDto()
            {
                Id = existingUser.Id,
                Username = existingUser.Username,
                Email = existingUser.Email
            };
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            try
            {
                await userRepository.DeleteAsync(id);
                await userRepository.SaveChangesAsync();
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
    }
}
