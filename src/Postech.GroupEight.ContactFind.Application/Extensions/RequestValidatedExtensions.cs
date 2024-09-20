using MediatR;
using Postech.GroupEight.ContactFind.Application.Commands.Outputs;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for validating request objects.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class RequestValidatedExtensions
    {
        /// <summary>
        /// Validates the specified request object and returns a collection of validation results.
        /// </summary>
        /// <typeparam name="T">The type of the request object.</typeparam>
        /// <param name="obj">The request object to validate.</param>
        /// <returns>A collection of validation results.</returns>
        public static IEnumerable<ValidationResult> Validate<T>(this IRequest<DefaultOutput<T>> obj)
        {
            List<ValidationResult> validationResult = [];
            ValidationContext validationContext = new(obj, null, null);
            Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return validationResult;
        }

        /// <summary>
        /// Validates the specified request object and returns a collection of validation results.
        /// </summary>
        /// <param name="obj">The request object to validate.</param>
        /// <returns>A collection of validation results.</returns>
        public static IEnumerable<ValidationResult> Validate(this IRequest<DefaultOutput> obj)
        {
            List<ValidationResult> validationResult = [];
            ValidationContext validationContext = new(obj, null, null);
            Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return validationResult;
        }
    }
}
