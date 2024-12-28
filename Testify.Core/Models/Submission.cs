using System.Text.Json.Serialization;

namespace Testify.Core.Models;

public partial class Submission
{
    public int SubmissionId { get; set; }

    public int TestId { get; set; }

    public int StudentId { get; set; }

    public DateTime? SubmittedAt { get; set; } = DateTime.Now;

    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    [JsonIgnore]
    public virtual User Student { get; set; } = null!;

    public virtual ICollection<SubmissionAnswer> SubmissionAnswers { get; set; } = new List<SubmissionAnswer>();

    [JsonIgnore]
    public virtual Test Test { get; set; } = null!;
}
