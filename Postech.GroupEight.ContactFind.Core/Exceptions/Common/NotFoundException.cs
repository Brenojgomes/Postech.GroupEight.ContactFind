using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Core.Exceptions.Common
{
    /// <summary>
    /// Represents an exception that is thrown when a requested entity is not found.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NotFoundException(string message) : Exception(message)
    {
        /// <summary>
        /// Throws a NotFoundException if the specified entity is null.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <param name="errorMessage">The error message to include in the exception.</param>
        public static void ThrowWhenNullEntity(object? entity, string errorMessage)
        {
            if (entity is not null) return;
            throw new NotFoundException(errorMessage);
        }

        /// <summary>
        /// Throws a NotFoundException if the specified list is null or empty.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="errorMessage">The error message to include in the exception.</param>
        public static void ThrowWhenNullOrEmptyList(
            IEnumerable<object> list,
            string errorMessage)
        {
            if (list is not null && list.Any()) return;
            throw new NotFoundException(errorMessage);
        }
    }
}
