namespace TwitterBackup.Web.Helpers.Filters
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Tweetinvi;
    using TwitterBackup.Web.Controllers;

    public class TwitterAuthorization : AuthorizeAttribute
    {
        private const int TWITTER_API_RATE_EXCEEDED_CODE = 88;
        private bool isTwitterApiRateExceeded = false;


        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                Auth.SetCredentials(Auth.ApplicationCredentials);
                var authenticatedUser = Tweetinvi.User.GetAuthenticatedUser(Auth.ApplicationCredentials);

                if (authenticatedUser == null)
                {
                    var latestException = ExceptionHandler.GetLastException();
                    if (latestException != null)
                    {
                        var exceptionCore = latestException.TwitterExceptionInfos.FirstOrDefault().Code;
                        switch (exceptionCore)
                        {
                            case TWITTER_API_RATE_EXCEEDED_CODE:
                                //Rate limit of twitter is exceeded https://dev.twitter.com/rest/public/rate-limiting
                                isTwitterApiRateExceeded = true;
                                break;
                            default:
                                break;
                        }
                    }

                    return false;
                }
                else
                {
                    TwitterController.authUser = authenticatedUser;

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (isTwitterApiRateExceeded)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}