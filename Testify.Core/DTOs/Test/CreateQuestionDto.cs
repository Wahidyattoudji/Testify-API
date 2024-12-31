using System.ComponentModel.DataAnnotations;

namespace Testify.Core.DTOs.Test
{
    public class CreateQuestionDto
    {
        [Required]
        public int TestId { get; set; }
        [Required]
        public string QuestionText { get; set; } = null!;
        [Required]
        public string QuestionType { get; set; } = null!;

        public ICollection<CreateOptionsDto> Options { get; set; } = new List<CreateOptionsDto>();
    }
}
