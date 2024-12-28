using System.ComponentModel.DataAnnotations;

namespace TestifyWebAPI.DTOs
{
    public class EvaluationDto
    {
        [Required]
        public int SubmissionId { get; set; }

        public int TotalScore { get; set; }

        public string? Feedback { get; set; }

        public DateTime? EvaluatedAt { get; set; } = DateTime.Now;
    }
}
