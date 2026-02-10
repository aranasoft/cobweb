using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Sqlite;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Sqlite {
    public class WhenValidatingSchemaGivenAnEmptyDatabase : IClassFixture<SqliteDatabaseFixture> {
        private readonly SqliteDatabaseFixture _fixture;

        public WhenValidatingSchemaGivenAnEmptyDatabase(SqliteDatabaseFixture fixture) {
            _fixture = fixture;
        }

        [Fact]
        public void ItShouldThrowValidationException() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should().ThrowExactly<SchemaValidationException>();
        }

        [Fact]
        public void ItShouldHaveValidationErrors() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotBeEmpty();
        }

        [Fact]
        public void ItShouldOnlyHaveMissingTableErrorsAndMissingViewErrors() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .OnlyContain(
                                error =>
                                    error.StartsWith("Missing table:", StringComparison.InvariantCultureIgnoreCase) ||
                                    error.StartsWith("Missing view:", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void ItShouldHaveMissingTableErrors() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .Contain(error => error.StartsWith("Missing table:",
                                                               StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void ItShouldHaveMissingViewErrors() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .Contain(error => error.StartsWith("Missing view:",
                                                               StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void ItShouldNotHaveMissingColumnErrors() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotContain(
                                error => error.StartsWith("Missing Column",
                                                          StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void ItShouldNotHaveMissingIndexErrors() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotContain(
                                error => error.StartsWith("Missing Index",
                                                          StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void ItShouldNotHaveMissingForeignKeyErrors() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotContain(error => error.StartsWith("Missing Foreign Key",
                                                                  StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
