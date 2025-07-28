using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace learner.Models
{
    public class Entry
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(5000)]
        public string Content { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(20)")]
        public EntryType Type { get; set; } = EntryType.Note;

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid SubjectId { get; set; }

        public Subject? Subject { get; set; }
    }
}