using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Sqlite;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Sqlite {
    public class WhenValidatingSchemaGivenMissingForeignKeys : IClassFixture<SqliteMigrationsFixture<MigrationsMissingForeignKeys>> {
        private readonly SqliteMigrationsFixture<MigrationsMissingForeignKeys> _fixture;

        public WhenValidatingSchemaGivenMissingForeignKeys(SqliteMigrationsFixture<MigrationsMissingForeignKeys> fixture) {
            _fixture = fixture;
        }

        [Fact]
        public void ItShouldNotThrowValidationException() {
            var context = _fixture.GetContext();
            Action validatingSchema = () => context.ValidateSchema(new SchemaValidationOptions {ValidateForeignKeys = false});
            validatingSchema.Should().NotThrow("SQLite does not migrate foreign keys");
        }
    }
}
