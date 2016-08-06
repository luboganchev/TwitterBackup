namespace TwitterBackup.Services.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TwitterBackup.Services.Tests.TestObjects;
    using TwitterBackup.Models;
    using TwitterBackup.Services.Exceptions;

    [TestClass]
    public class TweetServiceTests
    {
        private FakeMemoryRepository<Tweet> tweetRepo;
        private TweetService tweetService;

        [TestInitialize]
        public void Init()
        {
            this.tweetRepo = new FakeMemoryRepository<Tweet>();
            this.tweetService = new TweetService(tweetRepo);
        }

        #region Save

        [TestMethod]
        public void AddShouldPopulateDatabaseCorrect()
        {
            var dateTimeNow = DateTime.Now;
            var twitterId = 10023012321;
            var creatorId = 123465798;
            var tweet = this.GetTweetObject(twitterId, dateTimeNow, creatorId, "Sample text Sample text");
            var tweetId = tweet.Id;
            this.tweetService.Save(tweet);
            var storedTweet = this.tweetRepo.GetById(tweetId);

            Assert.IsNotNull(storedTweet);
            Assert.AreEqual(twitterId, storedTweet.TweetTwitterId);
            Assert.AreEqual("Sample text", storedTweet.Text);
            Assert.AreEqual(dateTimeNow, storedTweet.CreatedAt);
            Assert.AreEqual(2, storedTweet.RetweetCount);
            Assert.AreEqual(4, storedTweet.FavoriteCount);
            Assert.AreEqual("Sample text Sample text", storedTweet.FullText);
            Assert.AreEqual(creatorId, storedTweet.CreatedById);
            Assert.AreEqual(false, storedTweet.RetweetedFromMe);
        }

        [ExpectedException(typeof(TweetException))]
        [TestMethod]
        public void DoubleAddingSameTweetThrowException()
        {
            var dateTimeNow = DateTime.Now;
            var twitterId = 1111111;
            var creatorId = 123465798;
            var tweet = this.GetTweetObject(twitterId, dateTimeNow, creatorId);
            this.tweetService.Save(tweet);
            this.tweetService.Save(tweet);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddNullTweetThrowExcetpion()
        {
            this.tweetService.Save(null);
        }

        #endregion Save

        #region GetTotalTweetsCount

        [TestMethod]
        public void AddSomeTweetsGetCorrectCount()
        {
            var dateTimeNow = DateTime.Now;
            var creatorId = 123465798;
            var tweetOne = this.GetTweetObject(10023012321, dateTimeNow, creatorId);
            var tweetTwo = this.GetTweetObject(10023012322, dateTimeNow, creatorId);
            var tweetThree = this.GetTweetObject(10023012323, dateTimeNow, creatorId);
            var tweetFour = this.GetTweetObject(10023012324, dateTimeNow, creatorId);
            this.tweetService.Save(tweetOne);
            this.tweetService.Save(tweetTwo);
            this.tweetService.Save(tweetThree);
            this.tweetService.Save(tweetFour);

            var tweetsCount = this.tweetService.GetTotalTweetsCount();
            Assert.AreEqual(4, tweetsCount);
        }

        [TestMethod]
        public void DontAddTweetsGetCorrectCount()
        {
            var tweetsCount = this.tweetService.GetTotalTweetsCount();
            Assert.AreEqual(0, tweetsCount);
        }

        #endregion GetTotalTweetsCount

        #region GetTweets

        [TestMethod]
        public void AddSomeTweetsGetCorrectCollection()
        {
            var dateTimeNow = DateTime.Now;
            var creatorId = 123465798;
            var tweetOne = this.GetTweetObject(10023012321, dateTimeNow, creatorId);
            var tweetTwo = this.GetTweetObject(10023012322, dateTimeNow, creatorId);
            var tweetThree = this.GetTweetObject(10023012323, dateTimeNow, creatorId);
            var tweetFour = this.GetTweetObject(10023012324, dateTimeNow, creatorId);
            this.tweetService.Save(tweetOne);
            this.tweetService.Save(tweetTwo);
            this.tweetService.Save(tweetThree);
            this.tweetService.Save(tweetFour);

            var tweetsCollection = this.tweetService.GetTweets();
            Assert.IsNotNull(tweetsCollection);
            Assert.AreEqual(4, tweetsCollection.Count());

            var firstElement = tweetsCollection.FirstOrDefault();
            Assert.IsNotNull(firstElement);
            Assert.AreEqual(10023012321, firstElement.TweetTwitterId);

            var lastElement = tweetsCollection.LastOrDefault();
            Assert.IsNotNull(lastElement);
            Assert.AreEqual(10023012324, lastElement.TweetTwitterId);
        }

        [TestMethod]
        public void DontAddTweetsGetCorrectCollection()
        {
            var tweetsCollection = this.tweetService.GetTweets();
            Assert.IsNotNull(tweetsCollection);
            Assert.AreEqual(0, tweetsCollection.Count());

            var firstElement = tweetsCollection.FirstOrDefault();
            Assert.IsNull(firstElement);
        }

        #endregion GetTweets

        #region GetTweetsForFriend

        [TestMethod]
        public void GetCorrectCollectionOfTweetsForGivenFriend()
        {
            var dateTimeNow = DateTime.Now;
            var creatorOneId = 123465798;
            var creatorTwoId = 123465799;
            var userOwnerIdOne = 123654;
            var userOwnerIdTwo = 654321;

            var tweetOne = this.GetTweetObject(10023012321, dateTimeNow, creatorOneId, withOwner: true, userTwitterId: userOwnerIdOne);
            var tweetTwo = this.GetTweetObject(10023012322, dateTimeNow, creatorOneId, withOwner: true, userTwitterId: userOwnerIdOne);
            var tweetThree = this.GetTweetObject(10023012323, dateTimeNow, creatorOneId, withOwner: true, userTwitterId: userOwnerIdTwo);
            var tweetFour = this.GetTweetObject(10023012324, dateTimeNow, creatorTwoId, withOwner: true, userTwitterId: userOwnerIdTwo);
            var tweetFive = this.GetTweetObject(10023012325, dateTimeNow, creatorTwoId, withOwner: true, userTwitterId: userOwnerIdTwo);

            this.tweetService.Save(tweetOne);
            this.tweetService.Save(tweetTwo);
            this.tweetService.Save(tweetThree);
            this.tweetService.Save(tweetFour);
            this.tweetService.Save(tweetFive);

            var tweetsForFirstCreatorFirstOwner = this.tweetService.GetTweetsForFriend(creatorOneId, userOwnerIdOne);
            var tweetsForFirstCreatorSecondOwner = this.tweetService.GetTweetsForFriend(creatorOneId, userOwnerIdTwo);
            var tweetsForSecondCreatorFirstOwner = this.tweetService.GetTweetsForFriend(creatorTwoId, userOwnerIdOne);
            var tweetsForSecondCreatorSecondOwner = this.tweetService.GetTweetsForFriend(creatorTwoId, userOwnerIdTwo);

            var tweetsForNonExistingCreatorAndOwner = this.tweetService.GetTweetsForFriend(9156357147, 753159963321);

            Assert.IsNotNull(tweetsForFirstCreatorFirstOwner);
            Assert.IsNotNull(tweetsForFirstCreatorSecondOwner);
            Assert.IsNotNull(tweetsForSecondCreatorFirstOwner);
            Assert.IsNotNull(tweetsForSecondCreatorSecondOwner);
            Assert.IsNotNull(tweetsForNonExistingCreatorAndOwner);

            Assert.AreEqual(2, tweetsForFirstCreatorFirstOwner.Count());
            Assert.AreEqual(1, tweetsForFirstCreatorSecondOwner.Count());
            Assert.AreEqual(0, tweetsForSecondCreatorFirstOwner.Count());
            Assert.AreEqual(2, tweetsForSecondCreatorSecondOwner.Count());
            Assert.AreEqual(0, tweetsForNonExistingCreatorAndOwner.Count());
        }

        #endregion GetTweetsForFriend

        private Tweet GetTweetObject(long tweetTwitterId, DateTime createdAt, long createdById, string fullText = null, bool withOwner = false, long? userTwitterId = null)
        {
            var tweet = new Tweet()
            {
                Id = Guid.NewGuid().ToString(),
                TweetTwitterId = tweetTwitterId,
                Text = "Sample text",
                CreatedAt = createdAt,
                RetweetCount = 2,
                FavoriteCount = 4,
                FullText = fullText ?? "Sample text Sample text",
                CreatedById = createdById,
                RetweetedFromMe = false
            };

            if (withOwner)
            {
                tweet.Owner = new User
                {
                    UserTwitterId = userTwitterId ?? 456789123
                };
            }

            return tweet;
        }
    }
}
