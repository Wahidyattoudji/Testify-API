using Microsoft.EntityFrameworkCore;
using Testify.Core.Interfaces;
using Testify.Infrastructure.Repositories;
namespace Testify.Infrastructure.UnitOfWork;

public class UnitOfWork<T> : IUnitOfWork<T> where T : class, new()
{
    private readonly TestifyDbContext _context;

    private IGenericRepository<T> _entity;

    public UnitOfWork(TestifyDbContext context)
    {
        _context = context;
    }


    public IGenericRepository<T> EntityRepository
    {
        get
        {
            return _entity ??= new GenericRepository<T>(_context);
        }
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