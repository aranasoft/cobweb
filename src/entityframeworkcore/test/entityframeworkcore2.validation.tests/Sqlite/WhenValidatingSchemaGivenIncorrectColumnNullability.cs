using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Sqlite;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Sqlite {
    public class
        WhenValidatingSchemaGivenIncorrectColumnNullability : IClassFixture<
            SqliteMigrationsFixture<MigrationsWithIncorrectColumnNullability>> {
        private readonly SqliteMigrationsFixture<MigrationsWithIncorrectColumnNullability> _fixture;

        public WhenValidatingSchemaGivenIncorrectColumnNullability(
            SqliteMigrationsFixture<MigrationsWithIncorrectColumnNullability> fixture) {
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
        public void ItShouldNotHaveMissingTableErrors() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotContain(
                                error => error.StartsWith("Missing Table",
                                                          StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void ItShouldNotHaveMissingViewErrors() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotContain(
                                error => error.StartsWith("Missing View", StringComparison.InvariantCultureIgnoreCase));
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
        public void ItShouldNotHaveColumnTypeMismatchErrors() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .NotContain(error => error.StartsWith("Column type mismatch",
                                                                  StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void ItShouldOnlyHaveColumnNullabilityMismatchErrors() {
            var context = _fixture.GetContext();
            Action validatingSchema = () =>
                context.ValidateSchema(new SchemaValidationOptions { ValidateForeignKeys = false });
            validatingSchema.Should()
                            .Throw<SchemaValidationException>()
                            .Which.ValidationErrors
                            .Should()
                            .OnlyContain(error => error.StartsWith("Column nullability mismatch",
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
