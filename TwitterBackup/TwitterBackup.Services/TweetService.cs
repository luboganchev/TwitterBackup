namespace TwitterBackup.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TwitterBackup.Common.Constants;
    using TwitterBackup.Data;
    using TwitterBackup.Models;
    using TwitterBackup.Services.Contracts;
    using TwitterBackup.Services.Exceptions;

    public class TweetService : ITweetService
    {
        private readonly IRepository<Tweet> tweetRepo;

        public TweetService()
        {
            tweetRepo = new MongoDbRepository<Tweet>(Database.ConnectionString, Database.DatabaseName);
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

        public int GetTotalTweetsCount()
        {
            var tweetsCount = tweetRepo
                .All()
                .Count();

            return tweetsCount;
        }

        public ICollection<Tweet> GetTweets()
        {
            var tweets = tweetRepo
                .All()
                .ToArray();

            return tweets;
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
