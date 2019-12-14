using System.Web.Http;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Http.Validation.Tests.Routing.GivenIgnoredRoute {
    [TestFixture]
    public abstract class GivenIgnoredRoute {
        protected HttpConfiguration HttpConfiguration;

        [SetUp]
        public void ConfigureRoutes() {
            HttpConfiguration = new HttpConfiguration();
            HttpConfiguration.Routes.IgnoreRoute("Ignored", "{controller}/{resource}.jpg/{*pathInfo}");
            HttpConfiguration.Routes.MapHttpRoute(name: "DefaultApi",
                                                  routeTemplate: "{controller}/{id}",
                                                  defaults: new {id = RouteParameter.Optional});
        }
    }
}
