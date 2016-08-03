
namespace TwitterBackup.Services
{
    using TwitterBackup.Models;
    using TwitterBackup.Data;

    public class StoreService
    {
        private readonly IRepository<User> userRepo;

        public StoreService(string connectionString, string databaseName)
        {
            userRepo = new MongoDbRepository<User>(connectionString, databaseName);
        }

        public void AddUser()
        {
            var testUser = new User
            {
                Name = "Pesho",
                StatusesCount =2
            };

            var currentUser = userRepo.Add(testUser);
        }
    }
}
