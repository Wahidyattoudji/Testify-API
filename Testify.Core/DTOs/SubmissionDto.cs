using System.ComponentModel.DataAnnotations;
using TestifyWebAPI.DTOs;

namespace Testify.Core.DTOs;

public class SubmissionDto
{

    [Required]
    public int submittionId { get; set; }
    [Required]
    public int TestId { get; set; }
    [Required]
    public int StudentId { get; set; }

    public string testName { get; set; }

    public string StudentName { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public virtual ICollection<EvaluationDto> Evaluations { get; set; } = new List<EvaluationDto>();

    public virtual ICollection<SubmissionAnswerDto> SubmissionAnswers { get; set; } = new List<SubmissionAnswerDto>();
}
