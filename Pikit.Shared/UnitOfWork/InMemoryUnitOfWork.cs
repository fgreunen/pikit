using Pikit.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Shared.UnitOfWork
{
    public class InMemoryUnitOfWork
        : DisposableBase, IUnitOfWork
    {
        private Dictionary<Type, object> _repositories;

        public InMemoryUnitOfWork()
        {
            _repositories = new Dictionary<System.Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repository = new InMemoryRepository<TEntity>();
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void Save()
        {
            // Ignore
        }
    }
}