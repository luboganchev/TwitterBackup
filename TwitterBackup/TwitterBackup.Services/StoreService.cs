namespace TwitterBackup.Services
{
    using TwitterBackup.Models;
    using TwitterBackup.Data;

    public class StoreService
    {
        private readonly IRepository<User> userRepo;
        private readonly IRepository<Tweet> tweetRepo;

        public StoreService(string connectionString, string databaseName)
        {
            userRepo = new MongoDbRepository<User>(connectionString, databaseName);
            tweetRepo = new MongoDbRepository<Tweet>(connectionString, databaseName);
        }

        public Tweet StoreTweet(Tweet tweet)
        {
            var dbTweet = tweetRepo.Add(tweet);

            return dbTweet;
        }
    }
}
