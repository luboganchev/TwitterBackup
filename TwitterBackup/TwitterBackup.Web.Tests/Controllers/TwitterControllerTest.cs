namespace TwitterBackup.Web.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Configuration;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Results;
    using TwitterBackup.Services.Contracts;
    using TwitterBackup.Web.Controllers;
    using TwitterBackup.Web.Helpers;
    using TwitterBackup.Web.Tests.TestObjects;

    [TestClass]
    public class TwitterControllerTest
    {
        private ITweetService tweetService;
        private IRetweetService retweetService;
        private IUserService userService;

        [TestInitialize]
        public void Init()
        {
            this.tweetService = MockObjectFactory.GetTweetService();
            this.retweetService = MockObjectFactory.GetRetweetService();
            this.userService = MockObjectFactory.GetUserService();
        }

        #region StoreTweet

        [TestMethod]
        public void StoreTweet_WithInvalidModel_ShouldReturnBadRequest()
        {
            var controller = new TwitterController(this.tweetService, this.retweetService, this.userService);
            controller.Configuration = new HttpConfiguration();
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
            var controller = new TwitterController(this.tweetService, this.retweetService, this.userService);
            controller.Configuration = new HttpConfiguration();

            var validModel = MockObjectFactory.GetValidTweetViewModel();
            controller.Validate(validModel);
            BaseController.authUser = new MockedIAuthenticatedUser(123456);
            var result = controller.StoreTweet(validModel);
            var okResult = result as OkResult;

            Assert.IsTrue(controller.ModelState.IsValid);
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public void StoreTweet_WithAlreadyStoredTweet_ShouldReturnBadRequest()
        {
            var controller = new TwitterController(this.tweetService, this.retweetService, this.userService);
            controller.Configuration = new HttpConfiguration();

            var validModel = MockObjectFactory.GetValidTweetViewModelWhoThrowsException();
            controller.Validate(validModel);
            BaseController.authUser = new MockedIAuthenticatedUser(123456);
            var result = controller.StoreTweet(validModel);
            var badResult = result as BadRequestErrorMessageResult;

            Assert.IsTrue(controller.ModelState.IsValid);
            Assert.IsNotNull(badResult);
            Assert.AreEqual("Tweet is already saved", badResult.Message);
        }

        #endregion StoreTweet
    }
}
