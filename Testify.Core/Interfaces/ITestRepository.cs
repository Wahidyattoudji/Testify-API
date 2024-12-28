using Testify.Core.Models;

namespace Testify.Core.Interfaces
{
    public interface ITestRepository : IGenericRepository<Test>
    {
        Task<IEnumerable<Test>> GetFullTestAsync(int id);
    }
}
