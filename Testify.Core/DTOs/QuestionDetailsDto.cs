using System.ComponentModel.DataAnnotations;
using Testify.Core.DTOs.Test.Create;

namespace TestifyWebAPI.DTOs
{
    public class QuestionDetailsDto
    {
        [Required]
        public int TestId { get; set; }
        [Required]
        public string QuestionText { get; set; } = null!;
        [Required]
        public string QuestionType { get; set; } = null!;

        public ICollection<CreateOptionsDto> Options { get; set; } = new List<CreateOptionsDto>();

        public ICollection<SubmissionAnswerDto> SubmissionAnswers { get; set; } = new List<SubmissionAnswerDto>();
    }
}
