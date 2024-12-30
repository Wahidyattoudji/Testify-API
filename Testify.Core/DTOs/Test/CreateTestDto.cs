using System.ComponentModel.DataAnnotations;
using TestifyWebAPI.DTOs;

namespace Testify.Core.DTOs.Test
{
    public class CreateTestDto
    {
        [Required]
        public string TestName { get; set; } = null!;
        [Required]
        public int CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public ICollection<QuestionDto> Questions { get; set; } = new List<QuestionDto>();

        public ICollection<SubmissionDto> Submission { get; set; } = new List<SubmissionDto>();
    }
}
