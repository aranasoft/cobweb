namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation;
internal static class StringExtensions {
    /// <summary>
    /// Trims the parentheses from the start and end of a string.
    /// </summary>
    /// <param name="input">The string to trim.</param>
    /// <returns>The trimmed string.</returns>
    /// <remarks>
    /// This method removes the parentheses from the start and end of a string.
    /// If the string is null or empty, or does not start and end with parentheses,
    /// the original string is returned. If the string starts and ends with multiple
    /// parentheses, all of them are removed.
    /// </remarks>
    internal static string TrimParentheses(this string input) {
        while (true) {
            if (string.IsNullOrEmpty(input)) {
                return input;
            }

            if (!input.StartsWith("(") || !input.EndsWith(")")) {
                return input;
            }

            input = input.Substring(1, input.Length - 2);
        }
    }
}
