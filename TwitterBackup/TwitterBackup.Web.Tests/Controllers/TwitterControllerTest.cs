namespace TwitterBackup.Web.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Tweetinvi.Models;
    using TwitterBackup.Services.Contracts;
    using TwitterBackup.Web.Controllers;
    using TwitterBackup.Web.Helpers.TwitterDriver;
    using TwitterBackup.Web.Models.Users;
    using TwitterBackup.Web.Tests.TestObjects;
    using System.Linq;

    [TestClass]
    public class TwitterControllerTest
    {
        private ITweetService tweetService;
        private IRetweetService retweetService;
        private IUserService userService;
        private ITwitterApi twitterApi;
        private TwitterController controller;

        [TestInitialize]
        public void Init()
        {
            this.tweetService = MockObjectFactory.GetTweetService();
            this.retweetService = MockObjectFactory.GetRetweetService();
            this.userService = MockObjectFactory.GetUserService();
            this.twitterApi = MockObjectFactory.GetTwitterApi();
            this.controller = new TwitterController(this.tweetService, this.retweetService, this.userService, this.twitterApi);
            this.controller.Configuration = new HttpConfiguration();
        }

        #region GetFriends

        [TestMethod]
        public void GetFriends_WithCollection_ShouldReturnOkRequestWithData()
        {
            BaseController.authUser = new MockedIAuthenticatedUser(123456, MockObjectFactory.GetValidUserName());

            var result = controller.GetFriends();
            var okResult = result as OkNegotiatedContentResult<UserShortInfoViewModel[]>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(3, okResult.Content.Length);

            var firstElement = okResult.Content[0];
            Assert.AreEqual(MockObjectFactory.GetValidUserName(), firstElement.ScreenName);
            Assert.AreEqual(MockObjectFactory.GetValidUserId(), firstElement.Id);
            Assert.AreEqual(true, firstElement.Following);
        }

        [TestMethod]
        public void GetFriends_WithUserWithoutFriends_ShouldReturnOkRequestWithData()
        {
            BaseController.authUser = new MockedIAuthenticatedUser(123456, MockObjectFactory.GetInvalidUserName());

            var result = controller.GetFriends();
            var okResult = result as OkNegotiatedContentResult<UserShortInfoViewModel[]>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(0, okResult.Content.Length);
        }

        #endregion GetFriends

        #region UnfollowFriend

        [TestMethod]
        public void UnFollow_followedFriend_ShouldReturnOkRequest()
        {
            BaseController.authUser = new MockedIAuthenticatedUser(123456, "ivan");
            string validUserName = MockObjectFactory.GetValidUserName();

            var result = controller.UnfollowFriend(validUserName);
            var okResult = result as OkNegotiatedContentResult<bool>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(true, okResult.Content);
        }

        [TestMethod]
        public void UnFollow_NotFollowedOrExistingFriend_ShouldReturnBadRequest()
        {
            BaseController.authUser = new MockedIAuthenticatedUser(123456, "ivan");
            string invalidUserName = MockObjectFactory.GetInvalidUserName();

            var result = controller.UnfollowFriend(invalidUserName);
            var badResult = result as BadRequestErrorMessageResult;

            Assert.IsNotNull(badResult);
            Assert.AreEqual("This user doesn't exist or it's already unfollowed", badResult.Message);
        }

        #endregion UnfollowFriend

        #region SearchFriends

        [TestMethod]
        public void SearchFriends_WithKeywordThatMatch_ShouldReturnOkRequestWithData()
        {
            var result = controller.SearchFriends(MockObjectFactory.GetValidUserName());
            var okResult = result as OkNegotiatedContentResult<UserShortInfoViewModel[]>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(3, okResult.Content.Length);

            var firstElement = okResult.Content[0];
            Assert.AreEqual(MockObjectFactory.GetValidUserName(), firstElement.ScreenName);
            Assert.AreEqual(MockObjectFactory.GetValidUserId(), firstElement.Id);
            Assert.AreEqual(true, firstElement.Following);
        }

        [TestMethod]
        public void SearchFriends_WithKeywordThatDoesntMatch_ShouldReturnOkRequestWithData()
        {
            var result = controller.SearchFriends(MockObjectFactory.GetInvalidUserName());
            var okResult = result as OkNegotiatedContentResult<UserShortInfoViewModel[]>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(0, okResult.Content.Length);
        }

        #endregion SearchFriends

        #region FollowFriend

        [TestMethod]
        public void Follow_UnfollowedFriend_ShouldReturnOkRequest()
        {
            BaseController.authUser = new MockedIAuthenticatedUser(123456, "ivan");
            string validUserName = MockObjectFactory.GetValidNotFollowingUserName();

            var result = controller.FollowFriend(validUserName);
            var okResult = result as OkNegotiatedContentResult<bool>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(true, okResult.Content);
        }

        [TestMethod]
        public void Follow_NotExistingFriend_ShouldReturnBadRequest()
        {
            string invalidUserName = MockObjectFactory.GetInvalidUserName();

            var result = controller.FollowFriend(invalidUserName);
            var badResult = result as BadRequestErrorMessageResult;

            Assert.IsNotNull(badResult);
            Assert.AreEqual("This user doesn't exist", badResult.Message);
        }

        [TestMethod]
        public void Follow_AlreadyFollowedFriend_ShouldReturnBadRequest()
        {
            string validUserName = MockObjectFactory.GetValidUserName();

            var result = controller.FollowFriend(validUserName);
            var badResult = result as BadRequestErrorMessageResult;

            Assert.IsNotNull(badResult);
            Assert.AreEqual("This user is already followed", badResult.Message);
        }

        #endregion FollowFriend

        #region UserDetails

        [TestMethod]
        public void UserDetails_NotExistingFriend_ShouldReturnBadRequest()
        {
            string invalidUserName = MockObjectFactory.GetInvalidUserName();

            var result = controller.UserDetails(invalidUserName);
            var badResult = result as BadRequestErrorMessageResult;

            Assert.IsNotNull(badResult);
            Assert.AreEqual("This user doesn't exist", badResult.Message);
        }

        [TestMethod]
        public void UserDetails_WithExistingFriend_ShouldReturnOkRequest()
        {
            BaseController.authUser = new MockedIAuthenticatedUser(MockObjectFactory.GetValidUserId(), MockObjectFactory.GetValidUserName());
            string validUserName = MockObjectFactory.GetValidUserName();

            var result = controller.UserDetails(validUserName);
            var okResult = result as OkNegotiatedContentResult<UserViewModel>;

            Assert.IsNotNull(okResult.Content);

            var modelResult = okResult.Content;

            Assert.IsTrue(modelResult.Tweets.Any(tweet => tweet.HasStored == true));
            Assert.AreEqual(MockObjectFactory.GetValidUserId(), modelResult.UserTwitterId);
            Assert.AreEqual(MockObjectFactory.GetValidUserName(), modelResult.ScreenName);
            Assert.AreEqual(3, modelResult.Tweets.Count);
        }

        #endregion UserDetails

        #region Retweet

        [TestMethod]
        public void Retweet_WithExistingTweet_ShouldReturnOkRequest()
        {
            BaseController.authUser = new MockedIAuthenticatedUser(123456, "ivan");
            long validTweetId = MockObjectFactory.GetValidTweetId();

            var result = controller.Retweet(validTweetId);
            var okResult = result as OkNegotiatedContentResult<bool>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(true, okResult.Content);
        }

        [TestMethod]
        public void Retweet_WhenAlreadyRetweeted_ShouldReturnBadRequest()
        {
            BaseController.authUser = new MockedIAuthenticatedUser(123456, "ivan");
            long validTweetId = MockObjectFactory.GetValidTweetIdWhoIsAlreadyRetweetedId();

            var result = controller.Retweet(validTweetId);
            var badResult = result as BadRequestErrorMessageResult;

            Assert.IsNotNull(badResult);
            Assert.AreEqual("Tweet is already retweeted from you", badResult.Message);
        }

        [TestMethod]
        public void Retweet_WithNonExistingTweet_ShouldReturnBadRequest()
        {
            long invalidTweetId = MockObjectFactory.GetInvalidTweetId();

            var result = controller.Retweet(invalidTweetId);
            var badResult = result as BadRequestErrorMessageResult;

            Assert.IsNotNull(badResult);
            Assert.AreEqual("This tweet doesn't exist or is already retweeted", badResult.Message);
        }

        #endregion Retweet

        #region StoreTweet

        [TestMethod]
        public void StoreTweet_WithInvalidModel_ShouldReturnBadRequest()
        {
            var invalidModel = MockObjectFactory.GetInvalidTweetViewModel();
            controller.Validate(invalidModel);

            var result = controller.StoreTweet(invalidModel);
            var badResult = result as BadRequestErrorMessageResult;

            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.IsNotNull(badResult);
            Assert.AreEqual("Tweet that you're trying to save has some invalid arguments", badResult.Message);
        }

        [TestMethod]
        public void StoreTweet_WithValidModel_ShouldReturnOk()
        {
            var validModel = MockObjectFactory.GetValidTweetViewModel();
            controller.Validate(validModel);
            BaseController.authUser = new MockedIAuthenticatedUser(123456, "ivan");
            var result = controller.StoreTweet(validModel);
            var okResult = result as OkResult;

            Assert.IsTrue(controller.ModelState.IsValid);
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public void StoreTweet_WithAlreadyStoredTweet_ShouldReturnBadRequest()
        {
            var validModel = MockObjectFactory.GetValidTweetViewModelWhoThrowsException();
            controller.Validate(validModel);
            BaseController.authUser = new MockedIAuthenticatedUser(123456, "ivan");
            var result = controller.StoreTweet(validModel);
            var badResult = result as BadRequestErrorMessageResult;

            Assert.IsTrue(controller.ModelState.IsValid);
            Assert.IsNotNull(badResult);
            Assert.AreEqual("Tweet is already saved", badResult.Message);
        }

        #endregion StoreTweet
    }
}
