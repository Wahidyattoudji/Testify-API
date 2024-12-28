using System.ComponentModel.DataAnnotations;

namespace TestifyWebAPI.DTOs
{
    public partial class SubmissionAnswerDto
    {
        [Required]
        public int SubmissionId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int? SelectedOptionId { get; set; }
    }
}
