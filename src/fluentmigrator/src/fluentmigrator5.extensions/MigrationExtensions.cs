using System;
using FluentMigrator.Builders.Alter.Table;
using FluentMigrator.Builders.Create.Index;
using FluentMigrator.Builders.Create.Table;

namespace Aranasoft.Cobweb.FluentMigrator.Extensions {
    /// <summary>
    /// Provides extension methods for FluentMigrator to simplify migration operations.
    /// </summary>
    public static class MigrationExtensions {
        /// <summary>
        /// Adds a column to a table with specified options.
        /// </summary>
        /// <param name="columnSyntax">The column syntax to extend.</param>
        /// <param name="columnName">The name of the column to add.</param>
        /// <param name="columnOptions">A function to specify additional column options.</param>
        /// <returns>The updated column syntax.</returns>
        public static ICreateTableWithColumnSyntax WithColumn(
            this ICreateTableWithColumnSyntax columnSyntax,
            string columnName,
            Func<ICreateTableColumnAsTypeSyntax, ICreateTableWithColumnSyntax> columnOptions
        ) {
            var column = columnSyntax.WithColumn(columnName);
            return columnOptions.Invoke(column);
        }

        /// <summary>
        /// Adds a column to an existing table with specified options.
        /// </summary>
        /// <param name="columnSyntax">The column syntax to extend.</param>
        /// <param name="columnName">The name of the column to add.</param>
        /// <param name="columnOptions">A function to specify additional column options.</param>
        /// <returns>The updated column syntax.</returns>
        public static IAlterTableAddColumnOrAlterColumnSyntax AddColumn(
            this IAlterTableAddColumnOrAlterColumnSyntax columnSyntax,
            string columnName,
            Func<IAlterTableColumnAsTypeSyntax, IAlterTableAddColumnOrAlterColumnSyntax> columnOptions
        ) {
            var column = columnSyntax.AddColumn(columnName);
            return columnOptions.Invoke(column);
        }

        /// <summary>
        /// Alters an existing column in a table with specified options.
        /// </summary>
        /// <param name="columnSyntax">The column syntax to extend.</param>
        /// <param name="columnName">The name of the column to alter.</param>
        /// <param name="columnOptions">A function to specify additional column options.</param>
        /// <returns>The updated column syntax.</returns>
        public static IAlterTableAddColumnOrAlterColumnSyntax AlterColumn(
            this IAlterTableAddColumnOrAlterColumnSyntax columnSyntax,
            string columnName,
            Func<IAlterTableColumnAsTypeSyntax, IAlterTableAddColumnOrAlterColumnSyntax> columnOptions
        ) {
            var column = columnSyntax.AlterColumn(columnName);
            return columnOptions.Invoke(column);
        }

        /// <summary>
        /// Sets the column type to a string with a maximum length of 10000 characters.
        /// </summary>
        /// <param name="column">The column syntax to extend.</param>
        /// <returns>The updated column syntax.</returns>
        public static ICreateTableColumnOptionOrWithColumnSyntax AsStringMax(
            this ICreateTableColumnAsTypeSyntax column
        ) {
            return column.AsString(10000);
        }

        /// <summary>
        /// Sets the column type to a string with a maximum length of 10000 characters.
        /// </summary>
        /// <param name="column">The column syntax to extend.</param>
        /// <returns>The updated column syntax.</returns>
        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AsStringMax(
            this IAlterTableColumnAsTypeSyntax column
        ) {
            return column.AsString(10000);
        }

        /// <summary>
        /// Adds an index on a specified column with additional options.
        /// </summary>
        /// <param name="columnSyntax">The column syntax to extend.</param>
        /// <param name="columnName">The name of the column to index.</param>
        /// <param name="columnOptions">A function to specify additional index options.</param>
        /// <returns>The updated column syntax.</returns>
        public static ICreateIndexOnColumnSyntax OnColumn(
            this ICreateIndexOnColumnSyntax columnSyntax,
            string columnName,
            Func<ICreateIndexColumnOptionsSyntax, ICreateIndexOnColumnSyntax> columnOptions
        ) {
            var column = columnSyntax.OnColumn(columnName);
            return columnOptions.Invoke(column);
        }
    }
}
