using Testify.Core.Models;

namespace Testify.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepo { get; }
        ITestRepository TestRepo { get; }
        IGenericRepository<Submission> SubmissionRepo { get; }
        IGenericRepository<SubmissionAnswer> SubmissionAnswerRepo { get; }
        IGenericRepository<Question> QuestionRepo { get; }
        IGenericRepository<QuestionOption> QuestionOptionRepo { get; }
        IGenericRepository<Evaluation> EvaluationRepo { get; }

        Task CommitAsync();
        Task Rollback();
    }
}
