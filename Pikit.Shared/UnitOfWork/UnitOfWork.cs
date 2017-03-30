using Pikit.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikit.Shared.UnitOfWork
{
    public class UnitOfWork<TContext>
        : DisposableBase, IUnitOfWork
        where TContext : IDbContext, new()
    {
        private IDbContext _ctx;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork()
        {
            _ctx = new TContext();
            _repositories = new Dictionary<System.Type, object>();
            _disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repository = new Repository<TEntity>(_ctx);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        protected override void DoDispose()
        {
            if (_ctx != null)
            {
                _ctx.Dispose();
                _ctx = null;
            }
        }
    }
}
