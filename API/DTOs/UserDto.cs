using System.ComponentModel.DataAnnotations;

namespace learner.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class CreateUserDto
    {
        [Required]
        [MaxLength(32)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public class UpdateUserDto
    {
        [MaxLength(32)]
        public string? Username { get; set; }

        [MinLength(6)]
        public string? Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}