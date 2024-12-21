namespace Testify.Core.Interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepository<T> EntityRepository { get; }
        Task CommitAsync();
        void Rollback();
    }
}
