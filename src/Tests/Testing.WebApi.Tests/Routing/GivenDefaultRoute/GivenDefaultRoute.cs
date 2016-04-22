using System.Web.Http;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenDefaultRoute
{
    [TestFixture]
    public abstract class GivenDefaultRoute {
        [SetUp]
        public void ConfigureRoutes() {
            GlobalConfiguration.Configuration.Routes.Clear();
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(name: "default",
                                                                  routeTemplate: "{controller}/{id}",
                                                                  defaults: new {id = RouteParameter.Optional});
        }

        [TearDown]
        public void ClearRoutes() {
            GlobalConfiguration.Configuration.Routes.Clear();
        }
    }
}
