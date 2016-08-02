﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterBackup.Web.Models.Tweets;

namespace TwitterBackup.Web.Models.Users
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.Tweets = new HashSet<TweetViewModel>();
        }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Description { get; set; }

        public string ProfileBannerUrl { get; set; }

        public string ProfileImageUrl { get; set; }

        public string ProfileLinkColor { get; set; }

        public int FriendsCount { get; set; }

        public int FollowersCount { get; set; }

        public int StatusesCount { get; set; }

        public ICollection<TweetViewModel> Tweets { get; set; }
    }
}