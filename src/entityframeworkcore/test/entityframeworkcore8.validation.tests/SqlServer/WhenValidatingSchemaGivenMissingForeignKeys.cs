using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.SqlServer {
    [OperatingSystemRequirement(OperatingSystems.Windows)]
    public class
        WhenValidatingSchemaGivenMissingForeignKeys : IClassFixture<
            SqlServerMigrationsFixture<MigrationsMissingForeignKeys>> {
        private readonly SqlServerMigrationsFixture<MigrationsMissingForeignKeys> _fixture;

        public WhenValidatingSchemaGivenMissingForeignKeys(
            SqlServerMigrationsFixture<MigrationsMissingForeignKeys> fixture) {
            _fixture = fixture;
        }

        [ConditionalFact]
        public void ItShouldThrowValidationException() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().ThrowExactly<SchemaValidationException>();
        }

        [ConditionalFact]
        public void ItShouldHaveValidationErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotBeEmpty();
        }

        [ConditionalFact]
        public void ItShouldNotHaveMissingTableErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotContain(
                                error => error.StartsWith("Missing Table",
                                                          StringComparison.InvariantCultureIgnoreCase));
        }

        [ConditionalFact]
        public void ItShouldNotHaveMissingViewErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotContain(
                                error => error.StartsWith("Missing View", StringComparison.InvariantCultureIgnoreCase));
        }

        [ConditionalFact]
        public void ItShouldNotHaveMissingColumnErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotContain(
                                error => error.StartsWith("Missing Column",
                                                          StringComparison.InvariantCultureIgnoreCase));
        }

        [ConditionalFact]
        public void ItShouldNotHaveMissingIndexErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotContain(
                                error => error.StartsWith("Missing Index",
                                                          StringComparison.InvariantCultureIgnoreCase));
        }

        [ConditionalFact]
        public void ItShouldOnlyHaveMissingForeignKeyErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .OnlyContain(error => error.StartsWith("Missing Foreign Key",
                                                                   StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
