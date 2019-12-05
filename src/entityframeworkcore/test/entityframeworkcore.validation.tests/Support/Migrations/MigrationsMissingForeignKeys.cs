using System;
using Aranasoft.Cobweb.FluentMigrator.Extensions;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations
{
    [Migration(12345, "Add Identity Tables")]
    public class MigrationsMissingForeignKeys : Migration
    {
        public override void Up()
        {
            Create.Table("AspNetRoles")
                .WithColumn("Id", col => col.AsInt32().NotNullable().PrimaryKey("PK_AspNetRoles"))
                .WithColumn("ConcurrencyStamp", col => col.AsStringMax().Nullable())
                .WithColumn("Name", col => col.AsString(256).Nullable())
                .WithColumn("NormalizedName", col => col.AsString(256).Nullable())
                ;
            Create.Index("RoleNameIndex").OnTable("AspNetRoles")
                .OnColumn("NormalizedName", col => col.Unique().NullsNotDistinct())
                ;

            IfDatabase(dbType => string.Equals(dbType, "SQLite-Test", StringComparison.InvariantCultureIgnoreCase)).Delegate(() =>
            Create.Table("AspNetUsers")
                .WithColumn("Id", col => col.AsInt32().NotNullable().PrimaryKey("PK_AspNetUsers"))
                .WithColumn("AccessFailedCount", col => col.AsInt32().NotNullable())
                .WithColumn("ConcurrencyStamp", col => col.AsStringMax().Nullable())
                .WithColumn("Email", col => col.AsString(256).Nullable())
                .WithColumn("EmailConfirmed", col => col.AsBoolean().NotNullable())
                .WithColumn("LockoutEnabled", col => col.AsBoolean().NotNullable())
                .WithColumn("LockoutEnd", col => col.AsString().Nullable())
                .WithColumn("NormalizedEmail", col => col.AsString(256).Nullable().Indexed("EmailIndex"))
                .WithColumn("NormalizedUserName", col => col.AsString(256).Nullable())
                .WithColumn("PasswordHash", col => col.AsStringMax().Nullable())
                .WithColumn("PhoneNumber", col => col.AsStringMax().Nullable())
                .WithColumn("PhoneNumberConfirmed", col => col.AsBoolean().NotNullable())
                .WithColumn("SecurityStamp", col => col.AsStringMax().Nullable())
                .WithColumn("TwoFactorEnabled", col => col.AsBoolean().NotNullable())
                .WithColumn("UserName", col => col.AsString(256).Nullable())
                );
            IfDatabase(dbType => !string.Equals(dbType, "SQLite-Test", StringComparison.InvariantCultureIgnoreCase)).Delegate(() =>
            Create.Table("AspNetUsers")
                .WithColumn("Id", col => col.AsInt32().NotNullable().PrimaryKey("PK_AspNetUsers"))
                .WithColumn("AccessFailedCount", col => col.AsInt32().NotNullable())
                .WithColumn("ConcurrencyStamp", col => col.AsStringMax().Nullable())
                .WithColumn("Email", col => col.AsString(256).Nullable())
                .WithColumn("EmailConfirmed", col => col.AsBoolean().NotNullable())
                .WithColumn("LockoutEnabled", col => col.AsBoolean().NotNullable())
                .WithColumn("LockoutEnd", col => col.AsDateTimeOffset().Nullable())
                .WithColumn("NormalizedEmail", col => col.AsString(256).Nullable().Indexed("EmailIndex"))
                .WithColumn("NormalizedUserName", col => col.AsString(256).Nullable())
                .WithColumn("PasswordHash", col => col.AsStringMax().Nullable())
                .WithColumn("PhoneNumber", col => col.AsStringMax().Nullable())
                .WithColumn("PhoneNumberConfirmed", col => col.AsBoolean().NotNullable())
                .WithColumn("SecurityStamp", col => col.AsStringMax().Nullable())
                .WithColumn("TwoFactorEnabled", col => col.AsBoolean().NotNullable())
                .WithColumn("UserName", col => col.AsString(256).Nullable())
            );
            Create.Index("UserNameIndex").OnTable("AspNetUsers")
                .OnColumn("NormalizedUserName", col => col.Unique().NullsNotDistinct())
                ;

            Create.Table("AspNetRoleClaims")
                .WithColumn("Id", col => col.AsInt32().NotNullable().Identity().PrimaryKey("PK_AspNetRoleClaims"))
                .WithColumn("ClaimType", col => col.AsStringMax().Nullable())
                .WithColumn("ClaimValue", col => col.AsStringMax().Nullable())
                .WithColumn("RoleId", col => col.AsInt32().NotNullable()
                        .Indexed("IX_AspNetRoleClaims_RoleId"))
                ;

            Create.Table("AspNetUserClaims")
                .WithColumn("Id", col => col.AsInt32().NotNullable().Identity().PrimaryKey("PK_AspNetUserClaims"))
                .WithColumn("ClaimType", col => col.AsStringMax().Nullable())
                .WithColumn("ClaimValue", col => col.AsStringMax().Nullable())
                .WithColumn("UserId", col => col.AsInt32().NotNullable()
                        .Indexed("IX_AspNetUserClaims_UserId"))
                ;

            Create.Table("AspNetUserLogins")
                .WithColumn("LoginProvider", col => col.AsString(450).NotNullable().PrimaryKey("PK_AspNetUserLogins"))
                .WithColumn("ProviderKey", col => col.AsString(450).NotNullable().PrimaryKey("PK_AspNetUserLogins"))
                .WithColumn("ProviderDisplayName", col => col.AsStringMax().Nullable())
                .WithColumn("UserId", col => col.AsInt32().NotNullable()
                        .Indexed("IX_AspNetUserLogins_UserId"))
                ;

            Create.Table("AspNetUserRoles")
                .WithColumn("UserId", col => col.AsInt32().NotNullable().PrimaryKey("PK_AspNetUserRoles"))
                .WithColumn("RoleId", col => col.AsInt32().NotNullable().PrimaryKey("PK_AspNetUserRoles")
                        .Indexed("IX_AspNetUserRoles_RoleId"))
                ;

            Create.Table("AspNetUserTokens")
                .WithColumn("UserId", col => col.AsInt32().NotNullable().PrimaryKey("PK_AspNetUserTokens"))
                .WithColumn("LoginProvider", col => col.AsString(450).NotNullable().PrimaryKey("PK_AspNetUserTokens"))
                .WithColumn("Name", col => col.AsString(450).NotNullable().PrimaryKey("PK_AspNetUserTokens"))
                .WithColumn("Value", col => col.AsStringMax().Nullable())
                ;
        }

        public override void Down()
        {
            if (Schema.Table("AspNetRoleClaims").Exists())
            {
                Delete.Table("AspNetRoleClaims");
            }

            if (Schema.Table("AspNetUserClaims").Exists())
            {
                Delete.Table("AspNetUserClaims");
            }

            if (Schema.Table("AspNetUserLogins").Exists())
            {
                Delete.Table("AspNetUserLogins");
            }

            if (Schema.Table("AspNetUserRoles").Exists())
            {
                Delete.Table("AspNetUserRoles");
            }

            if (Schema.Table("AspNetUserTokens").Exists())
            {
                Delete.Table("AspNetUserTokens");
            }

            if (Schema.Table("AspNetRoles").Exists())
            {
                Delete.Table("AspNetRoles");
            }

            if (Schema.Table("AspNetUsers").Exists())
            {
                Delete.Table("AspNetUsers");
            }
        }
    }
}