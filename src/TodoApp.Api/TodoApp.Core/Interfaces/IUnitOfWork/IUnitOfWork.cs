using TodoApp.Core.Interfaces.IRepositories;

namespace TodoApp.Core.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IBaseRepository<T> Repository<T>() where T : class;

        Task<int> CommitAsync();

        Task<T> ExecutionStrategyAsync<T>(Func<Task<T>> action, bool isSaveChanges = true, CancellationToken cancellationToken = default);
        Task ExecutionStrategyAsync(Func<Task> action, bool isSaveChanges = true, CancellationToken cancellationToken = default);
        void Commit();

        void Rollback();
    }
}
