using System.Collections.Generic;
using TwitterBackup.Web.Models.Users;
using System.Linq;
namespace TwitterBackup.Web.Models.Admin
{
    public class AdminViewModel
    {
        public AdminViewModel()
        {
            this.Friends = new HashSet<UserShortInfoViewModel>();
        }

        public int DownloadedTweetsCount { get; set; }

        public int RetweetsCount { get; set; }

        public int FriendsCount
        {
            get
            {
                return this.Friends.Count();
            }
        }

        public IEnumerable<UserShortInfoViewModel> Friends { get; set; }
    }
}