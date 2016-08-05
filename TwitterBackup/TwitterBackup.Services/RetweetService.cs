namespace TwitterBackup.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TwitterBackup.Data;
    using TwitterBackup.Models;

    public class RetweetService
    {
        private readonly IRepository<Retweet> retweetRepo;

        public RetweetService(string connectionString, string databaseName)
        {
            retweetRepo = new MongoDbRepository<Retweet>(connectionString, databaseName);
        }

        public Retweet Save(long retweetId, long createdById, long tweetOwnerId)
        {
            var dataModel = new Retweet
            {
                DateCreated = DateTime.Now,
                ReweetTwitterId = retweetId,
                CreatedById = createdById,
                TweetOwnerId = tweetOwnerId
            };

            var dbTweet = retweetRepo.Add(dataModel);

            return dbTweet;
        }

        public int GetRetweetsCount(long currentLoggedUserId)
        {
            var retweetsCount = retweetRepo
                .All()
                .Where(tweet => tweet.CreatedById == currentLoggedUserId)
                .Count();

            return retweetsCount;
        }

        public ICollection<Retweet> GetRetweetsForFriends(long currentLoggedUserId, ICollection<long> friendsIds)
        {
            var tweets = retweetRepo
                .All()
                .Where(retweet => retweet.CreatedById == currentLoggedUserId && friendsIds.Contains(retweet.TweetOwnerId))
                .ToArray();

            return tweets;
        }


        public int GetRetweetsCountForFriend(long currentLoggedUserId, long friendId)
        {
            var retweetsCount = retweetRepo
                .All()
                .Where(tweet => tweet.CreatedById == currentLoggedUserId && tweet.TweetOwnerId == friendId)
                .Count();

            return retweetsCount;
        }
    }
}
