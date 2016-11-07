using System.Web.Http;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenActionRoute {
    [TestFixture]
    public abstract class GivenActionRoute {
        protected HttpConfiguration HttpConfiguration;

        [SetUp]
        public void ConfigureRoutes() {
            HttpConfiguration = new HttpConfiguration();
            HttpConfiguration.Routes.MapHttpRoute(name: "default",
                                                  routeTemplate: "{controller}/{action}/{id}",
                                                  defaults: new {id = RouteParameter.Optional});
        }
    }
}
