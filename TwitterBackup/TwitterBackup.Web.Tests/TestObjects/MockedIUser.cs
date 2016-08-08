namespace TwitterBackup.Web.Tests.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tweetinvi.Models;

    public class MockedIUser : IUser
    {
        private long id;
        private string screenName;
        private bool following;

        public MockedIUser(long id, string screenName, bool following)
        {
            this.Id = id;
            this.screenName = screenName;
            this.following = following;
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
            get { return "Sample description"; }
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
            get { return 3; }
        }

        public bool Following
        {
            get { return this.following; }
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
            get { return 5; }
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
            throw new NotImplementedException();
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
            get { return this.screenName; }
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
            get { return "http://www.image.com/banner"; }
        }

        public string ProfileImageUrl
        {
            get { return "http://www.image.com/profileimage"; }
        }

        public string ProfileImageUrl400x400
        {
            get { return "http://www.image.com/profileimage"; }
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
            get { return "#ccc"; }
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
            get { return 20; }
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
