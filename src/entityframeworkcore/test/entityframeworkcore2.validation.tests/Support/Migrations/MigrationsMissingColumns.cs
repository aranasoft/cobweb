using System;
using System.Data;
using Aranasoft.Cobweb.FluentMigrator.Extensions;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Migrations {
    [Migration(12345, "Add Identity Tables")]
    public class MigrationsMissingColumns : Migration {
        public override void Up() {
            Create.Table("AspNetRoles")
                  .WithColumn("Id", col => col.AsInt32().NotNullable().PrimaryKey("PK_AspNetRoles"))
                  .WithColumn("ConcurrencyStamp", col => col.AsStringMax().Nullable())
                  .WithColumn("Name", col => col.AsString(256).Nullable())
                  .WithColumn("NormalizedName", col => col.AsString(256).Nullable())
                ;
            Create.Index("RoleNameIndex")
                  .OnTable("AspNetRoles")
                  .OnColumn("NormalizedName", col => col.Unique().NullsNotDistinct())
                ;

            IfDatabase(dbType => string.Equals(dbType, "SQLite-Test", StringComparison.InvariantCultureIgnoreCase))
                .Delegate(() => {
                    Create.Table("AspNetUsers")
                          .WithColumn("Id", col => col.AsInt32().NotNullable().PrimaryKey("PK_AspNetUsers"))
                          .WithColumn("AccessFailedCount", col => col.AsInt32().NotNullable())
                          .WithColumn("ConcurrencyStamp", col => col.AsStringMax().Nullable())
                          .WithColumn("Email", col => col.AsString(256).Nullable())
                          .WithColumn("EmailConfirmed", col => col.AsBoolean().NotNullable())
                          .WithColumn("LockoutEnabled", col => col.AsBoolean().NotNullable())
                          .WithColumn("LockoutEnd", col => col.AsString().Nullable())
                          .WithColumn("NormalizedEmail",
                                      col => col.AsString(256).Nullable().Indexed("EmailIndex"))
                          .WithColumn("NormalizedUserName", col => col.AsString(256).Nullable())
                          .WithColumn("PasswordHash", col => col.AsStringMax().Nullable())
                          .WithColumn("PhoneNumber", col => col.AsStringMax().Nullable())
                          .WithColumn("PhoneNumberConfirmed", col => col.AsBoolean().NotNullable())
                          .WithColumn("SecurityStamp", col => col.AsStringMax().Nullable())
                          .WithColumn("TwoFactorEnabled", col => col.AsBoolean().NotNullable())
                          .WithColumn("UserName", col => col.AsString(256).Nullable());
                });
            IfDatabase(dbType => !string.Equals(dbType, "SQLite-Test", StringComparison.InvariantCultureIgnoreCase))
                .Delegate(() => {
                    Create.Table("AspNetUsers")
                          .WithColumn("Id", col => col.AsInt32().NotNullable().PrimaryKey("PK_AspNetUsers"))
                          .WithColumn("AccessFailedCount", col => col.AsInt32().NotNullable())
                          .WithColumn("ConcurrencyStamp", col => col.AsStringMax().Nullable())
                          .WithColumn("Email", col => col.AsString(256).Nullable())
                          .WithColumn("EmailConfirmed", col => col.AsBoolean().NotNullable())
                          .WithColumn("LockoutEnabled", col => col.AsBoolean().NotNullable())
                          .WithColumn("LockoutEnd", col => col.AsDateTimeOffset().Nullable())
                          .WithColumn("NormalizedEmail",
                                      col => col.AsString(256).Nullable().Indexed("EmailIndex"))
                          .WithColumn("NormalizedUserName", col => col.AsString(256).Nullable())
                          .WithColumn("PasswordHash", col => col.AsStringMax().Nullable())
                          .WithColumn("PhoneNumber", col => col.AsStringMax().Nullable())
                          .WithColumn("PhoneNumberConfirmed", col => col.AsBoolean().NotNullable())
                          .WithColumn("SecurityStamp", col => col.AsStringMax().Nullable())
                          .WithColumn("TwoFactorEnabled", col => col.AsBoolean().NotNullable())
                          .WithColumn("UserName", col => col.AsString(256).Nullable());
                });
            Create.Index("UserNameIndex")
                  .OnTable("AspNetUsers")
                  .OnColumn("NormalizedUserName", col => col.Unique().NullsNotDistinct())
                ;

            Create.Table("AspNetRoleClaims")
                  .WithColumn("Id", col => col.AsInt32().NotNullable().Identity().PrimaryKey("PK_AspNetRoleClaims"))
                  .WithColumn("ClaimValue", col => col.AsStringMax().Nullable())
                  .WithColumn("RoleId",
                              col => col.AsInt32()
                                        .NotNullable()
                                        .Indexed("IX_AspNetRoleClaims_RoleId")
                                        .ForeignKey("FK_AspNetRoleClaims_AspNetRoles_RoleId", "AspNetRoles", "Id")
                                        .OnDelete(Rule.Cascade))
                ;

            Create.Table("AspNetUserClaims")
                  .WithColumn("Id", col => col.AsInt32().NotNullable().Identity().PrimaryKey("PK_AspNetUserClaims"))
                  .WithColumn("ClaimType", col => col.AsStringMax().Nullable())
                  .WithColumn("ClaimValue", col => col.AsStringMax().Nullable())
                  .WithColumn("UserId",
                              col => col.AsInt32()
                                        .NotNullable()
                                        .Indexed("IX_AspNetUserClaims_UserId")
                                        .ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId", "AspNetUsers", "Id")
                                        .OnDelete(Rule.Cascade))
                ;

            Create.Table("AspNetUserLogins")
                  .WithColumn("LoginProvider", col => col.AsString(450).NotNullable().PrimaryKey("PK_AspNetUserLogins"))
                  .WithColumn("ProviderDisplayName", col => col.AsStringMax().Nullable())
                  .WithColumn("UserId",
                              col => col.AsInt32()
                                        .NotNullable()
                                        .Indexed("IX_AspNetUserLogins_UserId")
                                        .ForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId", "AspNetUsers", "Id")
                                        .OnDelete(Rule.Cascade))
                ;

            Create.Table("AspNetUserRoles")
                  .WithColumn("UserId",
                              col => col.AsInt32()
                                        .NotNullable()
                                        .PrimaryKey("PK_AspNetUserRoles")
                                        .ForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId", "AspNetUsers", "Id")
                                        .OnDelete(Rule.Cascade))
                  .WithColumn("RoleId",
                              col => col.AsInt32()
                                        .NotNullable()
                                        .PrimaryKey("PK_AspNetUserRoles")
                                        .Indexed("IX_AspNetUserRoles_RoleId")
                                        .ForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId", "AspNetRoles", "Id")
                                        .OnDelete(Rule.Cascade))
                ;

            Create.Table("AspNetUserTokens")
                  .WithColumn("UserId",
                              col => col.AsInt32()
                                        .NotNullable()
                                        .PrimaryKey("PK_AspNetUserTokens")
                                        .ForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId", "AspNetUsers", "Id")
                                        .OnDelete(Rule.Cascade))
                  .WithColumn("LoginProvider", col => col.AsString(450).NotNullable().PrimaryKey("PK_AspNetUserTokens"))
                  .WithColumn("Name", col => col.AsString(450).NotNullable().PrimaryKey("PK_AspNetUserTokens"))
                  .WithColumn("Value", col => col.AsStringMax().Nullable())
                ;

            IfDatabase(dbType => string.Equals(dbType, "SQLite-Test", StringComparison.InvariantCultureIgnoreCase))
                .Delegate(() => {
                    Create.Table("TableBasedEntity")
                          .WithColumn("Id",
                                      col => col.AsInt32().NotNullable().PrimaryKey("PK_TableBasedEntity"))
                          .WithColumn("Field", col => col.AsString(256).Nullable())
                          .WithColumn("NumberValue", col => col.AsString(20000).NotNullable())
                          .WithColumn("DefaultedNumberValue", col => col.AsInt32().NotNullable().WithDefaultValue(900))
                          .WithColumn("RoleId",
                                      col => col.AsInt32()
                                                .NotNullable()
                                                .Indexed("IX_TableBasedEntity_RoleId")
                                                .ForeignKey("FK_TableBasedEntity_AspNetRoles_RoleId",
                                                            "AspNetRoles",
                                                            "Id")
                                                .OnDelete(Rule.Cascade))
                        ;
                });
            IfDatabase(dbType => !string.Equals(dbType, "SQLite-Test", StringComparison.InvariantCultureIgnoreCase))
                .Delegate(() => {
                    Create.Table("TableBasedEntity")
                          .WithColumn("Id",
                                      col => col.AsInt32().NotNullable().PrimaryKey("PK_TableBasedEntity"))
                          .WithColumn("Field", col => col.AsString(256).Nullable())
                          .WithColumn("NumberValue", col => col.AsDecimal(18, 2).NotNullable())
                          .WithColumn("DefaultedNumberValue", col => col.AsInt32().NotNullable().WithDefaultValue(900))
                          .WithColumn("RoleId",
                                      col => col.AsInt32()
                                                .NotNullable()
                                                .Indexed("IX_TableBasedEntity_RoleId")
                                                .ForeignKey("FK_TableBasedEntity_AspNetRoles_RoleId",
                                                            "AspNetRoles",
                                                            "Id")
                                                .OnDelete(Rule.Cascade))
                        ;
                });

            Execute.Sql(@"CREATE VIEW ViewBasedEntities AS SELECT Id, Field, RoleId FROM TableBasedEntity");

            Create.Table("TableBasedChildEntity")
                  .WithColumn("Id", col => col.AsInt32().NotNullable().PrimaryKey("PK_TableBasedChildEntity"))
                  .WithColumn("ViewEntityId",
                              col => col.AsInt32()
                                        .NotNullable()
                                        .Indexed("IX_TableBasedChildEntity_ViewEntityId"))
                ;
        }

        public override void Down() {
            if (Schema.Table("TableBasedChildEntity").Exists()) {
                Delete.Table("TableBasedChildEntity");
            }

            Execute.Sql(@"DROP VIEW ViewBasedEntities");

            if (Schema.Table("TableBasedEntity").Exists()) {
                Delete.Table("TableBasedEntity");
            }

            if (Schema.Table("AspNetRoleClaims").Exists()) {
                Delete.Table("AspNetRoleClaims");
            }

            if (Schema.Table("AspNetUserClaims").Exists()) {
                Delete.Table("AspNetUserClaims");
            }

            if (Schema.Table("AspNetUserLogins").Exists()) {
                Delete.Table("AspNetUserLogins");
            }

            if (Schema.Table("AspNetUserRoles").Exists()) {
                Delete.Table("AspNetUserRoles");
            }

            if (Schema.Table("AspNetUserTokens").Exists()) {
                Delete.Table("AspNetUserTokens");
            }

            if (Schema.Table("AspNetRoles").Exists()) {
                Delete.Table("AspNetRoles");
            }

            if (Schema.Table("AspNetUsers").Exists()) {
                Delete.Table("AspNetUsers");
            }
        }
    }
}
