using Pikit.Shared.Repository.Infrastructure;
using Pikit.Shared.Repository.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Pikit.Shared.Repository.UnitOfWork
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState;
    }
}