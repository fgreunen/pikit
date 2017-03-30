using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Shared.Repositories
{
    public class InMemoryRepository<T>
        : IRepository<T> where T : class
    {
        private readonly List<T> _entities;

        public InMemoryRepository()
        {
            _entities = new List<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _entities.AsQueryable();
        }

        public virtual void Create(T entity)
        {
            _entities.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            // TODO?
        }
    }
}