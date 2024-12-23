using Testify.Core.Models;

namespace Testify.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> SearchAsync(string SearchItem);
    }
}
