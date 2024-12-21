using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Testify.Core.Interfaces;
namespace Testify.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
{
    private readonly DbContext _db;

    private DbSet<T>? _dbTable = null;

    public GenericRepository(DbContext context)
    {
        _db = context;
        _dbTable = context.Set<T>();

    }

    public async Task AddAsync(T entity)
    {
        await _dbTable.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbTable.Update(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await FindByIdAsync(id);
        if (entity != null)
        {
            _dbTable.Remove(entity);
        }
    }


    public async Task<IEnumerable<T>> GetAllAsync() => await _dbTable.ToListAsync() ?? new List<T>();

    public async Task<IEnumerable<T>> FindByFunctionAsync(Expression<Func<T, bool>> predicate) =>
        await _dbTable.Where(predicate).ToListAsync() ?? new List<T>();

    public async Task<T> FindByIdAsync(int id) => await _dbTable.FindAsync(id) ?? new T();
}
