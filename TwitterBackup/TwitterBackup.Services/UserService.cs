namespace TwitterBackup.Services
{
    using TwitterBackup.Data;
    using TwitterBackup.Models;
    using TwitterBackup.Services.Contracts;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using TwitterBackup.Services.Exceptions;

    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepo;

        public UserService(IRepository<User> userRepo)
        {
            this.userRepo = userRepo;
        }

        /// <summary>
        /// Store user in the TwitterBackup database
        /// </summary>
        /// <param name="user"></param>
        public User Save(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("Tweet is not valid");
            }

            var hasAlreadyExist = userRepo
                .All()
                .Any(userDTO => userDTO.UserTwitterId == user.UserTwitterId);

            if (hasAlreadyExist)
            {
                throw new UserException(UserExceptionType.IsAlreadySaved);
            }

            var userDataModel = userRepo.Add(user);

            return userDataModel;
        }

        public int GetUsersCount()
        {
            var usersCount = this.userRepo
                .All()
                .Count();

            return usersCount;
        }

        public IEnumerable<User> GetUsers()
        {
            var allUsers = this.userRepo
                .All()
                .ToArray();

            return allUsers;
        }
    }
}
