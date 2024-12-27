using Testify.Core.Models;

namespace TestifyWebAPI.Services.Contracts
{
    public interface IQuestionOptionService
    {
        Task<IEnumerable<QuestionOption>> GetAll();
        Task<QuestionOption> GetById(int id);

        Task<QuestionOption> AddQuestionOption(QuestionOption QuestionOption);
        Task<QuestionOption> UpdateQuestionOption(QuestionOption QuestionOption);
        Task DeleteQuestionOption(int id);
    }
}
