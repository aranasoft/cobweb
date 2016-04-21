using System;
using FluentMigrator.Builders.Alter.Table;
using FluentMigrator.Builders.Create.Table;

namespace Cobweb.Data.FluentMigrator {
    public static class MigrationExtensions {
        public static ICreateTableWithColumnSyntax WithColumn(
            this ICreateTableWithColumnSyntax columnSyntax,
            string columnName,
            Func<ICreateTableColumnAsTypeSyntax, ICreateTableWithColumnSyntax> columnOptions
            ) {
            var column = columnSyntax.WithColumn(columnName);
            return columnOptions.Invoke(column);
        }

        public static IAlterTableAddColumnOrAlterColumnSyntax AddColumn(
            this IAlterTableAddColumnOrAlterColumnSyntax columnSyntax,
            string columnName,
            Func<IAlterTableColumnAsTypeSyntax, IAlterTableAddColumnOrAlterColumnSyntax> columnOptions
            ) {
            var column = columnSyntax.AddColumn(columnName);
            return columnOptions.Invoke(column);
        }

        public static IAlterTableAddColumnOrAlterColumnSyntax AlterColumn(
            this IAlterTableAddColumnOrAlterColumnSyntax columnSyntax,
            string columnName,
            Func<IAlterTableColumnAsTypeSyntax, IAlterTableAddColumnOrAlterColumnSyntax> columnOptions
            ) {
            var column = columnSyntax.AlterColumn(columnName);
            return columnOptions.Invoke(column);
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax AsStringMax(this ICreateTableColumnAsTypeSyntax column) {
            return column.AsString(10000);
        }

        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AsStringMax(
            this IAlterTableColumnAsTypeSyntax column
            ) {
            return column.AsString(10000);
        }
    }
}
