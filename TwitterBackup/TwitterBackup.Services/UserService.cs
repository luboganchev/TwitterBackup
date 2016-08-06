﻿namespace TwitterBackup.Services
{
    using TwitterBackup.Common.Constants;
    using TwitterBackup.Data;
    using TwitterBackup.Models;
    using TwitterBackup.Services.Contracts;
    using System.Linq;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepo;

        public UserService()
        {
            userRepo = new MongoDbRepository<User>(Database.ConnectionString, Database.DatabaseName);
        }

        /// <summary>
        /// Store user in the system
        /// </summary>
        /// <param name="user"></param>
        public User Save(User user)
        {
            var hasAlreadyExist = userRepo
                .All()
                .Any(userDTO => userDTO.UserTwitterId == user.UserTwitterId);

            if (!hasAlreadyExist) 
            {
                var userDataModel = userRepo.Add(user);

                return userDataModel;
            }

            return null;
        }

        public int UsersCount()
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
