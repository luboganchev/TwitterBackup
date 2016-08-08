namespace TwitterBackup.Web.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Web.Http;
    using System.Web.Http.Results;
    using TwitterBackup.Services.Contracts;
    using TwitterBackup.Web.Controllers;
    using TwitterBackup.Web.Models.Admin;

    [TestClass]
    public class AdminControllerTest
    {
        private ITweetService tweetService;
        private IRetweetService retweetService;
        private IUserService userService;
        private AdminController controller;

        [TestInitialize]
        public void Init()
        {
            this.tweetService = MockObjectFactory.GetTweetService();
            this.retweetService = MockObjectFactory.GetRetweetService();
            this.userService = MockObjectFactory.GetUserService();
            this.controller = new AdminController(this.tweetService, this.retweetService, this.userService);
            this.controller.Configuration = new HttpConfiguration();
        }

        #region GetAdminData

        [TestMethod]
        public void GetAdminData_WhenCorrectData_ShouldReturnOkRequestWithData()
        {
            var result = this.controller.GetAdminData();
            var okResult = result as OkNegotiatedContentResult<AdminViewModel>;

            var adminViewModel = okResult.Content;
            Assert.IsNotNull(adminViewModel);
            Assert.AreEqual(1, adminViewModel.DownloadedTweetsCount);
            Assert.AreEqual(1, adminViewModel.RetweetsCount);
            Assert.AreEqual(1, adminViewModel.UsersCount);
        }


        #endregion GetAdminData
    }
}
