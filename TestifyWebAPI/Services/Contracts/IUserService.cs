using Testify.Core.Models;

namespace TestifyWebAPI.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);

        Task<IEnumerable<User>> GetAllStudents();
        Task<IEnumerable<User>> GetAllTeachers();

        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
