using Microsoft.EntityFrameworkCore;
using Testify.Core.Interfaces;
using Testify.Core.Models;
using Testify.Infrastructure.Repositories;
namespace Testify.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly TestifyDbContext _context;

    public IUserRepository UserRepo { get; }
    public IGenericRepository<Test> TestRepo { get; }
    public IGenericRepository<Submission> SubmissionRepo { get; }
    public IGenericRepository<SubmissionAnswer> SubmissionAnswerRepo { get; }
    public IGenericRepository<Question> QuestionRepo { get; }
    public IGenericRepository<QuestionOption> QuestionOptionRepo { get; }
    public IGenericRepository<Evaluation> EvaluationRepo { get; }


    public UnitOfWork(TestifyDbContext context)
    {
        _context = context;
        UserRepo = new UserRepository(_context);
        TestRepo = new GenericRepository<Test>(_context);
        SubmissionRepo = new GenericRepository<Submission>(_context);
        SubmissionAnswerRepo = new GenericRepository<SubmissionAnswer>(_context);
        QuestionRepo = new GenericRepository<Question>(_context);
        QuestionOptionRepo = new GenericRepository<QuestionOption>(_context);
        EvaluationRepo = new GenericRepository<Evaluation>(_context);
    }


    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Rollback()
    {
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}