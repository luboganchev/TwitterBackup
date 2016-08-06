
using AutoMapper;
using Tweetinvi.Models;
namespace TwitterBackup.Web.Models.Users
{
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
        }
    }
}