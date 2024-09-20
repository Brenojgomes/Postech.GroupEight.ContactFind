using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Core.Exceptions.Common
{
    /// <summary>
    /// Represents a domain exception.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DomainException(string message) : Exception(message)
    {
        /// <summary>
        /// Throws a domain exception when a specific rule is invalid.
        /// </summary>
        /// <param name="invalidRule">The condition to check.</param>
        /// <param name="message">The error message.</param>
        public static void ThrowWhen(bool invalidRule, string message)
        {
            if (invalidRule)
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// Throws a domain exception when there are error messages from validation.
        /// </summary>
        /// <param name="validationResults">The collection of validation results.</param>
        public static void ThrowWhenThereAreErrorMessages(IEnumerable<ValidationResult> validationResults)
        {
            if (validationResults.Any())
            {
                throw new DomainException(validationResults.ElementAt(0).ErrorMessage);
            }
        }
    }
}
