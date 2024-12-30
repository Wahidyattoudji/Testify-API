using System.ComponentModel.DataAnnotations;

namespace Testify.Core.DTOs.User
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = null!;
    }
}
