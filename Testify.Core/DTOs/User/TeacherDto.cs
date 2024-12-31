using System.ComponentModel.DataAnnotations;
using Testify.Core.DTOs.Test;

namespace Testify.Core.DTOs.User
{
    public class TeacherDto
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
        public string Role { get; set; } = "Teacher";

        public virtual ICollection<TestDetailesDto> Tests { get; set; } = new List<TestDetailesDto>();
    }
}
