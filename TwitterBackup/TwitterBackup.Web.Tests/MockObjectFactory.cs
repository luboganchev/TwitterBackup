namespace TwitterBackup.Web.Tests
{
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tweetinvi.Models;
    using TwitterBackup.Models;
    using TwitterBackup.Services.Contracts;
    using TwitterBackup.Services.Exceptions;
    using TwitterBackup.Web.Helpers.TwitterDriver;
    using TwitterBackup.Web.Models.Tweets;
    using TwitterBackup.Web.Tests.TestObjects;

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

        private static List<MockedITweet> mockedITweets = new List<MockedITweet>
        {
            new MockedITweet(null, MockObjectFactory.GetValidTweetId())
            {
                
            },
            new MockedITweet(null, MockObjectFactory.GetValidTweetIdThatIsStored())
            {
                
            },
            new MockedITweet(null, MockObjectFactory.GetValidTweetIdWhoIsAlreadyRetweetedId())
            {
                
            }
        };

        private static List<MockedIUser> mockedIUsers = new List<MockedIUser>
        {
            new MockedIUser(MockObjectFactory.GetValidUserId(), MockObjectFactory.GetValidUserName(), true)
            {
                
            },
            new MockedIUser(MockObjectFactory.GetInvalidUserId(), MockObjectFactory.GetValidNotFollowingUserName(), false)
            {
                
            },
            new MockedIUser(MockObjectFactory.GetInvalidUserId(), MockObjectFactory.GetInvalidUserName(), true)
            {
                
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
                    It.Is<string>(s => s == MockObjectFactory.GetValidUserName()),
                    It.Is<string>(s => s == MockObjectFactory.GetValidUserName())))
                .Returns(tweets);

            tweetService.Setup(ts => ts.GetTweetsForFriend(
                    It.Is<string>(s => s != MockObjectFactory.GetValidUserName()),
                    It.Is<string>(s => s != MockObjectFactory.GetValidUserName())))
                .Returns(new List<Tweet>());

            return tweetService.Object;
        }

        public static IRetweetService GetRetweetService()
        {
            var retweetService = new Mock<IRetweetService>();

            retweetService.Setup(rs => rs.Save(
                    It.Is<long>(id => id == MockObjectFactory.GetValidTweetId()),
                    It.IsAny<long>(),
                    It.IsAny<string>(),
                    It.IsAny<long>()))
                .Returns(retweets.FirstOrDefault());

            retweetService.Setup(rs => rs.Save(
                    It.Is<long>(id => id == MockObjectFactory.GetValidTweetIdWhoIsAlreadyRetweetedId()),
                    It.IsAny<long>(),
                    It.IsAny<string>(),
                    It.IsAny<long>()))
                .Throws(new RetweetException(RetweetExceptionType.IsAlreadySaved));

            retweetService.Setup(rs => rs.GetTotalRetweetsCount())
                .Returns(retweets.Count);

            retweetService.Setup(rs => rs.GetRetweets())
                .Returns(retweets);

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

        public static ITwitterApi GetTwitterApi()
        {
            var twitterApi = new Mock<ITwitterApi>();

            twitterApi.Setup(tw => tw.PublishRetweet(
                    It.Is<long>(id => id == MockObjectFactory.GetValidTweetId())))
                .Returns(mockedITweets.FirstOrDefault());

            twitterApi.Setup(tw => tw.PublishRetweet(
                    It.Is<long>(id => id == MockObjectFactory.GetValidTweetIdWhoIsAlreadyRetweetedId())))
                .Returns(mockedITweets.LastOrDefault());

            twitterApi.Setup(tw => tw.PublishRetweet(
                    It.Is<long>(id => id != MockObjectFactory.GetValidTweetId() && id != MockObjectFactory.GetValidTweetIdWhoIsAlreadyRetweetedId())))
                .Returns(MockObjectFactory.GetNullMockedITweet);

            twitterApi.Setup(tw => tw.GetUserFromScreenName(
                    It.Is<string>(name => name == MockObjectFactory.GetValidUserName())))
                .Returns(mockedIUsers.FirstOrDefault());

            twitterApi.Setup(tw => tw.GetUserFromScreenName(
                    It.Is<string>(name => name == MockObjectFactory.GetInvalidUserName())))
                .Returns(MockObjectFactory.GetNullMockedIUser());

            twitterApi.Setup(tw => tw.GetUserFromScreenName(
                    It.Is<string>(name => name == MockObjectFactory.GetValidNotFollowingUserName())))
                .Returns(mockedIUsers.Where(u => u.ScreenName == MockObjectFactory.GetValidNotFollowingUserName()).FirstOrDefault());

            twitterApi.Setup(tw => tw.SearchUsers(
                    It.Is<string>(k => k == MockObjectFactory.GetValidUserName()),
                    It.IsAny<int>()))
                .Returns(mockedIUsers);

            twitterApi.Setup(tw => tw.SearchUsers(
                    It.Is<string>(k => k != MockObjectFactory.GetValidUserName()),
                    It.IsAny<int>()))
                .Returns(new HashSet<MockedIUser>());

            twitterApi.Setup(tw => tw.GetUserTimeline(
                    It.Is<string>(u => u == MockObjectFactory.GetValidUserName())))
                .Returns(mockedITweets);

            return twitterApi.Object;
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

        public static ITweet GetNullMockedITweet()
        {
            MockedITweet tweet = null;
            return tweet;
        }

        public static long GetInvalidTweetId()
        {
            return 987654312;
        }

        public static long GetValidTweetId()
        {
            return 123456;
        }

        public static long GetValidTweetIdThatIsStored()
        {
            return 132456789;
        }

        public static long GetValidTweetIdWhoIsAlreadyRetweetedId()
        {
            return 99999999;
        }

        public static string GetValidUserName()
        {
            return "Ivan";
        }

        public static string GetInvalidUserName()
        {
            return "Stamat";
        }

        public static string GetValidNotFollowingUserName()
        {
            return "Ogi";
        }

        public static long GetInvalidUserId()
        {
            return 987654312;
        }

        public static long GetValidUserId()
        {
            return 123456;
        }

        public static IUser GetNullMockedIUser()
        {
            MockedIUser user = null;
            return user;
        }
    }
}
