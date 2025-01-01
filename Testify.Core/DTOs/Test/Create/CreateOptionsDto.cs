using System.ComponentModel.DataAnnotations;

namespace Testify.Core.DTOs.Test.Create
{
    public partial class CreateOptionsDto
    {
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string OptionText { get; set; } = null!;
        [Required]
        public bool IsCorrect { get; set; }
    }
}
