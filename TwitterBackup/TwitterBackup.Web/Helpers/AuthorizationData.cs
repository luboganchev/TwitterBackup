namespace TwitterBackup.Web.Helpers
{
    public class AuthorizationData
    {
        public string AuthorizationURL { get; set; }

        public string AuthorizationKey { get; set; }

        public string AuthorizationSecret { get; set; }

        public bool IsAdmin { get; set; }

        public string VerifierCode { get; set; }
    }
}