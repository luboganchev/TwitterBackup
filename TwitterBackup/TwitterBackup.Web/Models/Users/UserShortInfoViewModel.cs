namespace TwitterBackup.Web.Models.Users
{
    using AutoMapper;
    using Tweetinvi.Models;
    using TwitterBackup.Models;
    using TwitterBackup.Web.Helpers.AutoMapper;

    public class UserShortInfoViewModel : IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public bool Following { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<IUser, UserShortInfoViewModel>();

            configuration.CreateMap<UserShortInfoViewModel, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserTwitterId, opt => opt.MapFrom(src => src.Id));
        }
    }
}