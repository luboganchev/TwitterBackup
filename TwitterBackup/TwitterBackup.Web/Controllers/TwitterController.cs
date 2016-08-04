namespace TwitterBackup.Web.Controllers
{
    using AutoMapper;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Tweetinvi;
    using Tweetinvi.Models;
    using TwitterBackup.Services;
    using TwitterBackup.Web.Helpers;
    using TwitterBackup.Web.Helpers.Filters;
    using TwitterBackup.Web.Models.Tweets;
    using TwitterBackup.Web.Models.Users;

    [TwitterAuthorization]
    public class TwitterController : ApiController
    {
        internal static IAuthenticatedUser authUser;

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
            var friends = authUser.GetFriends();
            var friendsDTO = friends.Select(friend => new UserShortInfoViewModel
            {
                Id = friend.Id,
                Name = friend.Name,
                Description = friend.Description,
                ScreenName = friend.ScreenName,
                ProfileImageUrl = friend.ProfileImageUrl
            });

            return Ok(JsonConvert.SerializeObject(friendsDTO));
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
            var users = Search.SearchUsers(keyword, maxResults).Select(user => new UserShortInfoViewModel 
            {
                Id = user.Id,
                Name = user.Name,
                ScreenName = user.ScreenName,
                ProfileImageUrl = user.ProfileImageUrl,
                Following = user.Following
            });

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
                    Id = tweet.Id,
                    CreatedAt = tweet.CreatedAt,
                    FavoriteCount = tweet.FavoriteCount,
                    FullText = tweet.FullText,
                    //Retweet = tweet.RetweetedTweet.e,
                    RetweetCount = tweet.RetweetCount,
                    RetweetedFromMe = tweet.Retweeted,
                    Text = tweet.Text,
                    Creator = new UserShortInfoViewModel 
                    {
                        Id = tweet.CreatedBy.Id,
                        Name = tweet.CreatedBy.Name,
                        ScreenName = tweet.CreatedBy.ScreenName,
                        ProfileImageUrl = tweet.CreatedBy.ProfileImageUrl
                    },
                    IsRetweet = tweet.IsRetweet,
                    RetweetFrom = tweet.IsRetweet? new UserShortInfoViewModel
                    {
                        Id = tweet.RetweetedTweet.CreatedBy.Id,
                        Name = tweet.RetweetedTweet.CreatedBy.Name,
                        ScreenName = tweet.RetweetedTweet.CreatedBy.ScreenName,
                        ProfileImageUrl = tweet.RetweetedTweet.CreatedBy.ProfileImageUrl
                    } : null
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

            var retweet = Tweet.PublishRetweet(tweetId);

            if (retweet != null)
            {
                return Ok(true);
            }

            return this.BadRequest("This tweet doesn't exist");
        }

        [HttpPost]
        public IHttpActionResult StoreTweet(TweetViewModel viewModel)
        {
            try
            {
                StoreService service = new StoreService(ConfigHelper.ConnectionString, ConfigHelper.DatabaseName);
                var dataModel = Mapper.Map<TwitterBackup.Models.Tweet>(viewModel);
                service.StoreTweet(dataModel);

                return Ok();
            }
            catch (Exception)
            {
                return this.BadRequest("Error occured when storing the tweet");
            }
        }
    }
}
