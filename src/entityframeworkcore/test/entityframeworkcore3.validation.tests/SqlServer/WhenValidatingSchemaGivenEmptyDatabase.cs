using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.SqlServer {
    [OperatingSystemRequirement(OperatingSystems.Windows)]
    public class WhenValidatingSchemaGivenEmptyDatabase : IClassFixture<SqlServerLocalDbFixture> {
        private readonly SqlServerLocalDbFixture _fixture;

        public WhenValidatingSchemaGivenEmptyDatabase(SqlServerLocalDbFixture fixture) {
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
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotBeEmpty();
        }

        [ConditionalFact]
        public void ItShouldOnlyHaveMissingTableOrMissingViewErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().OnlyContain(error => error.StartsWith("Missing table: ", StringComparison.InvariantCultureIgnoreCase) || error.StartsWith("Missing view: ", StringComparison.InvariantCultureIgnoreCase));
        }

        [ConditionalFact]
        public void ItShouldHaveMissingViewErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().Contain(error => error.StartsWith("Missing view: ", StringComparison.InvariantCultureIgnoreCase));
        }

        [ConditionalFact]
        public void ItShouldHaveMissingTableErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().Contain(error => error.StartsWith("Missing table: ", StringComparison.InvariantCultureIgnoreCase));
        }

        [ConditionalFact]
        public void ItShouldNotHaveMissingColumnErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotContain(error => error.StartsWith("Missing Column", StringComparison.InvariantCultureIgnoreCase));
        }

        [ConditionalFact]
        public void ItShouldNotHaveMissingIndexErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotContain(error => error.StartsWith("Missing Index", StringComparison.InvariantCultureIgnoreCase));
        }

        [ConditionalFact]
        public void ItShouldNotHaveMissingForeignKeyErrors() {
            var applicationDbContext = _fixture.GetContext();
            Action validatingSchema = () => applicationDbContext.ValidateSchema();
            validatingSchema.Should().Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should().NotContain(error => error.StartsWith("Missing Foreign Key", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
