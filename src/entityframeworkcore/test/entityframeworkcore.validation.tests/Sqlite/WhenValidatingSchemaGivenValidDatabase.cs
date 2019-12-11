using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Sqlite;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Sqlite {
    public class WhenValidatingSchemaGivenValidDatabase : IClassFixture<SqliteMigrationsFixture<ValidIdentityMigrations>> {
        private readonly SqliteMigrationsFixture<ValidIdentityMigrations> _fixture;

        public WhenValidatingSchemaGivenValidDatabase(SqliteMigrationsFixture<ValidIdentityMigrations> fixture) {
            _fixture = fixture;
        }

        [Fact]
        public void ItShouldValidateAgainstExpectedSchema() {
            var context = _fixture.GetContext();
            Action validatingSchema = () => context.ValidateSchema(new SchemaValidationOptions {ValidateForeignKeys = false});
            validatingSchema.Should().NotThrow();
        }
    }
}
