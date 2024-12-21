using System;
using System.Collections.Generic;

namespace Testify.Core.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int TestId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string QuestionType { get; set; } = null!;

    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();

    public virtual ICollection<SubmissionAnswer> SubmissionAnswers { get; set; } = new List<SubmissionAnswer>();

    public virtual Test Test { get; set; } = null!;
}
