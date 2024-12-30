using System.ComponentModel.DataAnnotations;

namespace TestifyWebAPI.DTOs
{
    public partial class OptionsDto
    {
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string OptionText { get; set; } = null!;
        [Required]
        public bool IsCorrect { get; set; }

        public ICollection<SubmissionAnswerDto> SubmissionAnswer { get; set; } = new List<SubmissionAnswerDto>();
    }
}
