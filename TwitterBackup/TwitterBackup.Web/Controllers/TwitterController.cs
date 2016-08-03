namespace TwitterBackup.Web.Controllers
{
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Tweetinvi;
    using Tweetinvi.Models;
    using TwitterBackup.Web.Helpers;
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
        public IHttpActionResult FollowFriend(long userId)
        {
            var hasFollowed = authUser.FollowUser(userId);

            if (hasFollowed)
            {
                return this.Ok(true);
            }

            return this.BadRequest("This user doesn't exist or it's already followed");
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
            private const int TWITTER_API_RATE_EXCEEDED_CODE = 88;
            private bool isTwitterApiRateExceeded = false;


            protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
            {
                try
                {
                    Auth.SetCredentials(Auth.ApplicationCredentials);
                    var authenticatedUser = Tweetinvi.User.GetAuthenticatedUser(Auth.ApplicationCredentials);

                    if (authenticatedUser == null)
                    {
                        var latestException = ExceptionHandler.GetLastException();
                        if (latestException != null)
                        {
                            var exceptionCore = latestException.TwitterExceptionInfos.FirstOrDefault().Code;
                            switch (exceptionCore)
                            {
                                case TWITTER_API_RATE_EXCEEDED_CODE:
                                    //Rate limit of twitter is exceeded https://dev.twitter.com/rest/public/rate-limiting
                                    isTwitterApiRateExceeded = true;
                                    break;
                                default:
                                    break;
                            }
                        }

                        return false;
                    }
                    else
                    {
                        authUser = authenticatedUser;

                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
            {
                if (isTwitterApiRateExceeded)
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                }
                else
                {
                    base.HandleUnauthorizedRequest(actionContext);
                }
            }
        }
    }
}
