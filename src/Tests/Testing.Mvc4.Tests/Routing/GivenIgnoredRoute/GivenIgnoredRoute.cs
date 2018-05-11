using System;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenIgnoredRoute {
    [TestFixture]
    public abstract class GivenIgnoredRoute {
        [SetUp]
        public void ConfigureRoutes() {
            RouteTable.Routes.Clear();
            RouteTable.Routes.IgnoreRoute("{resource}.jpg/{*pathInfo}");
            RouteTable.Routes.MapRoute(
                "default",
                "{controller}/{action}/{id}",
                new {controller = "Home", Action = "Index", id = ""});
        }

        [TearDown]
        public void ClearRoutes() {
            RouteTable.Routes.Clear();
        }
    }
}
