using Testify.Core.Models;

namespace TestifyWebAPI.Services.Contracts
{
    public interface IEvaluationService
    {
        Task<IEnumerable<Evaluation>> GetAll();
        Task<Evaluation> GetById(int id);

        Task<Evaluation> AddEvaluation(Evaluation Evaluation);
        Task<Evaluation> UpdateEvaluation(Evaluation Evaluation);
        Task DeleteEvaluation(int id);
    }
}
