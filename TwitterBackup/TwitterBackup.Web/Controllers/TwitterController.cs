﻿namespace TwitterBackup.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Tweetinvi.Models;
    using TBModels = TwitterBackup.Models;
    using TwitterBackup.Services.Contracts;
    using TwitterBackup.Services.Exceptions;
    using TwitterBackup.Web.Helpers.Filters;
    using TwitterBackup.Web.Models.Tweets;
    using TwitterBackup.Web.Models.Users;
    using TwitterBackup.Web.Helpers.TwitterDriver;

    [TwitterAuthorization]
    public class TwitterController : BaseController
    {
        private ITweetService tweetService;

        private IRetweetService retweetService;

        private IUserService userService;

        private ITwitterApi twitterApi;

        public TwitterController(ITweetService tweetService, IRetweetService retweetService, IUserService userService, ITwitterApi twitterApi)
        {
            this.tweetService = tweetService;
            this.retweetService = retweetService;
            this.userService = userService;
            this.twitterApi = twitterApi;
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
            var authUser = TwitterAuth.SetAuthenticatedUser(request);
            if (authUser != null)
            {
                var userViewModel = Mapper.Map<IUser, UserViewModel>(authUser);
                var userDataModel = Mapper.Map<UserViewModel, TBModels.User>(userViewModel);
                try
                {
                    this.userService.Save(userDataModel);
                }
                catch (UserException userException)
                {
                    switch (userException.Type)
                    {
                        case UserExceptionType.IsAlreadySaved:
                            //User is already saved which is okey
                            break;
                    }
                }
            }

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
        public IHttpActionResult UnfollowFriend([FromBody] string screenName)
        {
            var hasUnfollowed = authUser.UnFollowUser(screenName);
            if (hasUnfollowed)
            {
                return this.Ok(true);
            }

            return this.BadRequest("This user doesn't exist or it's already unfollowed");
        }

        [HttpGet]
        public IHttpActionResult SearchFriends(string keyword, int maxResults = 5)
        {
            var users = this.twitterApi
                .SearchUsers(keyword, maxResults)
                .AsQueryable()
                .Project()
                .To<UserShortInfoViewModel>()
                .ToArray();

            return Ok(users);
        }

        [HttpPost]
        public IHttpActionResult FollowFriend([FromBody] string screenName)
        {
            var user = this.twitterApi.GetUserFromScreenName(screenName);
            if (user != null)
            {
                var hasAlreadyFollowed = user.Following;
                if (!hasAlreadyFollowed)
                {
                    var hasFollowed = authUser.FollowUser(screenName);
                    if (hasFollowed)
                    {
                        return this.Ok(true);
                    }

                    return this.BadRequest("This user doesn't exist");
                }

                return this.BadRequest("This user is already followed");
            }

            return this.BadRequest("This user doesn't exist");
        }

        [HttpGet]
        public IHttpActionResult UserDetails(string screenName)
        {
            var userDetails = this.twitterApi.GetUserFromScreenName(screenName);
            if (userDetails != null)
            {
                var userViewModel = Mapper.Map<IUser, UserViewModel>(userDetails);
                var userTweets = this.twitterApi.GetUserTimeline(screenName);
                userViewModel.Tweets = Mapper.Map<IEnumerable<ITweet>, ICollection<TweetViewModel>>(userTweets);

                var tweets = this.tweetService.GetTweetsForFriend(authUser.ScreenName, userViewModel.ScreenName);
                foreach (var tweet in tweets)
                {
                    var storedTweet = userViewModel.Tweets
                        .Where(userTweet => userTweet.IdString == tweet.TweetTwitterId.ToString())
                        .FirstOrDefault();

                    if (storedTweet != null)
                    {
                        storedTweet.HasStored = true;
                    }
                }

                return Ok(userViewModel);
            }

            return this.BadRequest("This user doesn't exist");
        }

        [HttpPost]
        public IHttpActionResult Retweet([FromBody]long tweetId)
        {
            var retweet = this.twitterApi.PublishRetweet(tweetId);
            if (retweet != null)
            {
                try
                {
                    this.retweetService.Save(retweet.Id, authUser.Id, authUser.ScreenName, retweet.RetweetedTweet.CreatedBy.Id);

                    return Ok(true);
                }
                catch (RetweetException retweetException)
                {
                    switch (retweetException.Type)
                    {
                        case RetweetExceptionType.IsAlreadySaved:
                        default:
                            return this.BadRequest("Tweet is already retweeted from you");
                    }
                }
            }

            return this.BadRequest("This tweet doesn't exist or is already retweeted");
        }

        [HttpPost]
        public IHttpActionResult StoreTweet(TweetViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dataModel = Mapper.Map<TwitterBackup.Models.Tweet>(viewModel);
                    dataModel.CreatedById = authUser.Id;
                    dataModel.CreatedByScreenName = authUser.ScreenName;
                    this.tweetService.Save(dataModel);

                    return Ok();
                }
                catch (ArgumentException argException)
                {
                    return this.BadRequest(argException.Message);
                }
                catch (TweetException tweetException)
                {
                    switch (tweetException.Type)
                    {
                        case TweetExceptionType.IsAlreadySaved:
                        default:
                            return this.BadRequest("Tweet is already saved");
                    }
                }
                catch (Exception)
                {
                    return this.BadRequest("Error occured when storing the tweet");
                }
            }

            return this.BadRequest("Tweet that you're trying to save has some invalid arguments");
        }
    }
}
