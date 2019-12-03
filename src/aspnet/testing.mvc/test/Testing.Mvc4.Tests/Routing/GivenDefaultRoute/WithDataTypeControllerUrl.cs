using System;
using System.Web.Mvc;
using Cobweb.Testing.Mvc.Assertions;
using Cobweb.Testing.Mvc.Extensions;
using Cobweb.Testing.Mvc.Tests.TestableTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Cobweb.Testing.Mvc.Tests.Routing.GivenDefaultRoute {
    [TestFixture]
    public class WithDataTypeControllerUrl : GivenDefaultRoute {
        [Test]
        public void ItShouldMapControllerUrlToDefaultAction() {
            "~/DataTypeInput".Should().MapTo<DataTypeInputController>(controller => controller.Index());
        }

        [Test]
        public void ItShouldMapActionUrlToNoArgumentAction() {
            "~/DataTypeInput/WithNothing".Should()
                                         .MapTo<DataTypeInputController>(controller =>
                                                                             controller.WithNothing());
        }

        [Test]
        public void ItShouldMapActionUrlWithTrailingSlashToNoArgumentAction() {
            "~/DataTypeInput/WithNothing/".Should()
                                          .MapTo<DataTypeInputController>(controller =>
                                                                              controller.WithNothing());
        }

        [Test]
        public void ItShouldPostActionUrlToNoArgumentAction() {
            "~/DataTypeInput/PostNothing".WithMethod(HttpVerbs.Post)
                                         .Should()
                                         .MapTo<DataTypeInputController>(controller =>
                                                                             controller.PostNothing());
        }

        [Test]
        public void ItShouldPostActionUrlWithTrailingSlashToNoArgumentAction() {
            "~/DataTypeInput/PostNothing/".WithMethod(HttpVerbs.Post)
                                          .Should()
                                          .MapTo<DataTypeInputController>(controller =>
                                                                              controller.PostNothing());
        }

        [Test, Ignore("Fix: Need to do Action Selection on Controller")]
        public void ItShouldNotMapActionUrlWithIncorrectActionToNoArgumentAction() {
            "~/DataTypeInput/WithNothing".WithMethod(HttpVerbs.Post)
                                         .Should()
                                         .BeNull();
        }

        [Test, Ignore("Fix: Need to do Action Selection on Controller")]
        public void ItShouldNotMapActionUrlWithTrailingSlashAndIncorrectActionToNoArgumentAction() {
            "~/DataTypeInput/WithNothing/".WithMethod(HttpVerbs.Post)
                                          .Should()
                                          .BeNull();
        }

        [Test, Ignore("Fix: Need to do Action Selection on Controller")]
        public void ItShouldNotPostActionUrlWithIncorrectActionToNoArgumentAction() {
            "~/DataTypeInput/PostNothing".WithMethod(HttpVerbs.Get)
                                         .Should()
                                         .BeNull();
        }

        [Test, Ignore("Fix: Need to do Action Selection on Controller")]
        public void ItShouldNotPostActionUrlWithTrailingSlashAndIncorrectActionToNoArgumentAction() {
            "~/DataTypeInput/PostNothing/".WithMethod(HttpVerbs.Get)
                                          .Should()
                                          .BeNull();
        }

        [Test]
        public void ItShouldIgnoreValueOnUrlToNoArgumentAction() {
            "~/DataTypeInput/WithNothing/27".Should()
                                            .MapTo<DataTypeInputController>(controller =>
                                                                                controller.WithNothing());
        }

        [Test]
        public void ItShouldIgnoreValueWithTrailingSlashOnUrlToNoArgumentAction() {
            "~/DataTypeInput/WithNothing/27/".Should()
                                             .MapTo<DataTypeInputController>(controller =>
                                                                                 controller.WithNothing());
        }

        [Test, Ignore("Fix Me?")]
        public void ItShouldIgnoreMultipleValuesOnUrlToNoArgumentAction() {
            "~/DataTypeInput/WithNothing/27/1".Should()
                                              .MapTo<DataTypeInputController>(controller =>
                                                                                  controller.WithNothing());
        }

        [Test, Ignore("Fix Me?")]
        public void ItShouldMapEmptyIntToDefaultActionParamaterValue() {
            "~/DataTypeInput/WithInteger".Should()
                                         .MapTo<DataTypeInputController>(controller =>
                                                                             controller.WithInteger(default(int)));
        }

        [Test, Ignore("Fix Me?")]
        public void ItShouldMapEmptyIntAndTrailingSlashToDefaultActionParamaterValue() {
            "~/DataTypeInput/WithInteger/".Should()
                                          .MapTo<DataTypeInputController>(controller =>
                                                                              controller.WithInteger(default(int)));
        }

        [Test]
        public void ItShouldMapSpecifiedIntToSpecifiedActionParamater() {
            "~/DataTypeInput/WithInteger/27".Should()
                                            .MapTo<DataTypeInputController>(controller =>
                                                                                controller.WithInteger(27));
        }

        [Test]
        public void ItShouldMapSpecifiedIntAndTrailingSlashToSpecifiedActionParamater() {
            "~/DataTypeInput/WithInteger/27/".Should()
                                             .MapTo<DataTypeInputController>(controller =>
                                                                                 controller.WithInteger(27));
        }

        [Test]
        public void ItShouldPostSpecifiedIntToSpecifiedActionParamater() {
            "~/DataTypeInput/PostInteger/27".WithMethod(HttpVerbs.Post)
                                            .Should()
                                            .MapTo<DataTypeInputController>(controller =>
                                                                                controller.PostInteger(27));
        }

        [Test]
        public void ItShouldPostSpecifiedIntAndTrailingSlashToSpecifiedActionParamater() {
            "~/DataTypeInput/PostInteger/27/".WithMethod(HttpVerbs.Post)
                                             .Should()
                                             .MapTo<DataTypeInputController>(controller =>
                                                                                 controller.PostInteger(27));
        }

        [Test]
        public void ItShouldMapEmptyNullableIntToEmptyActionParamater() {
            "~/DataTypeInput/WithNullInteger".Should()
                                             .MapTo<DataTypeInputController>(controller =>
                                                                                 controller.WithNullInteger(null));
        }

        [Test]
        public void ItShouldMapEmptyNullableIntAndTrailingSlashToEmptyActionParamater() {
            "~/DataTypeInput/WithNullInteger/".Should()
                                              .MapTo<DataTypeInputController>(controller =>
                                                                                  controller.WithNullInteger(null));
        }

        [Test]
        public void ItShouldMapSpecifiedNullableIntToSpecifiedActionParamater() {
            "~/DataTypeInput/WithNullInteger/27".Should()
                                                .MapTo<DataTypeInputController>(controller =>
                                                                                    controller.WithNullInteger(27));
        }

        [Test]
        public void ItShouldMapSpecifiedNullableIntAndTrailingSlashToSpecifiedActionParamater() {
            "~/DataTypeInput/WithNullInteger/27/".Should()
                                                 .MapTo<DataTypeInputController>(controller =>
                                                                                     controller.WithNullInteger(27));
        }

        [Test]
        public void ItShouldMapEmptyStringToEmptyActionParamater() {
            "~/DataTypeInput/WithString".Should()
                                        .MapTo<DataTypeInputController>(controller =>
                                                                            controller.WithString(null));
        }

        [Test]
        public void ItShouldMapEmptyStringTrailingSlashToEmptyActionParamater() {
            "~/DataTypeInput/WithString/".Should()
                                         .MapTo<DataTypeInputController>(controller =>
                                                                             controller.WithString(null));
        }

        [Test]
        public void ItShouldMapSpecifiedStringToSpecifiedActionParamater() {
            "~/DataTypeInput/WithString/35".Should()
                                           .MapTo<DataTypeInputController>(controller =>
                                                                               controller.WithString("35"));
        }

        [Test]
        public void ItShouldMapSpecifiedStringTrailingSlashToSpecifiedActionParamater() {
            "~/DataTypeInput/WithString/35/".Should()
                                            .MapTo<DataTypeInputController>(controller =>
                                                                                controller.WithString("35"));
        }

        [Test, Ignore("Fix Me?")]
        public void ItShouldMapEmptyGuidToEmptyActionParamater() {
            "~/DataTypeInput/WithGuid".Should()
                                      .MapTo<DataTypeInputController>(controller =>
                                                                          controller.WithGuid(default(Guid)));
        }

        [Test, Ignore("Fix Me?")]
        public void ItShouldMapEmptyGuidTrailingSlashToEmptyActionParamater() {
            "~/DataTypeInput/WithGuid/".Should()
                                       .MapTo<DataTypeInputController>(controller =>
                                                                           controller.WithGuid(default(Guid)));
        }

        [Test]
        public void ItShouldMapSpecifiedGuidToSpecifiedActionParamater() {
            var expected = Guid.Parse("88E262E5-9E61-4F98-BB0C-3837D2749C1A");
            const string actualUrl = "~/DataTypeInput/WithGuid/88E262E5-9E61-4F98-BB0C-3837D2749C1A";
            actualUrl.Should()
                     .MapTo<DataTypeInputController>(controller =>
                                                         controller.WithGuid(expected));
        }

        [Test]
        public void ItShouldMapSpecifiedGuidTrailingSlashToSpecifiedActionParamater() {
            var expected = Guid.Parse("88E262E5-9E61-4F98-BB0C-3837D2749C1A");
            const string actualUrl = "~/DataTypeInput/WithGuid/88E262E5-9E61-4F98-BB0C-3837D2749C1A/";
            actualUrl.Should()
                     .MapTo<DataTypeInputController>(controller =>
                                                         controller.WithGuid(expected));
        }

        [Test]
        public void ItShouldMapSpecifiedDateTimeToSpecifiedActionParamater() {
            var expected = 1.July(2015);
            const string actualUrl = "~/DataTypeInput/WithDateTime/2015-07-01";
            actualUrl.Should()
                     .MapTo<DataTypeInputController>(controller =>
                                                         controller.WithDateTime(expected));
        }

        [Test]
        public void ItShouldMapSpecifiedDateTimeAndTrailingSlashToSpecifiedActionParamater() {
            var expected = 1.July(2015);
            const string actualUrl = "~/DataTypeInput/WithDateTime/2015-07-01/";
            actualUrl.Should()
                     .MapTo<DataTypeInputController>(controller =>
                                                         controller.WithDateTime(expected));
        }

        [Test]
        public void ItShouldMapEmptyNullableDateTimeToEmptyActionParamater() {
            "~/DataTypeInput/WithNullDateTime".Should()
                                              .MapTo<DataTypeInputController>(controller =>
                                                                                  controller.WithNullDateTime(null));
        }

        [Test]
        public void ItShouldMapEmptyNullableDateTimeAndTrailingSlashToEmptyActionParamater() {
            "~/DataTypeInput/WithNullDateTime/".Should()
                                               .MapTo<DataTypeInputController>(controller =>
                                                                                   controller.WithNullDateTime(null));
        }

        [Test]
        public void ItShouldMapSpecifiedNullableDateTimeToSpecifiedActionParamater() {
            var expected = 1.July(2015);
            const string actualUrl = "~/DataTypeInput/WithNullDateTime/2015-07-01";
            actualUrl.Should()
                     .MapTo<DataTypeInputController>(controller =>
                                                         controller.WithNullDateTime(
                                                             expected));
        }

        [Test]
        public void ItShouldMapSpecifiedNullableDateTimeAndTrailingSlashToSpecifiedActionParamater() {
            var expected = 1.July(2015);
            const string actualUrl = "~/DataTypeInput/WithNullDateTime/2015-07-01/";
            actualUrl.Should()
                     .MapTo<DataTypeInputController>(controller =>
                                                         controller.WithNullDateTime(
                                                             expected));
        }
    }
}
