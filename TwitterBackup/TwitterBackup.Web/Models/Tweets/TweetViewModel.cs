namespace TwitterBackup.Web.Models.Tweets
{
    using System;
    using TwitterBackup.Models;
    using TwitterBackup.Web.Helpers.AutoMapper;
    using TwitterBackup.Web.Models.Users;

    public class TweetViewModel : IMapFrom<Tweet>
    {
        public string IdString { get; set; }

        public UserShortInfoViewModel Creator { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Text { get; set; }

        public string FullText { get; set; }

        public int FavoriteCount { get; set; }

        public int RetweetCount { get; set; }

        public bool RetweetedFromMe { get; set; }

        public bool IsRetweet { get; set; }

        public UserShortInfoViewModel RetweetFrom { get; set; }

        public TweetViewModel Retweet { get; set; }

        
    }
}