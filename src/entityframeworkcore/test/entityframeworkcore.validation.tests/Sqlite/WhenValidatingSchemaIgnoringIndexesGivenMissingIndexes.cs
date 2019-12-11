using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Sqlite;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Sqlite {
    public class WhenValidatingSchemaIgnoringIndexesGivenMissingIndexes : IClassFixture<SqliteMigrationsFixture<MigrationsMissingIndexes>> {
        private readonly SqliteMigrationsFixture<MigrationsMissingIndexes> _fixture;

        public WhenValidatingSchemaIgnoringIndexesGivenMissingIndexes(SqliteMigrationsFixture<MigrationsMissingIndexes> fixture) {
            _fixture = fixture;
        }

        [Fact]
        public void ItShouldNotThrowValidationException() {
            var context = _fixture.GetContext();
            Action validatingSchema = () => context.ValidateSchema(new SchemaValidationOptions {ValidateForeignKeys = false, ValidateIndexes = false});
            validatingSchema.Should().NotThrow();
        }
    }
}
