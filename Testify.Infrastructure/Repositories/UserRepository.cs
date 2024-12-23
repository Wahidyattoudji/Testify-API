using Microsoft.EntityFrameworkCore;
using Testify.Core.Interfaces;
using Testify.Core.Models;

namespace Testify.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(TestifyDbContext context) : base(context) { }

        public async Task<IEnumerable<User>> SearchAsync(string SearchItem)
        {
            return await _db.Users.Where(u => u.Username == SearchItem
                                   || u.UserId.ToString() == SearchItem
                                   || u.Role.Equals(SearchItem))
                                     .ToListAsync();
        }
    }
}
