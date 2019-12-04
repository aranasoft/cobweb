using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.SqlServer {
    [TestFixture]
    public class WhenValidatingSchemaGivenMissingForeignKeys : SqlServerMigrationsFixture<MigrationsMissingForeignKeys> {
        protected Action ValidatingSchema { get; set; }

        [OneTimeSetUp]
        public void ConfigureContext() {
            ValidatingSchema = () =>
                GetContext().ValidateSchema();
        }

        [Test]
        public void ItShouldThrowValidationException() {
            ValidatingSchema.Should().ThrowExactly<SchemaValidationException>();
        }

        [Test]
        public void ItShouldHaveValidationErrors() {
            ValidatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotBeEmpty();
        }

        [Test]
        public void ItShouldNotHaveMissingTableErrors() {
            ValidatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotContain(error => error.StartsWith("Missing Table", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void ItShouldNotHaveMissingColumnErrors() {
            ValidatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotContain(error => error.StartsWith("Missing Column", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void ItShouldNotHaveMissingIndexErrors() {
            ValidatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotContain(error => error.StartsWith("Missing Index", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void ItShouldOnlyHaveMissingForeignKeyErrors() {
            ValidatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().OnlyContain(error => error.StartsWith("Missing Foreign Key", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
