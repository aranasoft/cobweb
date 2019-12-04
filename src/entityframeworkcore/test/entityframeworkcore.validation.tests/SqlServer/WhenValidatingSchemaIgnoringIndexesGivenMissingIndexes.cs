using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.SqlServer {
    [TestFixture]
    public class WhenValidatingSchemaIgnoringIndexesGivenMissingIndexes : SqlServerMigrationsFixture<MigrationsMissingIndexes> {
        protected Action ValidatingSchema { get; set; }

        [OneTimeSetUp]
        public void ConfigureContext() {
            ValidatingSchema = () =>
                GetContext().ValidateSchema(new SchemaValidationOptions{ValidateIndexes = false});
        }

        [Test]
        public void ItShouldNotThrowValidationExceptionWhenIgnoringIndexes() {
            ValidatingSchema.Should().NotThrow();
        }
    }
}
