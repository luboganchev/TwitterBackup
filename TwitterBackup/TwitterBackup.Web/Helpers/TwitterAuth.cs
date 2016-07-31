using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Tweetinvi;
using Tweetinvi.Credentials.Models;
using Tweetinvi.Models;

namespace TwitterBackup.Web.Helpers
{
    public static class TwitterAuth
    {
        [ThreadStatic]
        public static IAuthenticatedUser User;

        private const string ConsumerKey = "tU2Hn3puJlsS5h5er6j5WTRzf";
        private const string ConsumerSecret = "83M4WmrFbDon009CbAtECREU9jGWplIxdigFxmFmd5mbD97BLO";

        private static IAuthenticationContext authenticationContext;


        public static string GetAuthorizationData(HttpRequestMessage request)
        {
            var queryParams = request.RequestUri.ParseQueryString();
            var redirectUrl = queryParams.Get("redirectUrl");

            var appCreds = new ConsumerCredentials(ConsumerKey, ConsumerSecret);
            authenticationContext = AuthFlow.InitAuthentication(appCreds, callbackURL: redirectUrl);

            var authorizationData = new AuthorizationData()
            {
                AuthorizationURL = authenticationContext.AuthorizationURL,
                AuthorizationKey = authenticationContext.Token.AuthorizationKey,
                AuthorizationSecret = authenticationContext.Token.AuthorizationSecret
            };

            return JsonConvert.SerializeObject(authorizationData);
        }

        public static void SetAuthenticatedUser(HttpRequest request)
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
                        User = Tweetinvi.User.GetAuthenticatedUser(userCredentials);
                        Auth.SetCredentials(userCredentials);
                    }
                }
            }
        }

        public static void RemoveAuthenticatedUser()
        {
            TwitterAuth.User = null;
        }

        //public static void SetAuthorizationCookies(HttpRequestMessage request, HttpResponseMessage response)
        //{
        //    var queryParams = request.RequestUri.ParseQueryString();
        //    var cookieHost = queryParams.Get("cookieHostUrl");
        //    var verifierCode = queryParams.Get("oauth_verifier");

        //    var authorizationDataJson = GetAuthorizationCookie(request);
        //    if (authorizationDataJson != null)
        //    {
        //        var authorizationData = JsonConvert.DeserializeObject<AuthorizationData>(authorizationDataJson);
        //        authorizationData.VerifierCode = verifierCode;
        //        AddCookie(request, response, authorizationData, cookieHost);
        //    }
        //}

        //public static void SetAuthenticatedUser(HttpRequest request)
        //{
        //    // Get some information back from the URL
        //    var verifierCode = request.Url.ParseQueryString().Get("oauth_verifier");

        //    // Create the user credentials
        //    var userCreds = AuthFlow.CreateCredentialsFromVerifierCode(verifierCode, authenticationContext);

        //    // Do whatever you want with the user now!
        //    //ViewBag.User = Tweetinvi.User.GetAuthenticatedUser(userCreds);
        //    //return View();
        //}

        //private static string GetAuthorizationCookie(HttpRequestMessage request)
        //{
        //    var authorizationDataCookie = request.Headers.GetCookies("twitter-authorization").FirstOrDefault();
        //    if (authorizationDataCookie != null)
        //    {
        //        var authorizationDataJson = authorizationDataCookie["twitter-authorization"].Value;

        //        return authorizationDataJson;
        //    }

        //    return null;
        //}

        //private static void AddCookie(HttpRequestMessage request, HttpResponseMessage response, AuthorizationData authorizationData, string cookieHost)
        //{
        //    var authorizationDataJson = JsonConvert.SerializeObject(authorizationData);

        //    var cookie = new CookieHeaderValue("twitter-authorization", authorizationDataJson);
        //    cookie.Expires = DateTimeOffset.Now.AddDays(30);
        //    // Fix cookie domain after deploy
        //    cookie.Domain = "127.0.0.1";
        //    cookie.Path = "/";

        //    response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
        //}

        private class AuthorizationData
        {
            public string AuthorizationURL { get; set; }
            public string AuthorizationKey { get; set; }
            public string AuthorizationSecret { get; set; }

            public string VerifierCode { get; set; }
        }
    }
}