using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBackup.Models;

namespace TwitterBackup.Data
{
    public class MongoDbRepository<T> : IRepository<T>
           where T : IEntity
    {
        private IMongoDatabase database;
        public IMongoCollection<T> collection;


        //    public static IMongoDatabase _database;
        //public static IMongoCollection<T> _collection;

        public MongoDbRepository(IMongoDatabase db)
        {
            GetDatabase();
            GetCollection();
            //this.db = db;
        }

        public async Task<IQueryable<T>> All()
        {
            //return this.collection.AsQueryable();// database.GetCollection<T>(typeof(T).Name);
            //var bsonValues = collection.Find(new BsonDocument()).ToListAsync();
            //var values = bsonValues.Select(bsonValue => BsonSerializer.Deserialize<T>(bsonValue));

            //return values.AsQueryable();

            //var collection = db.GetCollection<BsonDocument>(this.collectionName);
            //var bsonValues = this.collection.Find(new BsonDocument()).ToList();
            //var values = bsonValues.Select(bsonValue => BsonSerializer.Deserialize<T>(bsonValue));

            //return values.AsQueryable();


            var collection = database.GetCollection<BsonDocument>(typeof(T).Name);
            var bsonValues = await collection.Find(new BsonDocument()).ToListAsync();
            var values = bsonValues.Select(bsonValue => BsonSerializer.Deserialize<T>(bsonValue));

            return values.AsQueryable();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
            //return this.collection.Find<T>(id);
        }

        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void GetDatabase()
        {
            var _dbName = "123";// ConfigurationManager.AppSettings["MongoDbDatabaseName"];
            var _connectionString = "123";// ConfigurationManager.AppSettings["MongoDbConnectionString"];

            _connectionString = _connectionString.Replace("{DB_NAME}", _dbName);

            var client = new MongoClient(_connectionString);
            database = client.GetDatabase(_dbName);
        }

        private void GetCollection()
        {
            collection = database.GetCollection<T>(typeof(T).Name);
        }

        private MongoCollectionBase<T> GetCollection<T>()
        {
            return database.GetCollection<T>(typeof(T).Name) as MongoCollectionBase<T>;
        }

        public IEnumerable<T> List()
        {
            var _result = GetCollection<T>().AsQueryable<T>().ToList(); ;

            return _result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(IMongoCollection<T> collection)
        {
            return await collection.Find(f => true).ToListAsync();
        }
    }
}
