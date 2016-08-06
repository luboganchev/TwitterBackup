namespace TwitterBackup.Web.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Configuration;
    using System.Web.Http;
    using System.Web.Http.Results;
    using TwitterBackup.Services.Contracts;
    using TwitterBackup.Web.Controllers;
    using TwitterBackup.Web.Helpers;

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

        [TestMethod]
        public void StoreTweetShouldReturnBadRequestWithInvalidModel()
        {
            var controller = new TwitterController(this.tweetService, this.retweetService, this.userService);
            controller.Configuration = new HttpConfiguration();
            var invalidModel = MockObjectFactory.GetInvalidModel();
            controller.Validate(invalidModel);

            var result = controller.StoreTweet(invalidModel);
            var badResult = result as BadRequestErrorMessageResult;

            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.IsNotNull(badResult);
            Assert.AreEqual("Tweet that you're trying to save has some invalid arguments", badResult.Message);
        }
    }
}
