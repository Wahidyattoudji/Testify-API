using System.ComponentModel.DataAnnotations;
using Testify.Core.Models;

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

        public ICollection<SubmissionAnswer> SubmissionAnswers { get; set; } = new List<SubmissionAnswer>();
    }
}
