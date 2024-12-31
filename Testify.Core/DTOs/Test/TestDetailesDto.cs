using System.ComponentModel.DataAnnotations;

namespace Testify.Core.DTOs.Test
{
    public class TestDetailesDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string TestName { get; set; } = null!;
        [Required]
        public int CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public ICollection<CreateQuestionDto> Questions { get; set; } = new List<CreateQuestionDto>();

        public ICollection<SubmissionDto> Submission { get; set; } = new List<SubmissionDto>();
    }
}
