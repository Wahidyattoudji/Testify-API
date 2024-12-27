using Testify.Core.Models;

namespace TestifyWebAPI.Services.Contracts
{
    public interface ISubmissionAnswerService
    {
        Task<IEnumerable<SubmissionAnswer>> GetAll();
        Task<SubmissionAnswer> GetById(int id);

        Task<SubmissionAnswer> AddSubmissionAnswer(SubmissionAnswer SubmissionAnswer);
        Task<SubmissionAnswer> UpdateSubmissionAnswer(SubmissionAnswer SubmissionAnswer);
        Task DeleteSubmissionAnswer(int id);
    }
}
