using Pikit.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikit.Shared.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Save();
    }
}