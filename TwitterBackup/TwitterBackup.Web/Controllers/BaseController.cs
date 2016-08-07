using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweetinvi.Models;
using TwitterBackup.Web.Helpers;

namespace TwitterBackup.Web.Controllers
{
    public class BaseController : ApiController
    {
        protected internal readonly string ConnectionString = ConfigHelper.ConnectionString;
        protected internal readonly string DatabaseName = ConfigHelper.DatabaseName;

        public static IAuthenticatedUser authUser;
    }
}
