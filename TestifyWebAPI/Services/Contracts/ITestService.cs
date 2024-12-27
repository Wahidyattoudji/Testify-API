using Testify.Core.Models;

namespace TestifyWebAPI.Services.Contracts
{
    public interface ITestService
    {
        Task<IEnumerable<Test>> GetAll();
        Task<Test> GetById(int id);

        Task<Test> AddTest(Test test);
        Task<Test> UpdateTest(Test test);
        Task DeleteTest(int id);
    }
}
