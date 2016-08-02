using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterBackup.Web.Helpers;
using TwitterBackup.Web.Models.Tweets;
using TwitterBackup.Web.Models.Users;

namespace TwitterBackup.Web.Controllers
{
    [TwitterAuthorization]
    public class TwitterController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Authorize()
        {
            return this.Ok(TwitterAuth.GetAuthorizationData(this.Request));
        }

        [HttpGet]
        public IHttpActionResult GetFriends()
        {
            var friends = TwitterAuth.User.GetFriends();
            var friendsDTO = friends.Select(friend => new { friend.Name, friend.Description, friend.Id });

            return Ok(JsonConvert.SerializeObject(friendsDTO));
        }

        [HttpPost]
        public IHttpActionResult UnfollowFriend(long userId)
        {
            var hasUnfollowed = TwitterAuth.User.UnFollowUser(userId);

            if (hasUnfollowed)
            {
                return this.Ok(true);
            }

            return this.BadRequest("This user doesn't exist or it's already unfollowed");
        }

        [HttpPost]
        public IHttpActionResult FollowFriend(long userId)
        {
            var hasFollowed = TwitterAuth.User.FollowUser(userId);

            if (hasFollowed)
            {
                return this.Ok(true);
            }

            return this.BadRequest("This user doesn't exist or it's already followed");
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult UserDetails(long userId)
        {
            var userDetails = Tweetinvi.User.GetUserFromId(userId);

            //var userDetails = TwitterAuth.User.GetF Friends.FirstOrDefault(user => user.Id == userId);

            if (userDetails != null)
            {
                var userDTO = new UserViewModel
                {
                    Name = userDetails.Name,
                    ScreenName = userDetails.ScreenName,
                    Description = userDetails.Description,
                    ProfileImageUrl = userDetails.ProfileImageUrl,
                    ProfileBannerUrl = userDetails.ProfileBannerURL,
                    ProfileLinkColor = userDetails.ProfileLinkColor,
                    FollowersCount = userDetails.FollowersCount,
                    FriendsCount = userDetails.FriendsCount,
                    StatusesCount = userDetails.StatusesCount
                };

                var tweetCollection = Tweetinvi.Timeline.GetUserTimeline(userId);
                userDTO.Tweets = tweetCollection.Select(tweet => new TweetViewModel
                {
                    CreatedAt = tweet.CreatedAt,
                    CreatedByName = tweet.CreatedBy.Name,
                    FavoriteCount = tweet.FavoriteCount,
                    FullText = tweet.FullText,
                    //Retweet = tweet.RetweetedTweet.e,
                    RetweetCount = tweet.RetweetCount,
                    Retweeted = tweet.Retweeted,
                    Text = tweet.Text
                }).ToArray();

                return Ok(JsonConvert.SerializeObject(userDTO));
            }

            return this.BadRequest("This user doesn't exist");
        }

        [HttpPost]
        public IHttpActionResult Retweet(long tweetId)
        {
            //Check retweet string length
            // From a string (the extension namespace is 'Tweetinvi.Core.Extensions')
            //var twitterLength = "I love https://github.com/linvi/tweetinvi".TweetLength();

            var retweet = Tweet.GetTweet(tweetId).PublishRetweet();

            if (retweet != null)
            {
                return Ok(true);
            }

            return this.BadRequest("This tweet doesn't exist");
        }

        public class TwitterAuthorization : AuthorizeAttribute
        {
            protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
            {
                return TwitterAuth.User != null;
            }
        }
    }
}
