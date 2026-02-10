using Microsoft.EntityFrameworkCore;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation;
/// <summary>
/// Provides extension methods for schema validation.
/// </summary>
public static class SchemaValidatorExtensions {
    /// <summary>
    /// Validates the schema of a <see cref="DbContext"/>.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/> to validate.</param>
    /// <param name="validationOptions">The options for schema validation. If null, default options are used.</param>
    /// <remarks>
    /// This method creates a new instance of the <see cref="SchemaValidator"/> class and calls its ValidateSchema method.
    /// </remarks>
    public static void ValidateSchema(this DbContext context, SchemaValidationOptions validationOptions = null) {
        var validator = new SchemaValidator(context);
        validator.ValidateSchema(validationOptions ?? new SchemaValidationOptions());
    }
}
