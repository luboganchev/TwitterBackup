namespace TwitterBackup.Web.Models.Users
{
    using AutoMapper;
    using Tweetinvi.Models;
    using TwitterBackup.Models;

    public class UserAdminViewModel : UserShortInfoViewModel
    {
        public int DownloadedPostCount { get; set; }

        public int RetweetsCount { get; set; }

        public int FavoritesCount { get; set; }

        public override void CreateMappings(IConfiguration configuration)
        {
            base.CreateMappings(configuration);

            configuration.CreateMap<IUser, UserAdminViewModel>()
                .IncludeBase<IUser, UserShortInfoViewModel>()
                .ForMember(dest => dest.FavoritesCount, opt => opt.MapFrom(src => src.FavouritesCount));

            configuration.CreateMap<User, UserAdminViewModel>()
                .IncludeBase<User, UserShortInfoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserTwitterId))
                .ForMember(dest => dest.FavoritesCount, opt => opt.MapFrom(src => src.FriendsCount));
        }
    }
}