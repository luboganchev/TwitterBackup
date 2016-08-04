namespace TwitterBackup.Web.Models.Users
{
    using AutoMapper;
    using Tweetinvi.Models;
    using TwitterBackup.Web.Helpers.AutoMapper;

    public class UserShortInfoViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public bool Following { get; set; }
    }
}