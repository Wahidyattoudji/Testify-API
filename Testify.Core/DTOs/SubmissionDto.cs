using System.ComponentModel.DataAnnotations;
using Testify.Core.Models;

namespace Testify.Core.DTOs;

public class SubmissionDto
{
    [Required]
    public int TestId { get; set; }
    [Required]
    public int StudentId { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    public virtual ICollection<SubmissionAnswer> SubmissionAnswers { get; set; } = new List<SubmissionAnswer>();
}
