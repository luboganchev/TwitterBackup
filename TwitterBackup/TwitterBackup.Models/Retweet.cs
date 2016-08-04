namespace TwitterBackup.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;

    public class Retweet : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        public long ReweetTwitterId { get; set; }

        public DateTime DateCreated { get; set; }

        public long CreatorId { get; set; }

        public long RetweetedFromId { get; set; }
    }
}
