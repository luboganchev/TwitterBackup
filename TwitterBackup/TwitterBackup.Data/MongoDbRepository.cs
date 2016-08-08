namespace TwitterBackup.Data
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TwitterBackup.Models;

    public class MongoDbRepository<T> : IRepository<T>
           where T : IEntity
    {
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;

        public MongoDbRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
            this.collection = database.GetCollection<BsonDocument>(typeof(T).Name);
        }

        public IQueryable<T> All()
        {
            var bsonValues = collection.Find(new BsonDocument()).ToList();
            var values = bsonValues.Select(bsonValue => BsonSerializer.Deserialize<T>(bsonValue));

            return values.AsQueryable();
        }

        public T Add(T entity)
        {
            var valueAsBson = entity.ToBsonDocument();
            collection.InsertOne(valueAsBson);
            var values = valueAsBson.Select(bsonValue => BsonSerializer.Deserialize<T>(valueAsBson)).FirstOrDefault();

            return values;
        }

        T IRepository<T>.Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            var objectId = new ObjectId(id.ToString());
            var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
            this.collection.DeleteOneAsync(filter);
        }

        public void Delete(T entity)
        {
            this.Delete(entity.Id);
        }
    }
}
