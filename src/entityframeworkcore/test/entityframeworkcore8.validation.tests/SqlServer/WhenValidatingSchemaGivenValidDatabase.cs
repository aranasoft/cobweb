using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.SqlServer;
[OperatingSystemRequirement(OperatingSystems.Windows)]
public class
    WhenValidatingSchemaGivenValidDatabase : IClassFixture<SqlServerMigrationsFixture<ValidIdentityMigrations>> {
    private readonly SqlServerMigrationsFixture<ValidIdentityMigrations> _fixture;

    public WhenValidatingSchemaGivenValidDatabase(SqlServerMigrationsFixture<ValidIdentityMigrations> fixture) {
        _fixture = fixture;
    }

    [ConditionalFact]
    public void ItShouldValidateAgainstExpectedSchema() {
        var applicationDbContext = _fixture.GetContext();
        Action validatingSchema = () => applicationDbContext.ValidateSchema();
        validatingSchema.Should().NotThrow();
    }
}
