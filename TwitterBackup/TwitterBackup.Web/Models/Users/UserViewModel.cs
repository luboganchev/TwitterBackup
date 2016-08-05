namespace TwitterBackup.Web.Models.Users
{
    using AutoMapper;
    using System.Collections.Generic;
    using Tweetinvi.Models;
    using TwitterBackup.Web.Helpers.AutoMapper;
    using TwitterBackup.Web.Models.Tweets;

    public class UserViewModel : IHaveCustomMappings
    {
        public UserViewModel()
        {
            this.Tweets = new HashSet<TweetViewModel>();
        }

        public long UserTwitterId { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Description { get; set; }

        public string ProfileBannerUrl { get; set; }

        public string ProfileImageUrl { get; set; }

        public string ProfileLinkColor { get; set; }

        public int FriendsCount { get; set; }

        public int FollowersCount { get; set; }

        public int StatusesCount { get; set; }

        public ICollection<TweetViewModel> Tweets { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<IUser, UserViewModel>()
                .ForMember(dest => dest.UserTwitterId, opt => opt.MapFrom(src => src.Id));
        }
    }
}