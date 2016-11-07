using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cobweb.Testing.WebApi.Assertions;
using Cobweb.Testing.WebApi.Extensions;
using Cobweb.Testing.WebApi.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.WebApi.Tests.Routing.GivenActionRoute {
    [TestFixture]
    public class WithActionDataTypeControllerUrl : GivenActionRoute {
        [TestCase("~/ActionDataTypeInput/WithNothing/27/1")]
        [TestCase("~/ActionDataTypeInput/WithNothing/27/1/")]
        [Ignore("Fix Me?")]
        public void ItShouldIgnoreMultipleValuesOnUrlToNoArgumentAction(string url) {
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithNothing());
        }

        [TestCase("~/ActionDataTypeInput/WithNothing/27")]
        [TestCase("~/ActionDataTypeInput/WithNothing/27/")]
        public void ItShouldIgnoreValueOnUrlToNoArgumentAction(string url) {
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithNothing());
        }


        [TestCase("~/ActionDataTypeInput/WithNothing")]
        [TestCase("~/ActionDataTypeInput/WithNothing/")]
        public void ItShouldMapActionUrlToNoArgumentAction(string url) {
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithNothing());
        }


        [TestCase("~/ActionDataTypeInput/WithGuid")]
        [TestCase("~/ActionDataTypeInput/WithGuid/")]
        [Ignore("Fix Me?")]
        public void ItShouldMapEmptyGuidToEmptyActionParamater(string url) {
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithGuid(default(Guid)));
        }

        [TestCase("~/ActionDataTypeInput/WithInteger")]
        [TestCase("~/ActionDataTypeInput/WithInteger/")]
        [Ignore("Fix Me?")]
        public void ItShouldMapEmptyIntAndTrailingSlashToDefaultActionParamaterValue(string url) {
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithInteger(default(int)));
        }

        [TestCase("~/ActionDataTypeInput/WithNullDateTime")]
        [TestCase("~/ActionDataTypeInput/WithNullDateTime/")]
        public void ItShouldMapEmptyNullableDateTimeToEmptyActionParamater(string url) {
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithNullDateTime(null));
        }


        [TestCase("~/ActionDataTypeInput/WithNullInteger")]
        [TestCase("~/ActionDataTypeInput/WithNullInteger/")]
        public void ItShouldMapEmptyNullableIntToEmptyActionParamater(string url) {
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithNullInteger(null));
        }


        [TestCase("~/ActionDataTypeInput/WithString")]
        [TestCase("~/ActionDataTypeInput/WithString/")]
        public void ItShouldMapEmptyStringToEmptyActionParamater(string url) {
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithString(null));
        }


        [TestCase("~/ActionDataTypeInput/WithDateTime/2015-07-01")]
        [TestCase("~/ActionDataTypeInput/WithDateTime/2015-07-01/")]
        public void ItShouldMapSpecifiedDateTimeToSpecifiedActionParamater(string url) {
            var expected = 1.July(2015);
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithDateTime(expected));
        }

        [TestCase("~/ActionDataTypeInput/WithGuid/88E262E5-9E61-4F98-BB0C-3837D2749C1A")]
        [TestCase("~/ActionDataTypeInput/WithGuid/88E262E5-9E61-4F98-BB0C-3837D2749C1A/")]
        public void ItShouldMapSpecifiedGuidToSpecifiedActionParamater(string url) {
            var expected = Guid.Parse("88E262E5-9E61-4F98-BB0C-3837D2749C1A");
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithGuid(expected));
        }


        [TestCase("~/ActionDataTypeInput/WithInteger/27")]
        [TestCase("~/ActionDataTypeInput/WithInteger/27/")]
        public void ItShouldMapSpecifiedIntToSpecifiedActionParamater(string url) {
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithInteger(27));
        }


        [TestCase("~/ActionDataTypeInput/WithNullDateTime/2015-07-01")]
        [TestCase("~/ActionDataTypeInput/WithNullDateTime/2015-07-01/")]
        public void ItShouldMapSpecifiedNullableDateTimeToSpecifiedActionParamater(string url) {
            var expected = 1.July(2015);
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                         controller.WithNullDateTime(
                                                                                     expected));
        }

        [TestCase("~/ActionDataTypeInput/WithNullInteger/27")]
        [TestCase("~/ActionDataTypeInput/WithNullInteger/27/")]
        public void ItShouldMapSpecifiedNullableIntToSpecifiedActionParamater(string url) {
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithNullInteger(27));
        }

        [TestCase("~/ActionDataTypeInput/WithString/35")]
        [TestCase("~/ActionDataTypeInput/WithString/35/")]
        public void ItShouldMapSpecifiedStringToSpecifiedActionParamater(string url) {
            url.UsingConfiguration(HttpConfiguration).Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.WithString("35"));
        }

        [TestCase("~/ActionDataTypeInput/PostNothing")]
        [TestCase("~/ActionDataTypeInput/PostNothing/")]
        public void ItShouldPostActionUrlToNoArgumentAction(string url) {
            url.WithHttpMethod(HttpMethod.Post).UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.PostNothing());
        }


        [TestCase("~/ActionDataTypeInput/PostInteger/27")]
        [TestCase("~/ActionDataTypeInput/PostInteger/27/")]
        public void ItShouldPostSpecifiedIntAndTrailingSlashToSpecifiedActionParamater(string url) {
            url.WithHttpMethod(HttpMethod.Post).UsingConfiguration(HttpConfiguration)
               .Should()
               .MapTo<ActionDataTypeInputController>(controller =>
                                                             controller.PostInteger(27));
        }

        [Test]
        public void ItShould405OnDisallowedMethodToGetAction(
            [Values("~/ActionDataTypeInput/WithNothing", "~/ActionDataTypeInput/WithNothing/")] string url,
            [Values("POST", "PUT", "DELETE")] string method) {
            Action act =
                () => url.WithHttpMethod(new HttpMethod(method)).UsingConfiguration(HttpConfiguration).SelectAction();
            act.ShouldThrow<HttpResponseException>()
               .And.Response.StatusCode.Should()
               .Be(HttpStatusCode.MethodNotAllowed);
        }

        [Test]
        public void ItShould405OnDisallowedMethodToPostAction(
            [Values("~/ActionDataTypeInput/PostNothing", "~/ActionDataTypeInput/PostNothing/")] string url,
            [Values("GET", "PUT", "DELETE")] string method) {
            Action act =
                () => url.WithHttpMethod(new HttpMethod(method)).UsingConfiguration(HttpConfiguration).SelectAction();
            act.ShouldThrow<HttpResponseException>()
               .And.Response.StatusCode.Should()
               .Be(HttpStatusCode.MethodNotAllowed);
        }
    }
}
