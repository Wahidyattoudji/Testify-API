using System.Linq.Expressions;

namespace Testify.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> SearchAsync(string searchItem);
        Task<T> FindByIdAsync(int id);
        Task<IEnumerable<T>> FindByFunctionAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
