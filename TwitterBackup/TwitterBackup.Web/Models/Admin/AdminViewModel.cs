namespace TwitterBackup.Web.Models.Admin
{
    using System.Collections.Generic;
    using TwitterBackup.Web.Models.Users;
    using System.Linq;

    public class AdminViewModel
    {
        public AdminViewModel()
        {
            this.Users = new HashSet<UserShortInfoViewModel>();
        }

        public int DownloadedTweetsCount { get; set; }

        public int RetweetsCount { get; set; }

        public int UsersCount
        {
            get
            {
                return this.Users.Count();
            }
        }

        public IEnumerable<UserShortInfoViewModel> Users { get; set; }
    }
}