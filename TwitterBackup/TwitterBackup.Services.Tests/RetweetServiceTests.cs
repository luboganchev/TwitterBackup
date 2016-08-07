namespace TwitterBackup.Services.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TwitterBackup.Services.Tests.TestObjects;
    using TwitterBackup.Models;
    using TwitterBackup.Services.Exceptions;

    [TestClass]
    public class RetweetServiceTests
    {
        private FakeMemoryRepository<Retweet> retweetRepo;
        private RetweetService retweetService;

        [TestInitialize]
        public void Init()
        {
            this.retweetRepo = new FakeMemoryRepository<Retweet>();
            this.retweetService = new RetweetService(retweetRepo);
        }

        #region Save

        [TestMethod]
        public void Population_ShouldPopulateDatabaseCorrect()
        {
            var retweetTwitterId = 10023012321;
            var createdById = 123465798;
            var twitterOwnerId = 4433668899;
            this.retweetService.Save(retweetTwitterId, createdById, twitterOwnerId);
            var storedTweet = this.retweetRepo
                .All()
                .Where(retweet => retweet.ReweetTwitterId == retweetTwitterId)
                .FirstOrDefault();

            Assert.IsNotNull(storedTweet);
            Assert.AreEqual(createdById, storedTweet.CreatedById);
            Assert.AreEqual(twitterOwnerId, storedTweet.TweetOwnerId);
        }

        [ExpectedException(typeof(RetweetException))]
        [TestMethod]
        public void DoubleAddingSameRetweet_ThrowException()
        {
            var retweetTwitterId = 10023012321;
            var createdById = 123465798;
            var twitterOwnerId = 4433668899;
            this.retweetService.Save(retweetTwitterId, createdById, twitterOwnerId);
            this.retweetService.Save(retweetTwitterId, createdById, twitterOwnerId);
        }

        [TestMethod]
        public void DoubleAddingSameRetweetWithDiffrentCreator_ShouldWork()
        {
            var retweetTwitterId = 10023012321;
            var createdByIdOne = 123465798;
            var createdByIdTwo = 9876543211;
            var twitterOwnerId = 4433668899;
            this.retweetService.Save(retweetTwitterId, createdByIdOne, twitterOwnerId);
            this.retweetService.Save(retweetTwitterId, createdByIdTwo, twitterOwnerId);
        }

        #endregion Save

        #region GetTotalRetweetsCount

        [TestMethod]
        public void AddSomeRetweets_GetCorrectCount()
        {
            var retweetTwitterIdOne = 10023012321;
            var retweetTwitterIdTwo = 10023012322;
            var createdByIdOne = 123465798;
            var createdByIdTwo = 9876543211;
            var twitterOwnerId = 4433668899;
            this.retweetService.Save(retweetTwitterIdOne, createdByIdOne, twitterOwnerId);
            this.retweetService.Save(retweetTwitterIdOne, createdByIdTwo, twitterOwnerId);
            this.retweetService.Save(retweetTwitterIdTwo, createdByIdOne, twitterOwnerId);
            this.retweetService.Save(retweetTwitterIdTwo, createdByIdTwo, twitterOwnerId);

            var retweetsCount = this.retweetService.GetTotalRetweetsCount();
            Assert.AreEqual(4, retweetsCount);
        }

        [TestMethod]
        public void DontAddRetweets_GetCorrectCount()
        {
            var retweetsCount = this.retweetService.GetTotalRetweetsCount();
            Assert.AreEqual(0, retweetsCount);
        }

        #endregion GetTotalRetweetsCount

        #region GetRetweets

        [TestMethod]
        public void AddSomeRetweets_GetCorrectCollection()
        {
            var retweetTwitterIdOne = 10023012321;
            var retweetTwitterIdTwo = 10023012322;
            var createdByIdOne = 123465798;
            var createdByIdTwo = 9876543211;
            var twitterOwnerId = 4433668899;
            this.retweetService.Save(retweetTwitterIdOne, createdByIdOne, twitterOwnerId);
            this.retweetService.Save(retweetTwitterIdOne, createdByIdTwo, twitterOwnerId);
            this.retweetService.Save(retweetTwitterIdTwo, createdByIdOne, twitterOwnerId);
            this.retweetService.Save(retweetTwitterIdTwo, createdByIdTwo, twitterOwnerId);

            var retweetsCollection = this.retweetService.GetRetweets();
            Assert.IsNotNull(retweetsCollection);
            Assert.AreEqual(4, retweetsCollection.Count());

            var firstElement = retweetsCollection.FirstOrDefault();
            Assert.IsNotNull(firstElement);
            Assert.AreEqual(10023012321, firstElement.ReweetTwitterId);

            var lastElement = retweetsCollection.LastOrDefault();
            Assert.IsNotNull(lastElement);
            Assert.AreEqual(10023012322, lastElement.ReweetTwitterId);
        }

        [TestMethod]
        public void DontAddRetweets_GetCorrectCollection()
        {
            var retweetsCollection = this.retweetService.GetRetweets();
            Assert.IsNotNull(retweetsCollection);
            Assert.AreEqual(0, retweetsCollection.Count());

            var firstElement = retweetsCollection.FirstOrDefault();
            Assert.IsNull(firstElement);
        }

        #endregion GetRetweets

        #region GetRetweetsCountForFriend

        [TestMethod]
        public void AddSomeRetweets_GetCorrectCollectionOfTweetsForGivenFriend()
        {
            var retweetTwitterIdOne = 10023012321;
            var retweetTwitterIdTwo = 10023012322;
            var retweetTwitterIdThree = 10023012323;
            var createdByIdOne = 123465798;
            var createdByIdTwo = 9876543211;
            var twitterOwnerIdOne = 4433668899;
            var twitterOwnerIdTwo = 4433668810;

            this.retweetService.Save(retweetTwitterIdOne, createdByIdOne, twitterOwnerIdOne);
            this.retweetService.Save(retweetTwitterIdTwo, createdByIdOne, twitterOwnerIdOne);
            this.retweetService.Save(retweetTwitterIdThree, createdByIdOne, twitterOwnerIdTwo);
            this.retweetService.Save(retweetTwitterIdOne, createdByIdTwo, twitterOwnerIdOne);
            this.retweetService.Save(retweetTwitterIdTwo, createdByIdTwo, twitterOwnerIdOne);

            var retweetsForFirstCreatorFirstOwner = this.retweetService.GetRetweetsCountForFriend(createdByIdOne, twitterOwnerIdOne);
            var retweetsForFirstCreatorSecondOwner = this.retweetService.GetRetweetsCountForFriend(createdByIdOne, twitterOwnerIdTwo);
            var retweetsForSecondCreatorFirstOwner = this.retweetService.GetRetweetsCountForFriend(createdByIdTwo, twitterOwnerIdOne);
            var retweetsForSecondCreatorSecondOwner = this.retweetService.GetRetweetsCountForFriend(createdByIdTwo, twitterOwnerIdTwo);

            var retweetsForNonExistingCreatorAndOwner = this.retweetService.GetRetweetsCountForFriend(9156357147, 753159963321);

            Assert.AreEqual(2, retweetsForFirstCreatorFirstOwner);
            Assert.AreEqual(1, retweetsForFirstCreatorSecondOwner);
            Assert.AreEqual(2, retweetsForSecondCreatorFirstOwner);
            Assert.AreEqual(0, retweetsForSecondCreatorSecondOwner);
            Assert.AreEqual(0, retweetsForNonExistingCreatorAndOwner);
        }

        #endregion GetRetweetsCountForFriend
    }
}
