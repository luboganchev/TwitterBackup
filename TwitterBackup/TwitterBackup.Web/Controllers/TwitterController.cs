using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterBackup.Web.Helpers;

namespace TwitterBackup.Web.Controllers
{
    public class TwitterController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Authorize()
        {
            return this.Ok(TwitterAuth.GetAuthorizationData(this.Request));
        }

        [TwitterAuthorization]
        public IHttpActionResult GetFriends()
        {
            var friends = TwitterAuth.User.GetFriends();

            var friendsDTO = friends.Select(friend => new { friend.Name, friend.Description });

            return Content(HttpStatusCode.OK, JsonConvert.SerializeObject(friendsDTO));
        }

        public class TwitterAuthorization : AuthorizeAttribute
        {
            protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
            {
                return TwitterAuth.User != null;
            }
        }
    }
}
