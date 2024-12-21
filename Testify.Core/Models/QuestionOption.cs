using System;
using System.Collections.Generic;

namespace Testify.Core.Models;

public partial class QuestionOption
{
    public int OptionId { get; set; }

    public int QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual ICollection<SubmissionAnswer> SubmissionAnswers { get; set; } = new List<SubmissionAnswer>();
}
