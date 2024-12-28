namespace Testify.Core.Models;

public partial class SubmissionAnswer
{
    public int AnswerId { get; set; }

    public int SubmissionId { get; set; }

    public int QuestionId { get; set; }

    public int? SelectedOptionId { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual QuestionOption? SelectedOption { get; set; }

    public virtual Submission Submission { get; set; } = null!;
}
