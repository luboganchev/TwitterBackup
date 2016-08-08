namespace TwitterBackup.Web.Tests.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tweetinvi.Models;

    public class MockedIAuthenticatedUser : IAuthenticatedUser
    {
        private long id;
        private string screenName;

        public MockedIAuthenticatedUser(long id, string screenName)
        {
            this.Id = id;
            this.screenName = screenName;
        }

        public IAccountSettings AccountSettings
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool BlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public bool BlockUser(long userId)
        {
            throw new NotImplementedException();
        }

        public bool BlockUser(IUserIdentifier userIdentifier)
        {
            throw new NotImplementedException();
        }

        public ITwitterCredentials Credentials
        {
            get { throw new NotImplementedException(); }
        }

        public string Email
        {
            get { throw new NotImplementedException(); }
        }

        public void ExecuteAuthenticatedUserOperation(Action operation)
        {
            throw new NotImplementedException();
        }

        public T ExecuteAuthenticatedUserOperation<T>(Func<T> operation)
        {
            throw new NotImplementedException();
        }

        public bool FollowUser(string screenName)
        {
            return true;
        }

        public bool FollowUser(long userId)
        {
            throw new NotImplementedException();
        }

        public bool FollowUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public IAccountSettings GetAccountSettings()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<long> GetBlockedUserIds()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> GetBlockedUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITweet> GetHomeTimeline(Tweetinvi.Parameters.IHomeTimelineParameters timelineRequestParameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITweet> GetHomeTimeline(int maximumNumberOfTweets = 40)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMessage> GetLatestMessagesReceived(int maximumNumberOfMessagesToRetrieve = 40)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMessage> GetLatestMessagesSent(int maximumNumberOfMessagesToRetrieve = 40)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMention> GetMentionsTimeline(int maximumNumberOfMentions = 40)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<long> GetMutedUserIds(int maxUserIdsToRetrieve = 2147483647)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> GetMutedUsers(int maxUsersToRetrieve = 250)
        {
            throw new NotImplementedException();
        }

        public IRelationshipDetails GetRelationshipWith(string screenName)
        {
            throw new NotImplementedException();
        }

        public IRelationshipDetails GetRelationshipWith(long userId)
        {
            throw new NotImplementedException();
        }

        public IRelationshipDetails GetRelationshipWith(IUserIdentifier userIdentifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISavedSearch> GetSavedSearches()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> GetUsersRequestingFriendship(int maximumUserIdsToRetrieve = 5000)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> GetUsersYouRequestedToFollow(int maximumUserIdsToRetrieve = 5000)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMessage> LatestDirectMessagesReceived
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<IMessage> LatestDirectMessagesSent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<ITweet> LatestHomeTimeline
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<IMention> LatestMentionsTimeline
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool MuteUser(string screenName)
        {
            throw new NotImplementedException();
        }

        public bool MuteUser(long userId)
        {
            throw new NotImplementedException();
        }

        public bool MuteUser(IUserIdentifier userIdentifier)
        {
            throw new NotImplementedException();
        }

        public IMessage PublishMessage(Tweetinvi.Parameters.IPublishMessageParameters publishMessageParameters)
        {
            throw new NotImplementedException();
        }

        public ITweet PublishTweet(string text, Tweetinvi.Parameters.IPublishTweetOptionalParameters parameters = null)
        {
            throw new NotImplementedException();
        }

        public bool ReportUserForSpam(string userName)
        {
            throw new NotImplementedException();
        }

        public bool ReportUserForSpam(long userId)
        {
            throw new NotImplementedException();
        }

        public bool ReportUserForSpam(IUserIdentifier userIdentifier)
        {
            throw new NotImplementedException();
        }

        public bool ReportUserForSpam(IUser user)
        {
            throw new NotImplementedException();
        }

        public void SetCredentials(ITwitterCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public bool SubsribeToList(string slug, IUserIdentifier owner)
        {
            throw new NotImplementedException();
        }

        public bool SubsribeToList(string slug, string ownerScreenName)
        {
            throw new NotImplementedException();
        }

        public bool SubsribeToList(string slug, long ownerId)
        {
            throw new NotImplementedException();
        }

        public bool SubsribeToList(long listId)
        {
            throw new NotImplementedException();
        }

        public bool SubsribeToList(ITwitterListIdentifier list)
        {
            throw new NotImplementedException();
        }

        public bool UnBlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public bool UnBlockUser(long userId)
        {
            throw new NotImplementedException();
        }

        public bool UnBlockUser(IUserIdentifier userIdentifier)
        {
            throw new NotImplementedException();
        }

        public bool UnFollowUser(string screenName)
        {
            if (screenName == MockObjectFactory.GetValidUserName())
            {
                return true;
            }

            return false;
        }

        public bool UnFollowUser(long userId)
        {
            throw new NotImplementedException();
        }

        public bool UnFollowUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public bool UnMuteUser(string screenName)
        {
            throw new NotImplementedException();
        }

        public bool UnMuteUser(long userId)
        {
            throw new NotImplementedException();
        }

        public bool UnMuteUser(IUserIdentifier userIdentifier)
        {
            throw new NotImplementedException();
        }

        public bool UnSubscribeFromList(string slug, IUserIdentifier owner)
        {
            throw new NotImplementedException();
        }

        public bool UnSubscribeFromList(string slug, string ownerScreenName)
        {
            throw new NotImplementedException();
        }

        public bool UnSubscribeFromList(string slug, long ownerId)
        {
            throw new NotImplementedException();
        }

        public bool UnSubscribeFromList(long listId)
        {
            throw new NotImplementedException();
        }

        public bool UnSubscribeFromList(ITwitterListIdentifier list)
        {
            throw new NotImplementedException();
        }

        public IAccountSettings UpdateAccountSettings(Tweetinvi.Parameters.IAccountSettingsRequestParameters accountSettingsRequestParameters)
        {
            throw new NotImplementedException();
        }

        public IAccountSettings UpdateAccountSettings(IEnumerable<Language> languages = null, string timeZone = null, long? trendLocationWoeid = null, bool? sleepTimeEnabled = null, int? startSleepTime = null, int? endSleepTime = null)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRelationshipAuthorizationsWith(string screenName, bool retweetsEnabled, bool deviceNotificationsEnabled)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRelationshipAuthorizationsWith(long userId, bool retweetsEnabled, bool deviceNotificationsEnabled)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRelationshipAuthorizationsWith(IUserIdentifier userIdentifier, bool retweetsEnabled, bool deviceNotificationsEnabled)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FollowUserAsync(string screenName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FollowUserAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FollowUserAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IAccountSettings> GetAccountSettingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUser>> GetBlockedUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<long>> GetBlockedUsersIdsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITweet>> GetHomeTimelineAsync(Tweetinvi.Parameters.IHomeTimelineParameters timelineRequestParameters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITweet>> GetHomeTimelineAsync(int count = 40)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMessage>> GetLatestMessagesReceivedAsync(int count = 40)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMessage>> GetLatestMessagesSentAsync(int maximumMessages = 40)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IMention>> GetMentionsTimelineAsync(int count = 40)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<long>> GetMutedUserIdsAsync(int maxUserIds = 2147483647)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUser>> GetMutedUsersAsync(int maxUserIds = 250)
        {
            throw new NotImplementedException();
        }

        public Task<IRelationshipDetails> GetRelationshipWithAsync(string screenName)
        {
            throw new NotImplementedException();
        }

        public Task<IRelationshipDetails> GetRelationshipWithAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<IRelationshipDetails> GetRelationshipWithAsync(IUserIdentifier userIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ISavedSearch>> GetSavedSearchesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUser>> GetUsersRequestingFriendshipAsync(int maximumUserIdsToRetrieve = 75000)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUser>> GetUsersYouRequestedToFollowAsync(int maximumUsersToRetrieve = 75000)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MuteUserAsync(string screenName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MuteUserAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MuteUserAsync(IUserIdentifier userIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<IMessage> PublishMessageAsync(Tweetinvi.Parameters.IPublishMessageParameters publishMessageParameters)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToListAsync(string slug, IUserIdentifier owner)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToListAsync(string slug, string ownerScreenName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToListAsync(string slug, long ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToListAsync(long listId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToListAsync(ITwitterListIdentifier list)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnFollowUserAsync(string screenName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnFollowUserAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnFollowUserAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnMuteUserAsync(string screenName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnMuteUserAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnMuteUserAsync(IUserIdentifier userIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeFromListAsync(string slug, IUserIdentifier owner)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeFromListAsync(string slug, string ownerScreenName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeFromListAsync(string slug, long ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeFromListAsync(long listId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeFromListAsync(ITwitterListIdentifier list)
        {
            throw new NotImplementedException();
        }

        public Task<IAccountSettings> UpdateAccountSettingsAsync(Tweetinvi.Parameters.IAccountSettingsRequestParameters accountSettingsRequestParameters)
        {
            throw new NotImplementedException();
        }

        public Task<IAccountSettings> UpdateAccountSettingsAsync(IEnumerable<Language> languages = null, string timeZone = null, long? trendLocationWoeid = null, bool? sleepTimeEnabled = null, int? startSleepTime = null, int? endSleepTime = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRelationshipAuthorizationsWithAsync(string screenName, bool retweetsEnabled, bool deviceNotificationsEnabled)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRelationshipAuthorizationsWithAsync(long userId, bool retweetsEnabled, bool deviceNotificationsEnabled)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRelationshipAuthorizationsWithAsync(IUserIdentifier user, bool retweetsEnabled, bool deviceNotificationsEnabled)
        {
            throw new NotImplementedException();
        }

        public bool BlockUser()
        {
            throw new NotImplementedException();
        }

        public List<IUser> Contributees
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<IUser> Contributors
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool ContributorsEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime CreatedAt
        {
            get { throw new NotImplementedException(); }
        }

        public bool DefaultProfile
        {
            get { throw new NotImplementedException(); }
        }

        public bool DefaultProfileImage
        {
            get { throw new NotImplementedException(); }
        }

        public string Description
        {
            get { throw new NotImplementedException(); }
        }

        public Tweetinvi.Models.Entities.IUserEntities Entities
        {
            get { throw new NotImplementedException(); }
        }

        public int FavouritesCount
        {
            get { throw new NotImplementedException(); }
        }

        public bool FollowRequestSent
        {
            get { throw new NotImplementedException(); }
        }

        public List<long> FollowerIds
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<IUser> Followers
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int FollowersCount
        {
            get { throw new NotImplementedException(); }
        }

        public bool Following
        {
            get { throw new NotImplementedException(); }
        }

        public List<long> FriendIds
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<IUser> Friends
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int FriendsCount
        {
            get { throw new NotImplementedException(); }
        }

        public List<ITweet> FriendsRetweets
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool GeoEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IUser> GetContributees(bool createContributeeList = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> GetContributors(bool createContributorList = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITweet> GetFavorites(Tweetinvi.Parameters.IGetUserFavoritesParameters parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITweet> GetFavorites(int maximumNumberOfTweets = 40)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<long> GetFollowerIds(int maxFriendsToRetrieve = 5000)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> GetFollowers(int maxFriendsToRetrieve = 250)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<long> GetFriendIds(int maxFriendsToRetrieve = 5000)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> GetFriends(int maxFriendsToRetrieve = 250)
        {
            if (this.screenName == MockObjectFactory.GetValidUserName())
            {
                var result = new HashSet<MockedIUser>()
                {
                    new MockedIUser(MockObjectFactory.GetValidUserId(), MockObjectFactory.GetValidUserName(), true)
                    {
                        
                    },
                    new MockedIUser(MockObjectFactory.GetInvalidUserId(), MockObjectFactory.GetValidNotFollowingUserName(), true)
                    {
                
                    },
                    new MockedIUser(MockObjectFactory.GetInvalidUserId(), MockObjectFactory.GetInvalidUserName(), true)
                    {
                
                    }
                };

                return result;
            }

            return new HashSet<MockedIUser>();
        }

        public IEnumerable<ITwitterList> GetOwnedLists(int maximumNumberOfListsToRetrieve)
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream GetProfileImageStream(ImageSize imageSize = ImageSize.normal)
        {
            throw new NotImplementedException();
        }

        public IRelationshipDetails GetRelationshipWith(IUser user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITwitterList> GetSubscribedLists(int maximumNumberOfListsToRetrieve = 1000)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITweet> GetUserTimeline(Tweetinvi.Parameters.IUserTimelineParameters timelineParameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITweet> GetUserTimeline(int maximumNumberOfTweets = 40)
        {
            throw new NotImplementedException();
        }

        public bool IsTranslator
        {
            get { throw new NotImplementedException(); }
        }

        public Language Language
        {
            get { throw new NotImplementedException(); }
        }

        public int ListedCount
        {
            get { throw new NotImplementedException(); }
        }

        public string Location
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public bool Notifications
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileBackgroundColor
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileBackgroundImageUrl
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileBackgroundImageUrlHttps
        {
            get { throw new NotImplementedException(); }
        }

        public bool ProfileBackgroundTile
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileBannerURL
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileImageUrl
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileImageUrl400x400
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileImageUrlFullSize
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileImageUrlHttps
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileLinkColor
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileSidebarBorderColor
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileSidebarFillColor
        {
            get { throw new NotImplementedException(); }
        }

        public string ProfileTextColor
        {
            get { throw new NotImplementedException(); }
        }

        public bool ProfileUseBackgroundImage
        {
            get { throw new NotImplementedException(); }
        }

        public bool Protected
        {
            get { throw new NotImplementedException(); }
        }

        public bool ReportUserForSpam()
        {
            throw new NotImplementedException();
        }

        public List<ITweet> Retweets
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Tweetinvi.Models.DTO.ITweetDTO Status
        {
            get { throw new NotImplementedException(); }
        }

        public int StatusesCount
        {
            get { throw new NotImplementedException(); }
        }

        public string TimeZone
        {
            get { throw new NotImplementedException(); }
        }

        public List<ITweet> Timeline
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<ITweet> TweetsRetweetedByFollowers
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool UnBlockUser()
        {
            throw new NotImplementedException();
        }

        public string Url
        {
            get { throw new NotImplementedException(); }
        }

        public Tweetinvi.Models.DTO.IUserDTO UserDTO
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IUserIdentifier UserIdentifier
        {
            get { throw new NotImplementedException(); }
        }

        public int? UtcOffset
        {
            get { throw new NotImplementedException(); }
        }

        public bool Verified
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<string> WithheldInCountries
        {
            get { throw new NotImplementedException(); }
        }

        public string WithheldScope
        {
            get { throw new NotImplementedException(); }
        }

        public long Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string IdStr
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ScreenName
        {
            get
            {
                return this.screenName;
            }
            set
            {
                this.screenName = value;
            }
        }

        public Task<bool> BlockAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUser>> GetContributeesAsync(bool createContributeeList = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUser>> GetContributorsAsync(bool createContributorList = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITweet>> GetFavoritesAsync(int maximumTweets = 40)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<long>> GetFollowerIdsAsync(int maxFriendsToRetrieve = 5000)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUser>> GetFollowersAsync(int maxFriendsToRetrieve = 250)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<long>> GetFriendIdsAsync(int maxFriendsToRetrieve = 5000)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IUser>> GetFriendsAsync(int maxFriendsToRetrieve = 250)
        {
            throw new NotImplementedException();
        }

        public Task<System.IO.Stream> GetProfileImageStreamAsync(ImageSize imageSize = ImageSize.normal)
        {
            throw new NotImplementedException();
        }

        public Task<IRelationshipDetails> GetRelationshipWithAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITweet>> GetUserTimelineAsync(Tweetinvi.Parameters.IUserTimelineParameters timelineParameters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITweet>> GetUserTimelineAsync(int maximumTweet = 40)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnBlockAsync()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IUser other)
        {
            throw new NotImplementedException();
        }
    }
}
