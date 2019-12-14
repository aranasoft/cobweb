using System.Web.Http;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Http.Validation.Tests.Routing.GivenAttributeRoute {
    [TestFixture]
    public abstract class GivenAttributeRoute {
        protected HttpConfiguration Configuration;

        [SetUp]
        public void ConfigureRoutes() {
            Configuration = new HttpConfiguration();
            Configuration.MapHttpAttributeRoutes();
            Configuration.EnsureInitialized();
        }
    }
}
