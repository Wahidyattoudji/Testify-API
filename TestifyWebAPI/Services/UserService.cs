using Testify.Core.Interfaces;
using Testify.Core.Models;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IEnumerable<User>> GetAll()
    {
        var users = await _unitOfWork.UserRepo.GetAllAsync();
        return users.OrderBy(u => u.FullName);
    }

    public async Task<IEnumerable<User>> GetAllStudents()
    {
        var students = await GetUsersByRole("student");
        return students.OrderBy(u => u.FullName);
    }

    public async Task<IEnumerable<User>> GetAllTeachers()
    {
        var teacher = await GetUsersByRole("teacher");
        return teacher.OrderBy(u => u.FullName);
    }

    public async Task<User> GetById(int id)
    {
        return await _unitOfWork.UserRepo.FindByIdAsync(id);
    }


    public async Task<User> AddUser(User user)
    {
        try
        {
            await _unitOfWork.UserRepo.AddAsync(user);
            await _unitOfWork.CommitAsync();
            return user;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while adding a user", ex);
        }
    }

    public async Task<User> UpdateUser(User user)
    {
        try
        {
            await _unitOfWork.UserRepo.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
            return user;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while updating a user", ex);
        }
    }

    public async Task DeleteUser(int id)
    {
        try
        {
            await _unitOfWork.UserRepo.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception("Error while Deleting a user", ex);
        }
    }

    #region Helpers
    private async Task<IEnumerable<User>> GetUsersByRole(string role)
    {
        return await _unitOfWork.UserRepo.FindByFunctionAsync
                    (
                    u => u.Role.ToLower() == role.ToLower()
                   , new[] { $"{(role.ToLower() == "teacher" ? "Tests" : "Submissions")}" }
                    );
    }
    #endregion

}
