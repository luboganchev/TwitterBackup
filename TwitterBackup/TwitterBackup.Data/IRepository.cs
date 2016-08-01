namespace TwitterBackup.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using TwitterBackup.Models;

    public interface IRepository<T>
        where T : IEntity
    {
        IQueryable<T> All();

        T GetById(object id);

        T Add(T entity);

        void Add(IEnumerable<T> entities);

        T Update(T entity);

        void Delete(T entity);

        void Delete(object id);
    }
}