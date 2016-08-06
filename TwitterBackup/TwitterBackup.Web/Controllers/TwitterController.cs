﻿namespace TwitterBackup.Web.Controllers
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
    using TBModels = TwitterBackup.Models;
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

        private IUserService userService;

        public TwitterController(ITweetService tweetService, IRetweetService retweetService, IUserService userService)
        {
            this.tweetService = tweetService;
            this.retweetService = retweetService;
            this.userService = userService;
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
                this.userService.Save(userDataModel);
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
            if (ModelState.IsValid)
            {
                try
                {
                    var dataModel = Mapper.Map<TwitterBackup.Models.Tweet>(viewModel);
                    dataModel.CreatedById = authUser.Id;
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
