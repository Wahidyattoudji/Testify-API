using System.ComponentModel.DataAnnotations;
using Testify.Core.Models;

namespace TestifyWebAPI.DTOs
{
    public class TestDto
    {
        [Required]
        public string TestName { get; set; } = null!;
        [Required]
        public int CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Question> Questions { get; set; } = new List<Question>();

        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
