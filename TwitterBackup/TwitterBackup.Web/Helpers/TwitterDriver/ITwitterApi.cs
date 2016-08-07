namespace TwitterBackup.Web.Helpers.TwitterDriver
{
    using System.Collections.Generic;
    using Tweetinvi.Models;

    public interface ITwitterApi
    {
        ITweet PublishRetweet(long tweetId);

        IUser GetUserFromId(long userId);

        IUser GetUserFromScreenName(string screenName);

        IEnumerable<ITweet> GetUserTimeline(long userId);

        IEnumerable<ITweet> GetUserTimeline(string screenName);

        IEnumerable<IUser> SearchUsers(string keyword, int maxResults);
    }
}
