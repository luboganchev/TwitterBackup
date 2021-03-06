﻿namespace TwitterBackup.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;

    public class Tweet : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonRequired]
        public long TweetTwitterId { get; set; }

        [BsonRequired]
        public long CreatedById { get; set; }

        public string CreatedByScreenName{ get; set; }

        public User Owner { get; set; }

        [BsonRequired]
        public DateTime CreatedAt { get; set; }

        [BsonRequired]
        public string Text { get; set; }

        [BsonRequired]
        public string FullText { get; set; }

        [BsonRequired]
        public int FavoriteCount { get; set; }

        [BsonRequired]
        public int RetweetCount { get; set; }

        [BsonRequired]
        public bool RetweetedFromMe { get; set; }

        public bool IsRetweet { get; set; }

        public User RetweetFrom { get; set; }
    }
}
