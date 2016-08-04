using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBackup.Data;
using TwitterBackup.Models;

namespace TwitterBackup.Services
{
    public class RetweetService
    {
        private readonly IRepository<Retweet> retweetRepo;

        public RetweetService(string connectionString, string databaseName)
        {
            retweetRepo = new MongoDbRepository<Retweet>(connectionString, databaseName);
        }

        public Retweet Save(Retweet tweet)
        {
            var dbTweet = retweetRepo.Add(tweet);

            return dbTweet;
        }

        public int GetRetweetsCount(long currentLoggedUserId)
        {
            var retweetsCount = retweetRepo
                .All()
                .Where(tweet => tweet.CreatorId == currentLoggedUserId)
                .Count();

            return retweetsCount;
        }

        public ICollection<Retweet> GetRetweetsForFriends(long currentLoggedUserId, ICollection<long> friendsIds)
        {
            var tweets = retweetRepo
                .All()
                .Where(retweet => retweet.CreatorId == currentLoggedUserId && friendsIds.Contains(retweet.RetweetedFromId))
                .ToArray();

            return tweets;
        }


        public int GetRetweetsCountForFriend(long currentLoggedUserId, long friendId)
        {
            var retweetsCount = retweetRepo
                .All()
                .Where(tweet => tweet.CreatorId == currentLoggedUserId && tweet.RetweetedFromId == friendId)
                .Count();

            return retweetsCount;
        }
    }
}
