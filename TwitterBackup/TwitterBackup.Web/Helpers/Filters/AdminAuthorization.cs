namespace TwitterBackup.Web.Helpers.Filters
{
    using Newtonsoft.Json;
    using System.Linq;

    public class AdminAuthorization : TwitterAuthorization
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var isBaseAuth = base.IsAuthorized(actionContext);
            if (isBaseAuth)
            {
                var authDataHeader = actionContext.Request.Headers.GetValues(GlobalConstants.AuthorizationData).FirstOrDefault();
                if (!string.IsNullOrEmpty(authDataHeader))
                {
                    var authData = JsonConvert.DeserializeObject<AuthorizationData>(authDataHeader);
                    if (authData.IsAdmin)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}