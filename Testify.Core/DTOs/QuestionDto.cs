using System.ComponentModel.DataAnnotations;

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

        public ICollection<OptionsDto> Options { get; set; } = new List<OptionsDto>();

        public ICollection<SubmissionAnswerDto> SubmissionAnswers { get; set; } = new List<SubmissionAnswerDto>();
    }
}
