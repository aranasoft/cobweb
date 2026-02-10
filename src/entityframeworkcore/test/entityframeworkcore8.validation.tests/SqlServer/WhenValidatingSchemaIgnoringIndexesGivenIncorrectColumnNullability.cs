using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.SqlServer {
    [OperatingSystemRequirement(OperatingSystems.Windows)]
    public class
        WhenValidatingSchemaIgnoringIndexesGivenIncorrectColumnNullability : IClassFixture<
            SqlServerMigrationsFixture<MigrationsWithIncorrectColumnNullability>> {
        private readonly SqlServerMigrationsFixture<MigrationsWithIncorrectColumnNullability> _fixture;

        public WhenValidatingSchemaIgnoringIndexesGivenIncorrectColumnNullability(
            SqlServerMigrationsFixture<MigrationsWithIncorrectColumnNullability> fixture) {
            _fixture = fixture;
        }

        [ConditionalFact]
        public void ItShouldNotThrowValidationExceptionWhenIgnoringIndexes() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () =>
                applicationDbContext.ValidateSchema(
                    new SchemaValidationOptions { ValidateNullabilityForTables = false });
            validatingSchema.Should().NotThrow();
        }
    }
}
