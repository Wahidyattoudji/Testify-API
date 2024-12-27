using Testify.Core.Models;

namespace TestifyWebAPI.Services.Contracts
{
    public interface ISubmissionService
    {
        Task<IEnumerable<Submission>> GetAll();
        Task<Submission> GetById(int id);

        Task<Submission> AddSubmission(Submission submission);
        Task<Submission> UpdateSubmission(Submission submission);
        Task DeleteSubmission(int id);
    }
}
