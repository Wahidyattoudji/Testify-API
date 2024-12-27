using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Testify.Core.Interfaces;
namespace Testify.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
{
    protected readonly TestifyDbContext _db;

    protected DbSet<T>? _dbTable = null;

    public GenericRepository(TestifyDbContext context)
    {
        _db = context;
        _dbTable = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbTable.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entitys)
    {
        await _dbTable.AddRangeAsync(entitys);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbTable.Update(entity);
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await FindByIdAsync(id);
        if (entity != null)
        {
            _dbTable.Remove(entity);
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbTable.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<T>> FindByFunctionAsync(Expression<Func<T, bool>> predicate, string[] includes = null)
    {
        IQueryable<T> query = _dbTable;

        if (includes != null)
            foreach (var incluse in includes)
                query = query.Include(incluse);

        return await query.Where(predicate).AsNoTracking().ToListAsync();
    }

    public async Task<T> FindByIdAsync(int id) => await _dbTable.FindAsync(id);
}
