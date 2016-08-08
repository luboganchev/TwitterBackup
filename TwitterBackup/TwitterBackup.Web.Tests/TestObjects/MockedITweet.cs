namespace TwitterBackup.Web.Tests.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tweetinvi.Models;

    public class MockedITweet : ITweet
    {
        private IUser createdBy;
        private long id;

        public MockedITweet(IUser createdBy, long id)
        {
            this.createdBy = createdBy;
            this.id = id;
        }

        public int CalculateLength(bool willBePublishedWithMedia)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<long> Contributors
        {
            get { throw new NotImplementedException(); }
        }

        public int[] ContributorsIds
        {
            get { throw new NotImplementedException(); }
        }

        public ICoordinates Coordinates
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

        public DateTime CreatedAt
        {
            get { return DateTime.Now; }
        }

        public IUser CreatedBy
        {
            get { return this.createdBy; }
        }

        public ITweetIdentifier CurrentUserRetweetIdentifier
        {
            get { throw new NotImplementedException(); }
        }

        public bool Destroy()
        {
            throw new NotImplementedException();
        }

        public int[] DisplayTextRange
        {
            get { throw new NotImplementedException(); }
        }

        public Tweetinvi.Models.Entities.ITweetEntities Entities
        {
            get { throw new NotImplementedException(); }
        }

        public Tweetinvi.Models.DTO.IExtendedTweet ExtendedTweet
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

        public void Favorite()
        {
            throw new NotImplementedException();
        }

        public int FavoriteCount
        {
            get { return 5; }
        }

        public bool Favorited
        {
            get { throw new NotImplementedException(); }
        }

        public string FilterLevel
        {
            get { throw new NotImplementedException(); }
        }

        public string FullText { get; set; }

        public IOEmbedTweet GenerateOEmbedTweet()
        {
            throw new NotImplementedException();
        }

        public List<ITweet> GetRetweets()
        {
            throw new NotImplementedException();
        }

        public List<Tweetinvi.Models.Entities.IHashtagEntity> Hashtags
        {
            get { throw new NotImplementedException(); }
        }

        public string InReplyToScreenName
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

        public long? InReplyToStatusId
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

        public string InReplyToStatusIdStr
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

        public long? InReplyToUserId
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

        public string InReplyToUserIdStr
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

        public bool IsRetweet
        {
            get { return false; }
        }

        public bool IsTweetDestroyed
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsTweetPublished
        {
            get { throw new NotImplementedException(); }
        }

        public Language Language
        {
            get { throw new NotImplementedException(); }
        }

        public List<Tweetinvi.Models.Entities.IMediaEntity> Media
        {
            get { throw new NotImplementedException(); }
        }

        public IPlace Place
        {
            get { throw new NotImplementedException(); }
        }

        public bool PossiblySensitive
        {
            get { throw new NotImplementedException(); }
        }

        public string Prefix
        {
            get { throw new NotImplementedException(); }
        }

        public ITweet PublishRetweet()
        {
            throw new NotImplementedException();
        }

        public int PublishedTweetLength
        {
            get { throw new NotImplementedException(); }
        }

        public long? QuotedStatusId
        {
            get { throw new NotImplementedException(); }
        }

        public string QuotedStatusIdStr
        {
            get { throw new NotImplementedException(); }
        }

        public ITweet QuotedTweet
        {
            get { throw new NotImplementedException(); }
        }

        public int RetweetCount
        {
            get { return 10; }
        }

        public bool Retweeted
        {
            get { return false; }
        }

        public ITweet RetweetedTweet
        {
            get 
            {
                var createdBy = new MockedIUser(55664455, "Veri", false);
                var retweetedTweet = new MockedITweet(createdBy, 123456666);

                return retweetedTweet;
            }
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

        public Dictionary<string, object> Scopes
        {
            get { throw new NotImplementedException(); }
        }

        public string Source
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

        public string Suffix
        {
            get { throw new NotImplementedException(); }
        }

        public string Text { get; set; }

        public bool Truncated
        {
            get { throw new NotImplementedException(); }
        }

        public Tweetinvi.Models.DTO.ITweetDTO TweetDTO
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

        public DateTime TweetLocalCreationDate
        {
            get { throw new NotImplementedException(); }
        }

        public void UnFavorite()
        {
            throw new NotImplementedException();
        }

        public bool UnRetweet()
        {
            throw new NotImplementedException();
        }

        public string Url
        {
            get { throw new NotImplementedException(); }
        }

        public List<Tweetinvi.Models.Entities.IUrlEntity> Urls
        {
            get { throw new NotImplementedException(); }
        }

        public List<Tweetinvi.Models.Entities.IUserMentionEntity> UserMentions
        {
            get { throw new NotImplementedException(); }
        }

        public bool WithheldCopyright
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
            get { return this.id; }
        }

        public string IdStr
        {
            get { return this.id.ToString(); }
        }

        public Task<bool> DestroyAsync()
        {
            throw new NotImplementedException();
        }

        public Task FavoriteAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IOEmbedTweet> GenerateOEmbedTweetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ITweet>> GetRetweetsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ITweet> PublishRetweetAsync()
        {
            throw new NotImplementedException();
        }

        public Task UnFavoriteAsync()
        {
            throw new NotImplementedException();
        }

        public bool Equals(ITweet other)
        {
            throw new NotImplementedException();
        }
    }
}
