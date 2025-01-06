﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Testify.Core.DTOs.Test.Show;
using Testify.Core.DTOs.User;
using Testify.Core.Models;
using Testify.Infrastructure;
using TestifyWebAPI.Enums;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TestifyDbContext testifyDb;

        public UsersController(IUserService userService, TestifyDbContext testifyDb)
        {
            _userService = userService;
            this.testifyDb = testifyDb;
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
                Id = ExistUser.UserId,
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

        [HttpGet("Students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await testifyDb.Users
                .Where(u => u.Role.Equals("Student"))
                .Include(s => s.Submissions)
                .ThenInclude(s => s.Evaluations)
                .ToListAsync();



            if (students == null || !students.Any())
            {
                return NotFound("No Students found.");
            }

            var studentsDto = new List<StudentDto>();

            foreach (var user in students)
            {
                studentsDto.Add(new StudentDto
                {
                    Id = user.UserId,
                    Username = user.Username,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role,
                    Submissions = user.Submissions.Select(s => new Testify.Core.DTOs.SubmissionDto
                    {
                        StudentId = user.UserId,
                        StudentName = user.FullName,
                        TestId = s.TestId,
                        testName = testifyDb.Tests.Find(s.TestId).TestName,
                        SubmittedAt = s.SubmittedAt,
                        Evaluations = s.Evaluations.Select(e => new DTOs.EvaluationDto
                        {
                            TotalScore = e.TotalScore,
                            Feedback = e.Feedback,
                            EvaluatedAt = e.EvaluatedAt,
                            SubmissionId = e.SubmissionId
                        }).ToList(),
                    }).ToList(),
                });
            }

            return Ok(studentsDto);
        }

        [HttpGet("Teachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _userService.GetAllTeachers();
            if (teachers == null || !teachers.Any())
            {
                return NotFound("No Teachers found.");
            }

            var teachersDto = new List<TeacherDto>();

            foreach (var user in teachers)
            {
                teachersDto.Add(new TeacherDto
                {
                    Id = user.UserId,
                    Username = user.Username,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role,
                    Tests = user.Tests.Select(t => new TestDetailesDto
                    {
                        TestId = t.TestId,
                        TestName = t.TestName,
                        CreatedBy = t.CreatedBy,
                        CreatedAt = (DateTime)t.CreatedAt,
                    }).ToList()
                });
            }

            return Ok(teachersDto);
        }
    }
}
