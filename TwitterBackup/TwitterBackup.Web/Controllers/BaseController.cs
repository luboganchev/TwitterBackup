using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweetinvi.Models;

namespace TwitterBackup.Web.Controllers
{
    public class BaseController : ApiController
    {
        internal static IAuthenticatedUser authUser;
    }
}
