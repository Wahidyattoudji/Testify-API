using Microsoft.AspNetCore.Mvc;
using Testify.Core.Interfaces;

namespace TestifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var users = await _unitOfWork.UserRepo.GetAllAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepo.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        [HttpGet("GetUsersByRole/{role}")]
        public async Task<IActionResult> GetUsersByRoleAsync(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return BadRequest("Role cannot be null or empty.");
            }

            var result = await _unitOfWork.UserRepo
                .FindByFunctionAsync(
                    u => u.Role.ToLower() == role.ToLower()
                   , new[] { $"{(role.ToLower() == "teacher" ? "Tests" : "Submissions")}" }
                    );

            if (result == null || !result.Any())
            {
                return NotFound($"No users with the role '{role}' found.");
            }

            return Ok(result);
        }

    }
}
