using System.ComponentModel.DataAnnotations;

namespace learner.DTOs
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public int EntryCount { get; set; }
    }

    public class SubjectDetailDto : SubjectDto
    {
        public List<EntryDto> Entries { get; set; } = new List<EntryDto>();
    }

    public class CreateSubjectDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
    }

    public class UpdateSubjectDto
    {
        [MaxLength(100)]
        public string? Title { get; set; }
    }
}