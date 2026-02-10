using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation;
/// <summary>
/// Represents errors that occur during schema validation.
/// </summary>
/// <remarks>
/// This exception is thrown when there are validation errors in the schema.
/// </remarks>
public class SchemaValidationException : Exception {
    /// <summary>
    /// Gets the validation errors.
    /// </summary>
    /// <value>
    /// The validation errors as a read-only collection of strings.
    /// </value>
    public ReadOnlyCollection<string> ValidationErrors { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SchemaValidationException"/> class with a specified error message and a list of validation errors.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="validationErrors">The list of validation errors.</param>
    public SchemaValidationException(string message, IList<string> validationErrors) : base(message) {
        ValidationErrors = new ReadOnlyCollection<string>(validationErrors);
    }
}
