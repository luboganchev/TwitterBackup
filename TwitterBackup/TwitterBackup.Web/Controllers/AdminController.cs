namespace TwitterBackup.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using TwitterBackup.Web.Models.Users;
    using TwitterBackup.Web.Models.Admin;
    using Tweetinvi.Models;
    using TwitterBackup.Services.Contracts;

    public class AdminController : BaseController
    {
        private ITweetService tweetService;
        private IRetweetService retweetService;


        public AdminController(ITweetService tweetService, IRetweetService retweetService)
        {
            this.tweetService = tweetService;
            this.retweetService = retweetService;
        }

        [HttpGet]
        public IHttpActionResult GetAdminData()
        {
            //CHECKED! a. Total number of users, downloaded posts and retweets.
            //b. Number of items in the favorite list for every user.
            //c. Number if downloaded posts for every user.
            //d. Number of retweets for every users.

            var twitterFriends = authUser
                .GetFriends();
            var friends = Mapper.Map<IEnumerable<IUser>, ICollection<UserAdminViewModel>>(twitterFriends);
            var friendsIds = friends
                .Select(friend => friend.Id)
                .ToArray();

            var allStoreTweetsForFriends = this.tweetService.GetTweetsForFriends(authUser.Id, friendsIds);
            var allRetweetsForFriends = this.retweetService.GetRetweetsForFriends(authUser.Id, friendsIds);
            foreach (var friend in friends)
            {
                friend.DownloadedPostCount = allStoreTweetsForFriends.Count(tweet => tweet.Owner.UserTwitterId == friend.Id);
                friend.RetweetsCount = allRetweetsForFriends.Count(retweet => retweet.TweetOwnerId == friend.Id);
            }

            var viewModel = new AdminViewModel
            {
                Friends = friends,
                DownloadedTweetsCount = this.tweetService.GetTweetsCount(authUser.Id),
                RetweetsCount = this.retweetService.GetRetweetsCount(authUser.Id)
            };

            return Ok(viewModel);
        }
    }
}
