namespace TwitterBackup.Web.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Reflection;
    using TwitterBackup.Web.Helpers.AutoMapper;

    [TestClass]
    public class TestInit
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var assembliesForAutoMapper = new HashSet<Assembly>();
            assembliesForAutoMapper.Add(typeof(Startup).Assembly);
            AutoMapperConfig.Execute(assembliesForAutoMapper);
        }
    }
}
