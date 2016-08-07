namespace TwitterBackup.Web.Helpers.TwitterDriver
{
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Web;
    using Tweetinvi;
    using Tweetinvi.Models;

    public static class TwitterAuth
    {
        private const string ConsumerKey = "tU2Hn3puJlsS5h5er6j5WTRzf";
        private const string ConsumerSecret = "83M4WmrFbDon009CbAtECREU9jGWplIxdigFxmFmd5mbD97BLO";

        public static string GetAuthorizationData(HttpRequestMessage request)
        {
            var queryParams = request.RequestUri.ParseQueryString();
            var redirectUrl = queryParams.Get("redirectUrl");

            var appCreds = new ConsumerCredentials(ConsumerKey, ConsumerSecret);
            var authenticationContext = AuthFlow.InitAuthentication(appCreds, callbackURL: redirectUrl);

            var authorizationData = new AuthorizationData()
            {
                AuthorizationURL = authenticationContext.AuthorizationURL,
                AuthorizationKey = authenticationContext.Token.AuthorizationKey,
                AuthorizationSecret = authenticationContext.Token.AuthorizationSecret
            };

            return JsonConvert.SerializeObject(authorizationData);
        }

        public static IAuthenticatedUser SetAuthenticatedUser(HttpRequestBase request)
        {
            if (request.Headers.AllKeys.Any(key => key == GlobalConstants.AuthorizationData))
            {
                var authorizationDataJson = request.Headers.Get(GlobalConstants.AuthorizationData);

                if (authorizationDataJson != null)
                {

                    var authorizationData = JsonConvert.DeserializeObject<AuthorizationData>(authorizationDataJson);
                    var userCredentials = AuthFlow.CreateCredentialsFromVerifierCode(authorizationData.VerifierCode, authorizationData.AuthorizationKey, authorizationData.AuthorizationSecret, ConsumerKey, ConsumerSecret);
                    if (userCredentials != null)
                    {
                        Auth.SetCredentials(userCredentials);
                        // When a new thread is created, the default credentials will be the Application Credentials
                        Auth.ApplicationCredentials = userCredentials;

                        return User.GetAuthenticatedUser(Auth.ApplicationCredentials);
                    }
                }
            }

            return null;
        }
    }
}