namespace TwitterBackup.Services.Contracts
{
    using System.Collections.Generic;
    using TwitterBackup.Models;

    public interface ITweetService
    {
        Tweet Save(Tweet tweet);

        int GetTweetsCount(long currentLoggedUserId);

        ICollection<Tweet> GetTweetsForFriends(long currentLoggedUserId, ICollection<long> friendsIds);

        ICollection<Tweet> GetTweetsForFriend(long currentLoggedUserId, long friendId);
    }
}
