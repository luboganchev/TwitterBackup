namespace TwitterBackup.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> All();

        T GetById(object id);

        T Add(T entity);

        void BulkInsert(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        void DeleteById(object id);
    }
}