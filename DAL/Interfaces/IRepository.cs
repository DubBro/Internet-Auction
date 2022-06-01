using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Delete(int id);
    }
}
