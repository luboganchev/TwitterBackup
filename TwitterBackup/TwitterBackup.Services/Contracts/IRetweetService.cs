namespace TwitterBackup.Services.Contracts
{
    using System.Collections.Generic;
    using TwitterBackup.Models;

    public interface IRetweetService
    {
        Retweet Save(long retweetId, long createdById, long tweetOwnerId);

        int GetTotalRetweetsCount();

        ICollection<Retweet> GetRetweets();

        int GetRetweetsCountForFriend(long currentLoggedUserId, long friendId);
    }
}
