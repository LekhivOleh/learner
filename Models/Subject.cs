using System.ComponentModel.DataAnnotations;
using Models;

namespace Models
{
    public class Subject
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        public ICollection<Entry> Entries { get; set; } = new List<Entry>();
    }
}