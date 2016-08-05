namespace TwitterBackup.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TwitterBackup.Data;
    using TwitterBackup.Models;
    using TwitterBackup.Services.Exceptions;

    public class TweetService
    {
         private readonly IRepository<Tweet> tweetRepo;

        public TweetService(string connectionString, string databaseName)
        {
            tweetRepo = new MongoDbRepository<Tweet>(connectionString, databaseName);
        }

        public Tweet Save(Tweet tweet)
        {
            var hasAlreadySavedTweet = tweetRepo
                .All()
                .Any(tweetDTO => tweetDTO.TweetTwitterId == tweet.TweetTwitterId);

            if (hasAlreadySavedTweet)
            {
                throw new TweetException(TweetExceptionType.TweetIsAlreadySaved);
            }

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

        public ICollection<Tweet> GetTweetsForFriend(long currentLoggedUserId, long friendId)
        {
            var tweets = tweetRepo
                .All()
                .Where(tweet => tweet.CreatedById == currentLoggedUserId && tweet.Owner.UserTwitterId == friendId)
                .ToArray();

            return tweets;
        }
    }
}
