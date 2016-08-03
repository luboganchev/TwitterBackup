using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterBackup.Web.Models.Users;

namespace TwitterBackup.Web.Models.Tweets
{
    public class TweetViewModel
    {
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