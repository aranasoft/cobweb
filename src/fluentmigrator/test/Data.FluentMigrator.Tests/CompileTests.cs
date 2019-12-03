using FluentMigrator;

namespace Cobweb.Data.FluentMigrator.Tests {
    /// <summary>
    ///     These tests are not executed by NUnit. They are here for compile-time checking. 'If it builds, ship it.'
    /// </summary>
    public class CompileTests {
        internal class SampleMigration : Migration {
            private void AlterTableUsingOldAddAndNewAdd() {
                Alter.Table("Sample")
                     .AddColumn("OldAddSyntax")
                     .AsInt32()
                     .NotNullable()
                     .WithDefaultValue(7)
                     .AddColumn("NewAddSyntax", col => col.AsInt32());
            }

            private void AlterTableUsingNewAddAndOldAdd() {
                Alter.Table("Sample")
                     .AddColumn("NewAddSyntax", col => col.AsInt32())
                     .AddColumn("OldAddSyntax")
                     .AsInt32()
                     .NotNullable()
                     .WithDefaultValue(7);
            }

            private void AlterTableUsingNewAddAndNewAdd() {
                Alter.Table("Sample")
                     .AddColumn("NewAddSyntax", col => col.AsInt32())
                     .AddColumn("NewAddSyntax", col => col.AsInt32());
            }

            private void AlterTableUsingOldAlterAndNewAlter() {
                Alter.Table("Sample")
                     .AlterColumn("OldAlterSyntax")
                     .AsInt32()
                     .NotNullable()
                     .WithDefaultValue(7)
                     .AlterColumn("NewAlterSyntax", col => col.AsInt32());
            }


            private void AlterTableUsingNewAlterAndOldAlter() {
                Alter.Table("Sample")
                     .AlterColumn("NewAlterSyntax", col => col.AsInt32())
                     .AlterColumn("OldAlterSyntax")
                     .AsInt32()
                     .NotNullable()
                     .WithDefaultValue(7);
            }

            private void AlterTableUsingNewAlterAndNewAlter() {
                Alter.Table("Sample")
                     .AlterColumn("NewAlterSyntax", col => col.AsInt32())
                     .AlterColumn("NewAlterSyntax", col => col.AsInt32());
            }

            private void AlterTableUsingOldAddAndNewAlter() {
                Alter.Table("Sample")
                     .AddColumn("OldAddSyntax")
                     .AsInt32()
                     .NotNullable()
                     .WithDefaultValue(7)
                     .AlterColumn("NewAlterSyntax", col => col.AsInt32());
            }

            private void AlterTableUsingNewAddAndOldAlter() {
                Alter.Table("Sample")
                     .AddColumn("NewAddSyntax", col => col.AsInt32())
                     .AlterColumn("OldAlterSyntax")
                     .AsInt32()
                     .NotNullable()
                     .WithDefaultValue(7);
            }

            private void AlterTableUsingNewAddAndNewAlter() {
                Alter.Table("Sample")
                     .AddColumn("NewAddSyntax", col => col.AsInt32())
                     .AlterColumn("NewAlterSyntax", col => col.AsInt32());
            }

            private void AlterTableUsingOldAlterAndNewAdd() {
                Alter.Table("Sample")
                     .AlterColumn("OldAlterSyntax")
                     .AsInt32()
                     .NotNullable()
                     .WithDefaultValue(7)
                     .AddColumn("NewAddSyntax", col => col.AsInt32());
            }

            private void AlterTableUsingNewAlterAndOldAdd() {
                Alter.Table("Sample")
                     .AlterColumn("NewAlterSyntax", col => col.AsInt32())
                     .AddColumn("OldAddSyntax")
                     .AsInt32()
                     .NotNullable()
                     .WithDefaultValue(7);
            }

            private void AlterTableUsingNewAlterAndNewAdd() {
                Alter.Table("Sample")
                     .AlterColumn("NewAlterSyntax", col => col.AsInt32())
                     .AddColumn("NewAddSyntax", col => col.AsInt32());
            }

            private void CreateTableUsingOldWithAndNewWith() {
                Create.Table("Sample")
                      .WithColumn("OldSyntax")
                      .AsBoolean()
                      .WithColumn("NewSyntax", col => col.AsInt32().NotNullable().WithDefaultValue(7));
            }

            private void CreateTableUsingNewWithAndOldWith() {
                Create.Table("Sample")
                      .WithColumn("NewSyntax", col => col.AsInt32().NotNullable().WithDefaultValue(7))
                      .WithColumn("OldSyntax")
                      .AsBoolean();
            }

            private void CreateTableUsingNewWithAndNewWith() {
                Create.Table("Sample")
                      .WithColumn("NewSyntax", col => col.AsInt32().NotNullable().WithDefaultValue(7))
                      .WithColumn("NewSyntax", col => col.AsBoolean().Nullable());
            }

            private void CreateIndexUsingOldOnAndNewOn() {
                Create.Index("Sample").OnTable("Table")
                      .OnColumn("NewSyntax")
                      .Ascending()
                      .OnColumn("NewSyntax", col => col.Descending());
            }

            private void CreateIndexUsingNewOnAndOldOn() {
                Create.Index("Sample").OnTable("Table")
                      .OnColumn("NewSyntax", col => col.Ascending())
                      .OnColumn("NewSyntax")
                      .Descending();
            }

            private void CreateIndexUsingNewOnAndNewOn() {
                Create.Index("Sample").OnTable("Table")
                      .OnColumn("NewSyntax", col => col.Ascending())
                      .OnColumn("NewSyntax", col => col.Descending());
            }

            public override void Up() {}
            public override void Down() {}
        }
    }
}
