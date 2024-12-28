namespace Testify.Core.Models;

public partial class Evaluation
{
    public int EvaluationId { get; set; }

    public int SubmissionId { get; set; }

    public int TotalScore { get; set; }

    public string? Feedback { get; set; }

    public DateTime? EvaluatedAt { get; set; } = DateTime.Now;

    public virtual Submission Submission { get; set; } = null!;
}
