using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterBackup.Web.Models.Tweets
{
    public class TweetViewModel
    {
        public string CreatedByName { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Text { get; set; }

        public string FullText { get; set; }

        public int FavoriteCount { get; set; }

        public int RetweetCount { get; set; }

        public bool Retweeted { get; set; }

        public TweetViewModel Retweet { get; set; }
    }
}