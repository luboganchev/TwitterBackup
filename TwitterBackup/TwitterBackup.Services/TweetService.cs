namespace TwitterBackup.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TwitterBackup.Data;
    using TwitterBackup.Models;
    using TwitterBackup.Services.Contracts;
    using TwitterBackup.Services.Exceptions;

    public class TweetService : ITweetService
    {
        private readonly IRepository<Tweet> tweetRepo;

        public TweetService(IRepository<Tweet> tweetRepo)
        {
            this.tweetRepo = tweetRepo;
        }

        public Tweet Save(Tweet tweet)
        {
            if (tweet == null)
            {
                throw new ArgumentException("Tweet is not valid");
            }

            var hasAlreadySavedTweet = tweetRepo
                .All()
                .Any(tweetDTO => tweetDTO.TweetTwitterId == tweet.TweetTwitterId && tweetDTO.CreatedByScreenName == tweet.CreatedByScreenName);

            if (hasAlreadySavedTweet)
            {
                throw new TweetException(TweetExceptionType.IsAlreadySaved);
            }

            var dbTweet = tweetRepo.Add(tweet);

            return dbTweet;
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

        public ICollection<Tweet> GetTweetsForFriend(string currentLoggedUserScreenName, string friendScreenName)
        {
            var tweets = tweetRepo
                .All()
                .Where(tweet => tweet.CreatedByScreenName == currentLoggedUserScreenName && tweet.Owner.ScreenName == friendScreenName)
                .ToArray();

            return tweets;
        }
    }
}
