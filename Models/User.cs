using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}