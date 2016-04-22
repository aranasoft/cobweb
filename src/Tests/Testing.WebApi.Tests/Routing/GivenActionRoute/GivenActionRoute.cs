using System.Web.Http;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenActionRoute
{
    [TestFixture]
    public abstract class GivenActionRoute {
        [SetUp]
        public void ConfigureRoutes() {
            GlobalConfiguration.Configuration.Routes.Clear();
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(name: "default",
                                                                  routeTemplate: "{controller}/{action}/{id}",
                                                                  defaults: new {id = RouteParameter.Optional});
        }

        [TearDown]
        public void ClearRoutes() {
            GlobalConfiguration.Configuration.Routes.Clear();
        }
    }
}
