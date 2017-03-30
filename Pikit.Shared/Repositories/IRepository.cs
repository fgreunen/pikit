using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikit.Shared.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}