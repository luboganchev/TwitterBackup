using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using TwitterBackup.Web.Models.Users;
using TwitterBackup.Web.Models.Admin;
using Tweetinvi.Models;
using TwitterBackup.Services;
using TwitterBackup.Web.Helpers;

namespace TwitterBackup.Web.Controllers
{
    public class AdminController : BaseController
    {
        [HttpGet]
        public IHttpActionResult GetAdminData()
        {
            //CHECKED! a. Total number of users, downloaded posts and retweets.
            //b. Number of items in the favorite list for every user.
            //c. Number if downloaded posts for every user.
            //d. Number of retweets for every users.

            RetweetService retweetService = new RetweetService(ConfigHelper.ConnectionString, ConfigHelper.DatabaseName);
            TweetService tweetService = new TweetService(ConfigHelper.ConnectionString, ConfigHelper.DatabaseName);

            var friends = authUser
                .GetFriends()
                .Select(friend => new UserAdminViewModel
                {
                    Id = friend.Id,
                    Name = friend.Name,
                    Description = friend.Description,
                    ScreenName = friend.ScreenName,
                    ProfileImageUrl = friend.ProfileImageUrl,
                    FavoritesCount = friend.FavouritesCount
                })
                .ToArray();

            var friendsIds = friends
                .Select(friend => friend.Id)
                .ToArray();

            var allStoreTweetsForFriends = tweetService.GetTweetsForFriends(authUser.Id, friendsIds);
            var allRetweetsForFriends = retweetService.GetRetweetsForFriends(authUser.Id, friendsIds);
            foreach (var friend in friends)
            {
                friend.DownloadedPostCount = allStoreTweetsForFriends.Count(tweet => tweet.Owner.UserTwitterId == friend.Id);
                friend.RetweetsCount = allRetweetsForFriends.Count(retweet => retweet.TweetOwnerId == friend.Id);
            }

            var viewModel = new AdminViewModel
            {
                Friends = friends,
                DownloadedTweetsCount = tweetService.GetTweetsCount(authUser.Id),
                RetweetsCount = retweetService.GetRetweetsCount(authUser.Id)
            };

            return Ok(viewModel);
        }
    }
}
