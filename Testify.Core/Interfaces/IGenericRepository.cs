using System.Linq.Expressions;

namespace Testify.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<IEnumerable<T>> FindByFunctionAsync(Expression<Func<T, bool>> predicate, string[] includes = null);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entitys);

        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
