using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tweetinvi;
using TwitterBackup.Web.Helpers;
using TwitterBackup.Web.Helpers.AutoMapper;

namespace TwitterBackup.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        protected void Application_Start()
        {
            var assembliesForAutoMapper = new HashSet<Assembly>();
            assembliesForAutoMapper.Add(Assembly.GetExecutingAssembly());
            AutoMapperConfig.Execute(assembliesForAutoMapper);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
