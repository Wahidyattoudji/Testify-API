using System;
using System.Collections.Generic;

namespace Testify.Core.Models;

public partial class Test
{
    public int TestId { get; set; }

    public string TestName { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
