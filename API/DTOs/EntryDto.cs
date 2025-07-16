using System.ComponentModel.DataAnnotations;
using learner.Models;

namespace learner.DTOs
{
    public class EntryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public EntryType Type { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectTitle { get; set; } = string.Empty;
    }

    public class CreateEntryDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(5000)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public EntryType Type { get; set; } = EntryType.Note;

        public bool IsCompleted { get; set; } = false;

        [Required]
        public Guid SubjectId { get; set; }
    }

    public class UpdateEntryDto
    {
        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(5000)]
        public string? Content { get; set; }

        public bool? IsCompleted { get; set; }
    }
}