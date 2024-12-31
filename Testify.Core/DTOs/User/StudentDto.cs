using System.ComponentModel.DataAnnotations;

namespace Testify.Core.DTOs.User
{
    public class StudentDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Role { get; set; } = "Student";

        public virtual ICollection<SubmissionDto> Submissions { get; set; } = new List<SubmissionDto>();

    }
}
