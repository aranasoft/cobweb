using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.SqlServer {
    public class WhenValidatingSchemaGivenMissingForeignKeys : IClassFixture<SqlServerMigrationsFixture<MigrationsMissingForeignKeys>> {
        private readonly SqlServerMigrationsFixture<MigrationsMissingForeignKeys> _fixture;

        public WhenValidatingSchemaGivenMissingForeignKeys(SqlServerMigrationsFixture<MigrationsMissingForeignKeys> fixture) {
            _fixture = fixture;
        }

        [Fact]
        public void ItShouldThrowValidationException() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().ThrowExactly<SchemaValidationException>();
        }

        [Fact]
        public void ItShouldHaveValidationErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotBeEmpty();
        }

        [Fact]
        public void ItShouldNotHaveMissingTableErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotContain(error => error.StartsWith("Missing Table", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void ItShouldNotHaveMissingColumnErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotContain(error => error.StartsWith("Missing Column", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void ItShouldNotHaveMissingIndexErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotContain(error => error.StartsWith("Missing Index", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void ItShouldOnlyHaveMissingForeignKeyErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().OnlyContain(error => error.StartsWith("Missing Foreign Key", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
