using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.SqlServer {
    public class WhenValidatingSchemaIgnoringIndexesGivenMissingIndexes : IClassFixture<SqlServerMigrationsFixture<MigrationsMissingIndexes>> {
        private readonly SqlServerMigrationsFixture<MigrationsMissingIndexes> _fixture;

        public WhenValidatingSchemaIgnoringIndexesGivenMissingIndexes(SqlServerMigrationsFixture<MigrationsMissingIndexes> fixture) {
            _fixture = fixture;
        }

        [Fact]
        public void ItShouldNotThrowValidationExceptionWhenIgnoringIndexes() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema(new SchemaValidationOptions{ValidateIndexes = false});
            validatingSchema.Should().NotThrow();
        }
    }
}
