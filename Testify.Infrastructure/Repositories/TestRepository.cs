using Microsoft.EntityFrameworkCore;
using Testify.Core.Interfaces;
using Testify.Core.Models;

namespace Testify.Infrastructure.Repositories
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {

        public TestRepository(TestifyDbContext context) : base(context) { }

        public async Task<IEnumerable<Test>> GetFullTestAsync(int id)
        {
            return await _dbTable
                .Include(t => t.Questions)
                .ThenInclude(q => q.QuestionOptions)
                .ToListAsync();
        }
    }
}
