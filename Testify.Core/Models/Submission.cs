using System;
using System.Collections.Generic;

namespace Testify.Core.Models;

public partial class Submission
{
    public int SubmissionId { get; set; }

    public int TestId { get; set; }

    public int StudentId { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    public virtual User Student { get; set; } = null!;

    public virtual ICollection<SubmissionAnswer> SubmissionAnswers { get; set; } = new List<SubmissionAnswer>();

    public virtual Test Test { get; set; } = null!;
}
