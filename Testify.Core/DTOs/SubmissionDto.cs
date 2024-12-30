using System.ComponentModel.DataAnnotations;
using TestifyWebAPI.DTOs;

namespace Testify.Core.DTOs;

public class SubmissionDto
{
    [Required]
    public int TestId { get; set; }
    [Required]
    public int StudentId { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public virtual ICollection<EvaluationDto> Evaluations { get; set; } = new List<EvaluationDto>();

    public virtual ICollection<SubmissionAnswerDto> SubmissionAnswers { get; set; } = new List<SubmissionAnswerDto>();
}
