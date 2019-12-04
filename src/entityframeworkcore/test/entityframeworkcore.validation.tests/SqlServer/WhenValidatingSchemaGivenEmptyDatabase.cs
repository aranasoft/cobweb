using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.SqlServer {
    [TestFixture]
    public class WhenValidatingSchemaGivenEmptyDatabase : SqlServerLocalDbFixture {
        protected Action ValidatingSchema { get; set; }

        [OneTimeSetUp]
        public void ConfigureContext() {
            ValidatingSchema = () => GetContext().ValidateSchema();
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
        public void ItShouldOnlyHaveMissingTableErrors() {
            ValidatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().OnlyContain(error => error.StartsWith("Missing table: ", StringComparison.InvariantCultureIgnoreCase));
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
        public void ItShouldNotHaveMissingForeignKeyErrors() {
            ValidatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotContain(error => error.StartsWith("Missing Foreign Key", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
