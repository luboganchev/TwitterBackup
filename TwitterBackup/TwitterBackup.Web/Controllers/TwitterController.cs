namespace TwitterBackup.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Tweetinvi;
    using Tweetinvi.Models;
    using TwitterBackup.Services;
    using TwitterBackup.Services.Contracts;
    using TwitterBackup.Services.Exceptions;
    using TwitterBackup.Web.Helpers;
    using TwitterBackup.Web.Helpers.Filters;
    using TwitterBackup.Web.Models.Tweets;
    using TwitterBackup.Web.Models.Users;

    [TwitterAuthorization]
    public class TwitterController : BaseController
    {
        private ITweetService tweetService;

        private IRetweetService retweetService;

        public TwitterController(ITweetService tweetService, IRetweetService retweetService)
        {
            this.tweetService = tweetService;
            this.retweetService = retweetService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Authorize()
        {
            return this.Ok(TwitterAuth.GetAuthorizationData(this.Request));
        }

        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult VerifyUser()
        {
            var context = new HttpContextWrapper(HttpContext.Current);
            HttpRequestBase request = context.Request;
            TwitterAuth.SetAuthenticatedUser(request);
            //TwitterAuth.TrackRateLimits();
            return this.Ok();
        }

        [HttpGet]
        public IHttpActionResult GetFriends()
        {
            var friends = authUser
                .GetFriends()
                .AsQueryable()
                .Project()
                .To<UserShortInfoViewModel>()
                .ToArray();

            return Ok(friends);
        }

        [HttpPost]
        public IHttpActionResult UnfollowFriend([FromBody] long userId)
        {
            var hasUnfollowed = authUser.UnFollowUser(userId);
            if (hasUnfollowed)
            {
                return this.Ok(true);
            }

            return this.BadRequest("This user doesn't exist or it's already unfollowed");
        }

        [HttpGet]
        public IHttpActionResult SearchFriends(string keyword, int maxResults = 5)
        {
            var users = Search
                .SearchUsers(keyword, maxResults)
                .AsQueryable()
                .Project()
                .To<UserShortInfoViewModel>()
                .ToArray();

            return Ok(users);
        }

        [HttpPost]
        public IHttpActionResult FollowFriend([FromBody] long userId)
        {
            var hasAlreadyFollowed = Tweetinvi.User.GetUserFromId(userId).Following;
            if (!hasAlreadyFollowed)
            {
                var hasFollowed = authUser.FollowUser(userId);
                if (hasFollowed)
                {
                    return this.Ok(true);
                }

                return this.BadRequest("This user doesn't exist");
            }

            return this.BadRequest("This user is already followed");
        }

        [HttpGet]
        public IHttpActionResult UserDetails(long userId)
        {
            var userDetails = Tweetinvi.User.GetUserFromId(userId);
            if (userDetails != null)
            {
                var userViewModel = Mapper.Map<IUser, UserViewModel>(userDetails);
                var userTweets = Tweetinvi.Timeline
                    .GetUserTimeline(userId);
                userViewModel.Tweets = Mapper.Map<IEnumerable<ITweet>, ICollection<TweetViewModel>>(userTweets);

                var tweets = this.tweetService.GetTweetsForFriend(authUser.Id, userViewModel.UserTwitterId);
                foreach (var tweet in tweets)
                {
                    var storedTweet = userViewModel.Tweets
                        .Where(userTweet => userTweet.IdString == tweet.TweetTwitterId.ToString())
                        .FirstOrDefault();

                    storedTweet.HasStored = true;
                }

                return Ok(userViewModel);
            }

            return this.BadRequest("This user doesn't exist");
        }

        [HttpPost]
        public IHttpActionResult Retweet([FromBody]long tweetId)
        {
            var retweet = Tweet.PublishRetweet(tweetId);
            if (retweet != null)
            {
                this.retweetService.Save(retweet.Id, authUser.Id, retweet.RetweetedTweet.CreatedBy.Id);

                return Ok(true);
            }

            return this.BadRequest("This tweet doesn't exist or is already retweeted");
        }

        [HttpPost]
        public IHttpActionResult StoreTweet(TweetViewModel viewModel)
        {
            try
            {
                var dataModel = Mapper.Map<TwitterBackup.Models.Tweet>(viewModel);
                dataModel.CreatedById = authUser.Id;
                this.tweetService.Save(dataModel);

                return Ok();
            }
            catch (TweetException tweetException)
            {
                switch (tweetException.Type)
                {
                    case TweetExceptionType.TweetIsAlreadySaved:
                    default:
                        return this.BadRequest("Tweet is already saved");
                }
            }
            catch (Exception)
            {
                return this.BadRequest("Error occured when storing the tweet");
            }
        }
    }
}
