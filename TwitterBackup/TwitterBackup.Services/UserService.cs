namespace TwitterBackup.Services
{
    using TwitterBackup.Data;
    using TwitterBackup.Models;
    using TwitterBackup.Services.Contracts;
    using System.Linq;
    using System.Collections.Generic;

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
