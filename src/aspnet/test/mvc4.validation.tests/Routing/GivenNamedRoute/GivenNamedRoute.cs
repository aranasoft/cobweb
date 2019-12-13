using System;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenNamedRoute {
    [TestFixture]
    public abstract class GivenNamedRoute {
        public const string ActualRouteName = "routeName";
        public const string ExpectedUrl = "/Route/Name";

        [SetUp]
        public void ConfigureRoutes() {
            RouteTable.Routes.Clear();
            RouteTable.Routes.MapRoute(
                ActualRouteName,
                "Route/Name",
                new {controller = "Home", Action = "Index", id = ""});
        }

        [TearDown]
        public void ClearRoutes() {
            RouteTable.Routes.Clear();
        }
    }
}
