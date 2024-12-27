using Testify.Core.Models;

namespace TestifyWebAPI.Services.Contracts
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAll();
        Task<Question> GetById(int id);

        Task<Question> AddQuestion(Question question);
        Task<Question> UpdateQuestion(Question question);
        Task DeleteQuestion(int id);
    }
}
