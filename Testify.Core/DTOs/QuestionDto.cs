using System.ComponentModel.DataAnnotations;
using Testify.Core.Models;

namespace TestifyWebAPI.DTOs
{
    public class QuestionDto
    {
        [Required]
        public int TestId { get; set; }
        [Required]
        public string QuestionText { get; set; } = null!;
        [Required]
        public string QuestionType { get; set; } = null!;

        public ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();

        public ICollection<SubmissionAnswer> SubmissionAnswers { get; set; } = new List<SubmissionAnswer>();
    }
}
