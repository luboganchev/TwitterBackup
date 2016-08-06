using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBackup.Models;
using TwitterBackup.Services.Contracts;
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

        public static ITweetService GetTweetService()
        {
            var tweetService = new Mock<ITweetService>();

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

            tweetService.Setup(ts => ts.Save(
                    It.IsAny<Tweet>()))
                .Returns(tweets.FirstOrDefault());

            return tweetService.Object;
        }

        public static IRetweetService GetRetweetService()
        {
            var retweetService = new Mock<IRetweetService>();

            return retweetService.Object;
        }

        public static IUserService GetUserService()
        {
            var userService = new Mock<IUserService>();

            return userService.Object;
        }

        public static TweetViewModel GetInvalidModel()
        {
            return new TweetViewModel { FullText = "Sample" };
        }
    }
}
