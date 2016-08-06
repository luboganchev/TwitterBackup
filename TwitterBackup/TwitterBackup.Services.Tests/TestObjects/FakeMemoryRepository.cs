namespace TwitterBackup.Services.Tests.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TwitterBackup.Data;
    using TwitterBackup.Models;

    internal class FakeMemoryRepository<T>: IRepository<T>
        where T : IEntity
    {
        private readonly IList<T> data;

        public FakeMemoryRepository()
        {
            this.data = new List<T>();
        }

        public IList<T> UpdatedEntities { get; private set; }

        public int NumberOfSaves { get; private set; }

        public IQueryable<T> All()
        {
            return this.data.AsQueryable();
        }

        public T GetById(object id)
        {
            if (this.data.Count == 0)
            {
                throw new InvalidOperationException("No objects in database");
            }

            return this.data[0];
        }

        public T Add(T entity)
        {
            this.data.Add(entity);

            return entity;
        }

        public void Add(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            this.UpdatedEntities.Add(entity);

            return entity;
        }

        public void Delete(object id)
        {
            if (this.data.Count == 0)
            {
                throw new InvalidOperationException("Nothing to delete");
            }

            this.data.Remove(this.data[0]);
        }

        public void Delete(T entity)
        {
            if (!this.data.Contains(entity))
            {
                throw new InvalidOperationException("Entity not found");
            }

            this.data.Remove(entity);
        }
    }
}
