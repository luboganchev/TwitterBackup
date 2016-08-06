namespace TwitterBackup.Services.Contracts
{
    using System.Collections.Generic;
    using TwitterBackup.Models;

    public interface IRetweetService
    {
        Retweet Save(long retweetId, long createdById, long tweetOwnerId);

        int GetRetweetsCount(long currentLoggedUserId);

        ICollection<Retweet> GetRetweetsForFriends(long currentLoggedUserId, ICollection<long> friendsIds);

        int GetRetweetsCountForFriend(long currentLoggedUserId, long friendId);
    }
}
