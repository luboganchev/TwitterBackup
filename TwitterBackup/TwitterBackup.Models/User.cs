namespace TwitterBackup.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    public class User : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonRequired]
        public long UserTwitterId { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string ScreenName { get; set; }

        public string Description { get; set; }

        public string ProfileBannerUrl { get; set; }

        public string ProfileImageUrl { get; set; }

        public string ProfileLinkColor { get; set; }

        public int FriendsCount { get; set; }

        public int FollowersCount { get; set; }

        public int StatusesCount { get; set; }
    }
}
