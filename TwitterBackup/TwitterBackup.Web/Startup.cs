using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TwitterBackup.Web.Startup))]

namespace TwitterBackup.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}
