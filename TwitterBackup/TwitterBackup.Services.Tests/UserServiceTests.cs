namespace TwitterBackup.Services.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TwitterBackup.Services.Tests.TestObjects;
    using TwitterBackup.Models;
    using TwitterBackup.Services.Exceptions;

    [TestClass]
    public class UserServiceTests
    {
        private FakeMemoryRepository<User> userRepo;
        private UserService userService;

        [TestInitialize]
        public void Init()
        {
            this.userRepo = new FakeMemoryRepository<User>();
            this.userService = new UserService(userRepo);
        }

        #region Save

        [TestMethod]
        public void Populate_ShouldDatabaseBeCorrect()
        {
            var userTwitterId = 10023012321;
            var user = this.GetUserObject(userTwitterId, "peshoivanov");
            var userId = user.Id;
            this.userService.Save(user);
            var storedUser = this.userRepo.GetById(userId);

            Assert.IsNotNull(storedUser);
            Assert.AreEqual(userTwitterId, storedUser.UserTwitterId);
            Assert.AreEqual(user.Name, storedUser.Name);
            Assert.AreEqual(user.ScreenName, storedUser.ScreenName);
            Assert.AreEqual(user.StatusesCount, storedUser.StatusesCount);
            Assert.AreEqual(user.Description, storedUser.Description);
            Assert.AreEqual(user.FollowersCount, storedUser.FollowersCount);
            Assert.AreEqual(user.FriendsCount, storedUser.FriendsCount);
            Assert.AreEqual(user.ProfileImageUrl, storedUser.ProfileImageUrl);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void DoubleAddingSomeUser_ThrowException()
        {
            var userTwitterId = 10023012321;
            var user = this.GetUserObject(userTwitterId, "peshoivanov");
            this.userService.Save(user);
            this.userService.Save(user);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddNullUser_ThrowExcetpion()
        {
            this.userService.Save(null);
        }

        #endregion Save

        #region GetUsersCount

        [TestMethod]
        public void AddSomeUsers_GetCorrectCount()
        {
            var userOne = this.GetUserObject(10023012321, "peshoivanov1");
            var userTwo = this.GetUserObject(10023012322, "peshoivanov2");
            var userThree = this.GetUserObject(10023012323, "peshoivanov3");
            var userFour = this.GetUserObject(10023012324, "peshoivanov4");
            this.userService.Save(userOne);
            this.userService.Save(userTwo);
            this.userService.Save(userThree);
            this.userService.Save(userFour);

            var usersCount = this.userService.GetUsersCount();
            Assert.AreEqual(4, usersCount);
        }

        [TestMethod]
        public void DontAddUsers_GetCorrectCount()
        {
            var usersCount = this.userService.GetUsersCount();
            Assert.AreEqual(0, usersCount);
        }

        #endregion GetUsersCount

        #region GetUsers

        [TestMethod]
        public void AddSomeUsers_GetCorrectCollection()
        {
            var userOne = this.GetUserObject(10023012321, "peshoivanov1");
            var userTwo = this.GetUserObject(10023012322, "peshoivanov2");
            var userThree = this.GetUserObject(10023012323, "peshoivanov3");
            var userFour = this.GetUserObject(10023012324, "peshoivanov4");
            this.userService.Save(userOne);
            this.userService.Save(userTwo);
            this.userService.Save(userThree);
            this.userService.Save(userFour);

            var usersCollection = this.userService.GetUsers();
            Assert.IsNotNull(usersCollection);
            Assert.AreEqual(4, usersCollection.Count());

            var firstElement = usersCollection.FirstOrDefault();
            Assert.IsNotNull(firstElement);
            Assert.AreEqual(10023012321, firstElement.UserTwitterId);

            var lastElement = usersCollection.LastOrDefault();
            Assert.IsNotNull(lastElement);
            Assert.AreEqual(10023012324, lastElement.UserTwitterId);
        }

        [TestMethod]
        public void DontAddUsers_GetCorrectCollection()
        {
            var usersCollection = this.userService.GetUsers();
            Assert.IsNotNull(usersCollection);
            Assert.AreEqual(0, usersCollection.Count());

            var firstElement = usersCollection.FirstOrDefault();
            Assert.IsNull(firstElement);
        }

        #endregion GetUsers

        private User GetUserObject(long userTwitterId, string screenName)
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "Its me",
                Name = "Pesho",
                ScreenName = screenName,
                UserTwitterId = userTwitterId,
                StatusesCount = 50,
                FriendsCount = 100,
                FollowersCount = 23,
                ProfileBannerUrl = null,
                ProfileImageUrl = "http://pbs.twimg.com/profile_images/757546391426179072/bq3mi1XN_normal.jpg",
                ProfileLinkColor = "1B95E0"
            };

            return user;
        }
    }
}
