namespace TwitterBackup.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using TwitterBackup.Models;

    public interface IRepository<T>
        where T : IEntity
    {
        IQueryable<T> All();

        T Add(T entity);

        T Update(T entity);

        void Delete(T entity);

        void Delete(object id);
    }
}