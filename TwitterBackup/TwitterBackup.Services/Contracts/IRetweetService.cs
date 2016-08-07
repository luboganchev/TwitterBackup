namespace TwitterBackup.Services.Contracts
{
    using System.Collections.Generic;
    using TwitterBackup.Models;

    public interface IRetweetService
    {
        Retweet Save(long retweetId, long createdById, string screenName, long tweetOwnerId);

        int GetTotalRetweetsCount();

        ICollection<Retweet> GetRetweets();
    }
}
