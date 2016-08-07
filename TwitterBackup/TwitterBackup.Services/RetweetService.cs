namespace TwitterBackup.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TwitterBackup.Data;
    using TwitterBackup.Models;
    using TwitterBackup.Services.Contracts;
    using TwitterBackup.Services.Exceptions;

    public class RetweetService : IRetweetService
    {
        private readonly IRepository<Retweet> retweetRepo;

        public RetweetService(IRepository<Retweet> retweetRepo)
        {
            this.retweetRepo = retweetRepo;
        }

        public Retweet Save(long retweetId, long createdById, long tweetOwnerId)
        {
            var hasAlreadySavedRetweet = retweetRepo
                .All()
                .Any(retweet => retweet.ReweetTwitterId == retweetId && retweet.CreatedById == createdById);

            if (hasAlreadySavedRetweet)
            {
                throw new RetweetException(RetweetExceptionType.IsAlreadySaved);
            }

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

        public int GetTotalRetweetsCount()
        {
            var retweetsCount = retweetRepo
               .All()
               .Count();

            return retweetsCount;
        }

        public ICollection<Retweet> GetRetweets()
        {
            var tweets = retweetRepo
                .All()
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
