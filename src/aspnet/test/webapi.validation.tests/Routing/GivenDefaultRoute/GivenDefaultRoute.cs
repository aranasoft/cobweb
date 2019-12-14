using System.Web.Http;
using NUnit.Framework;

namespace Aranasoft.Cobweb.Http.Validation.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public abstract class GivenDefaultRoute {
        protected HttpConfiguration HttpConfiguration;

        [SetUp]
        public void ConfigureRoutes() {
            HttpConfiguration = new HttpConfiguration();
            HttpConfiguration.Routes.MapHttpRoute(name: "default",
                                                  routeTemplate: "{controller}/{id}",
                                                  defaults: new {id = RouteParameter.Optional});
        }
    }
}
