using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Sqlite;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Sqlite;
public class
    WhenValidatingSchemaIgnoringIndexesGivenIncorrectColumnNullability : IClassFixture<
        SqliteMigrationsFixture<MigrationsWithIncorrectColumnNullability>> {
    private readonly SqliteMigrationsFixture<MigrationsWithIncorrectColumnNullability> _fixture;

    public WhenValidatingSchemaIgnoringIndexesGivenIncorrectColumnNullability(
        SqliteMigrationsFixture<MigrationsWithIncorrectColumnNullability> fixture) {
        _fixture = fixture;
    }

    [Fact]
    public void ItShouldNotThrowValidationException() {
        var context = _fixture.GetContext();
        Action validatingSchema = () => context.ValidateSchema(new SchemaValidationOptions {
            ValidateForeignKeys = false, ValidateNullabilityForTables = false
        });
        validatingSchema.Should().NotThrow();
    }
}
