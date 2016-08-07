namespace TwitterBackup.Services.Contracts
{
    using System.Collections.Generic;
    using TwitterBackup.Models;

    public interface ITweetService
    {
        Tweet Save(Tweet tweet);

        int GetTotalTweetsCount();

        ICollection<Tweet> GetTweets();

        ICollection<Tweet> GetTweetsForFriend(string currentLoggedUserScreenName, string friendScreenName);
    }
}
