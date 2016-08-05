namespace TwitterBackup.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TwitterBackup.Data;
    using TwitterBackup.Models;

    public class TweetService
    {
         private readonly IRepository<Tweet> tweetRepo;

        public TweetService(string connectionString, string databaseName)
        {
            tweetRepo = new MongoDbRepository<Tweet>(connectionString, databaseName);
        }

        public Tweet Save(Tweet tweet)
        {
            var dbTweet = tweetRepo.Add(tweet);

            return dbTweet;
        }

        public int GetTweetsCount(long currentLoggedUserId)
        {
            var tweetsCount = tweetRepo
                .All()
                .Where(tweet => tweet.CreatedById == currentLoggedUserId)
                .Count();

            return tweetsCount;
        }

        public ICollection<Tweet> GetTweetsForFriends(long currentLoggedUserId, ICollection<long> friendsIds)
        {
            var tweets = tweetRepo
                .All()
                .Where(tweet => tweet.CreatedById == currentLoggedUserId && friendsIds.Contains(tweet.Owner.UserTwitterId))
                .ToArray();

            return tweets;
        }

        public int GetTweetsCountForFriend(long currentLoggedUserId, long friendId)
        {
            var retweetsCount = tweetRepo
                .All()
                .Where(tweet => tweet.CreatedById == currentLoggedUserId && tweet.Owner.UserTwitterId == friendId)
                .Count();

            return retweetsCount;
        }
    }
}
