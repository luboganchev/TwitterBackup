namespace TwitterBackup.Web.Helpers
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

        //private static IAuthenticationContext authenticationContext;
        //private static ITwitterCredentials creds = new TwitterCredentials(ConsumerKey, ConsumerSecret, "757545439130509312-JiRdKd3PncbtEDX3kThqt3sButW8syn", "8UsBdoDEcFyeCgN8SqlNXCxqtQCng1Yyqmg8jWQRlH85h");

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

        //public static void TrackRateLimits()
        //{
        //    TweetinviEvents.QueryBeforeExecute += (sender, args) =>
        //    {
        //        var queryRateLimits = RateLimit.GetQueryRateLimit(args.QueryURL);

        //        // Some methods are not RateLimited. Invoking such a method will result in the queryRateLimits to be null
        //        if (queryRateLimits != null)
        //        {

        //            if (queryRateLimits.Remaining > 0)
        //            {
        //                // We have enough resource to execute the query
        //                return;
        //            }

        //            args.Cancel = true;
        //        }
        //    };
        //}

        public static void SetAuthenticatedUser(HttpRequestBase request)
        {
            if (request.Headers.AllKeys.Any(key => key == "AuthorizationData"))
            {
                var authorizationDataJson = request.Headers.Get("AuthorizationData");

                if (authorizationDataJson != null)
                {

                    var authorizationData = JsonConvert.DeserializeObject<AuthorizationData>(authorizationDataJson);
                    var userCredentials = AuthFlow.CreateCredentialsFromVerifierCode(authorizationData.VerifierCode, authorizationData.AuthorizationKey, authorizationData.AuthorizationSecret, ConsumerKey, ConsumerSecret);
                    if (userCredentials != null)
                    {
                        Auth.SetCredentials(userCredentials);
                        // When a new thread is created, the default credentials will be the Application Credentials
                        Auth.ApplicationCredentials = userCredentials;
                    }
                }
            }
        }

        private class AuthorizationData
        {
            public string AuthorizationURL { get; set; }
            public string AuthorizationKey { get; set; }
            public string AuthorizationSecret { get; set; }

            public string VerifierCode { get; set; }
        }
    }
}