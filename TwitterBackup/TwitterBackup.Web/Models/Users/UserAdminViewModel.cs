
namespace TwitterBackup.Web.Models.Users
{
    public class UserAdminViewModel : UserShortInfoViewModel
    {
        public int DownloadedPostCount { get; set; }

        public int RetweetsCount { get; set; }

        public int FavoritesCount { get; set; }
    }
}