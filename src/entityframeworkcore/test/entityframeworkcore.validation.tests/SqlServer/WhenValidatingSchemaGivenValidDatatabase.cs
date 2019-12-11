using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.SqlServer {
    public class WhenValidatingSchemaGivenValidDatatabase : IClassFixture<SqlServerMigrationsFixture<ValidIdentityMigrations>> {
        private readonly SqlServerMigrationsFixture<ValidIdentityMigrations> _fixture;

        public WhenValidatingSchemaGivenValidDatatabase(SqlServerMigrationsFixture<ValidIdentityMigrations> fixture) {
            _fixture = fixture;
        }

        [Fact]
        public void ItShouldValidateAgainstExpectedSchema() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().NotThrow();
        }
    }
}
