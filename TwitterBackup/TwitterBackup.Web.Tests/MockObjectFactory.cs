using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBackup.Models;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Services.Exceptions;
using TwitterBackup.Web.Models.Tweets;

namespace TwitterBackup.Web.Tests
{
    public static class MockObjectFactory
    {
        private static List<Tweet> tweets = new List<Tweet>
        {
            new Tweet()
            {
                Id = Guid.NewGuid().ToString(),
                TweetTwitterId = 132456789,
                Text = "Sample text",
                CreatedAt = DateTime.Now,
                RetweetCount = 2,
                FavoriteCount = 4,
                FullText = "Sample text Sample text",
                CreatedById = 123465789,
                RetweetedFromMe = false
            }
        };

        private static List<Retweet> retweets = new List<Retweet>
        {
            new Retweet()
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now,
                ReweetTwitterId = 987654321,
                TweetOwnerId = 555566666,
                CreatedById = 123465789
            }
        };

        private static List<User> users = new List<User>
        {
            new User()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "Its me",
                Name = "Pesho",
                ScreenName = "peshoivanov",
                UserTwitterId = 123465789,
                StatusesCount = 50,
                FriendsCount = 100,
                FollowersCount = 23,
                ProfileBannerUrl = null,
                ProfileImageUrl = "http://pbs.twimg.com/profile_images/757546391426179072/bq3mi1XN_normal.jpg",
                ProfileLinkColor = "1B95E0"
            }
        };

        public static ITweetService GetTweetService()
        {
            var tweetService = new Mock<ITweetService>();

            tweetService.Setup(ts => ts.Save(
                    It.Is<Tweet>(tweet => tweet.TweetTwitterId == 123456789)))
                .Returns(tweets.FirstOrDefault());

            tweetService.Setup(ts => ts.Save(
                    It.Is<Tweet>(tweet => tweet.Text == "ExceptionItsAlreadySaved")))
                .Throws(new TweetException(TweetExceptionType.IsAlreadySaved));

            tweetService.Setup(ts => ts.GetTotalTweetsCount())
                .Returns(tweets.Count);

            tweetService.Setup(ts => ts.GetTweets())
                .Returns(tweets);

            tweetService.Setup(ts => ts.GetTweetsForFriend(
                    It.Is<long>(s => s == 123456),
                    It.Is<long>(s => s == 654321)))
                .Returns(tweets);

            tweetService.Setup(ts => ts.GetTweetsForFriend(
                    It.Is<long>(s => s != 123456),
                    It.Is<long>(s => s != 654321)))
                .Returns(new List<Tweet>());

            return tweetService.Object;
        }

        public static IRetweetService GetRetweetService()
        {
            var retweetService = new Mock<IRetweetService>();

            retweetService.Setup(rs => rs.Save(
                    It.IsAny<long>(),
                    It.IsAny<long>(),
                    It.IsAny<long>()))
                .Returns(retweets.FirstOrDefault());

            retweetService.Setup(rs => rs.GetTotalRetweetsCount())
                .Returns(retweets.Count);

            retweetService.Setup(rs => rs.GetRetweets())
                .Returns(retweets);

            retweetService.Setup(rs => rs.GetRetweetsCountForFriend(
                    It.Is<long>(c => c == 123465789),
                    It.Is<long>(f => f == 555566666)))
                .Returns(retweets.Count);

            retweetService.Setup(rs => rs.GetRetweetsCountForFriend(
                    It.Is<long>(c => c != 123465789),
                    It.Is<long>(f => f != 555566666)))
                .Returns(0);

            return retweetService.Object;
        }

        public static IUserService GetUserService()
        {
            var userService = new Mock<IUserService>();

            userService.Setup(us => us.Save(
                    It.IsAny<User>()))
                .Returns(users.FirstOrDefault());

            userService.Setup(us => us.GetUsers())
                .Returns(users);

            userService.Setup(us => us.GetUsersCount())
                .Returns(users.Count);

            return userService.Object;
        }

        public static TweetViewModel GetInvalidTweetViewModel()
        {
            return new TweetViewModel { FullText = "Sample" };
        }

        public static TweetViewModel GetValidTweetViewModel()
        {
            return new TweetViewModel
            {
                IdString = "123456789",
                CreatedAt = DateTime.Now,
                Text = "Tweet",
                FullText = "Tweet full text",
                FavoriteCount = 5,
                RetweetCount = 1,
                RetweetedFromMe = true
            };
        }

        public static TweetViewModel GetValidTweetViewModelWhoThrowsException()
        {
            return new TweetViewModel
            {
                IdString = "123456789",
                CreatedAt = DateTime.Now,
                Text = "ExceptionItsAlreadySaved",
                FullText = "Tweet full text",
                FavoriteCount = 5,
                RetweetCount = 1,
                RetweetedFromMe = true
            };
        }
    }
}
