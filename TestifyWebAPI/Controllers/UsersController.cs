using Microsoft.AspNetCore.Mvc;
using Testify.Core.DTOs.User;
using Testify.Core.Models;
using TestifyWebAPI.Enums;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAll();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }

            var usersDto = new List<UserDetailesDto>();

            foreach (var user in users)
            {
                usersDto.Add(new UserDetailesDto
                {
                    Id = user.UserId,
                    Username = user.Username,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role,
                });
            }

            return Ok(usersDto);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            var userDto = new CreateUserDto
            {
                Username = user.Username,
                Password = user.Password,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
            };
            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] CreateUserDto request)
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
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var deletedUser = await _userService.GetById(id);

            if (deletedUser == null)
            {
                return NotFound($"No user found with ID : {id}");
            }

            await _userService.DeleteUser(deletedUser.UserId);

            return Ok(deletedUser);
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var UsersList = await _userService.GetAll();

            var ExistUser = UsersList
                .FirstOrDefault(
                u => u.Username.Equals(request.Username)
                && u.Password.Equals(request.Password)
                );

            if (ExistUser == null)
            {
                return BadRequest("Wrong Username or Password");
            }

            var UserDto = new UserDetailesDto
            {
                FullName = ExistUser.FullName,
                Username = ExistUser.Username,
                Email = ExistUser.Email,
                Role = ExistUser.Role,
            };

            return Ok(UserDto);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto request)
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
    }
}
