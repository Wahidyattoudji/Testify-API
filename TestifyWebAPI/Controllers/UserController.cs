using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Testify.Core.Models;
using TestifyWebAPI.DTOs;
using TestifyWebAPI.Enums;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAll();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDto request)
        {
            var UsersList = await _userService.GetAll();

            if (UsersList.Any(u => u.Username == request.Username))
            {
                return Conflict("Username is Already Exsist");
            }
            if (UsersList.Any(u => u.Email == request.Email))
            {
                return Conflict("Email is Already Exist");
            }

            if (!Enum.TryParse<Roles>(request.Role, out _))
            {
                return BadRequest("Role should be 'Teacher' or 'Student'");
            }

            var user = new User()
            {
                FullName = request.FullName,
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                Role = request.Role
            };


            var newUser = await _userService.AddUser(user);

            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserDto request)
        {
            var existingUser = await _userService.GetById(id);

            if (existingUser == null)
            {
                return NotFound($"No user found with ID : {id}");
            }

            var UsersList = await _userService.GetAll();

            if (UsersList.Any(u => u.Username == request.Username))
            {
                return Conflict("Username is Already Exsist");
            }
            if (UsersList.Any(u => u.Email == request.Email))
            {
                return Conflict("Email is Already Exist");
            }
            if (!Enum.TryParse<Roles>(request.Role, out _))
            {
                return BadRequest("Role should be 'Teacher' or 'Student'");
            }


            existingUser.FullName = request.FullName;
            existingUser.Username = request.Username;
            existingUser.Password = request.Password;
            existingUser.Email = request.Email;
            existingUser.Role = request.Role;

            var updatedUser = await _userService.UpdateUser(existingUser);

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id, [FromBody] UserDto request)
        {
            var deletedUser = await _userService.GetById(id);

            if (deletedUser == null)
            {
                return NotFound($"No user found with ID : {id}");
            }

            await _userService.DeleteUser(deletedUser.UserId);

            return Ok(deletedUser);
        }
    }
}
