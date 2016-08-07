namespace TwitterBackup.Web.Helpers.TwitterDriver
{
    using System.Collections.Generic;
    using Tweetinvi;
    using Tweetinvi.Models;

    public class TwitterApiAdapter : ITwitterApi
    {
        public ITweet PublishRetweet(long tweetId)
        {
            return Tweetinvi.Tweet.PublishRetweet(tweetId);
        }

        public IUser GetUserFromId(long userId)
        {
            return Tweetinvi.User.GetUserFromId(userId);
        }

        public IUser GetUserFromScreenName(string screenName)
        {
            return Tweetinvi.User.GetUserFromScreenName(screenName);
        }

        public IEnumerable<ITweet> GetUserTimeline(long userId)
        {
            return Tweetinvi.Timeline.GetUserTimeline(userId);
        }

        public IEnumerable<ITweet> GetUserTimeline(string screenName)
        {
            return Tweetinvi.Timeline.GetUserTimeline(screenName);
        }

        public IEnumerable<IUser> SearchUsers(string keyword, int maxResults)
        {
            return Search.SearchUsers(keyword, maxResults);
        }
    }
}