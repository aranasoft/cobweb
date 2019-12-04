﻿using System;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations;
using Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Sqlite;
using FluentAssertions;
using NUnit.Framework;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Sqlite {
    [TestFixture]
    public class WhenValidatingSchemaGivenValidDatabase : SqliteMigrationsFixture<ValidIdentityMigrations> {
        protected Action ValidatingSchema { get; set; }

        [OneTimeSetUp]
        public void ConfigureContext() {
            ValidatingSchema = () =>
                GetContext().ValidateSchema(new SchemaValidationOptions{ValidateForeignKeys = false});
        }

        [Test]
        public void ItShouldValidateAgainstExpectedSchema() {
            ValidatingSchema.Should().NotThrow();
        }
    }
}
