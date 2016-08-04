namespace TwitterBackup.Web.Helpers
{
    using System.Configuration;

    public static class ConfigHelper
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString;

        public static readonly string DatabaseName = ConfigurationManager.AppSettings["DatabaseName"].ToString();
    }
}