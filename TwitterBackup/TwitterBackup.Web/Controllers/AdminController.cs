namespace TwitterBackup.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using TwitterBackup.Web.Models.Users;
    using TwitterBackup.Web.Models.Admin;
    using TwitterBackup.Services.Contracts;
    using TwitterBackup.Models;
    using TwitterBackup.Web.Helpers.Filters;

    [AdminAuthorization]
    public class AdminController : BaseController
    {
        private ITweetService tweetService;

        private IRetweetService retweetService;

        private IUserService userService;

        public AdminController(ITweetService tweetService, IRetweetService retweetService, IUserService userService)
        {
            this.tweetService = tweetService;
            this.retweetService = retweetService;
            this.userService = userService;
        }

        [HttpGet]
        public IHttpActionResult GetAdminData()
        {
            var allStoreTweets = this.tweetService.GetTweets();
            var allRetweets = this.retweetService.GetRetweets();
            var twitterUsersDataModels = this.userService.GetUsers();
            var twitterBackupUsers = Mapper.Map<IEnumerable<User>, ICollection<UserAdminViewModel>>(twitterUsersDataModels);

            foreach (var user in twitterBackupUsers)
            {
                user.DownloadedPostCount = allStoreTweets.Count(tweet => tweet.CreatedByScreenName == user.ScreenName);
                user.RetweetsCount = allRetweets.Count(retweet => retweet.CreatedByScreenName == user.ScreenName);
            }

            var viewModel = new AdminViewModel
            {
                Users = twitterBackupUsers,
                DownloadedTweetsCount = this.tweetService.GetTotalTweetsCount(),
                RetweetsCount = this.retweetService.GetTotalRetweetsCount()
            };

            return Ok(viewModel);
        }
    }
}
