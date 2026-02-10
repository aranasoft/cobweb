using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.SqlServer;
[OperatingSystemRequirement(OperatingSystems.Windows)]
public class
    WhenValidatingSchemaIgnoringForeignKeysGivenMissingForeignKeys : IClassFixture<
        SqlServerMigrationsFixture<MigrationsMissingForeignKeys>> {
    private readonly SqlServerMigrationsFixture<MigrationsMissingForeignKeys> _fixture;

    public WhenValidatingSchemaIgnoringForeignKeysGivenMissingForeignKeys(
        SqlServerMigrationsFixture<MigrationsMissingForeignKeys> fixture) {
        _fixture = fixture;
    }

    [ConditionalFact]
    public void ItShouldNotThrowValidationException() {
        var applicationDbContext = _fixture.GetContext();
        Action validatingSchema = () =>
            applicationDbContext.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
        validatingSchema.Should().NotThrow();
    }
}
