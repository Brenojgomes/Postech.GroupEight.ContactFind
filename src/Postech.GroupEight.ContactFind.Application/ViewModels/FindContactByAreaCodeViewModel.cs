using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Application.ViewModels
{
    /// <summary>
    /// Object that stores data returned from the find contact endpoint.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public record FindContactByAreaCodeViewModel
    {
        /// <summary>
        /// The identification of the contact.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// The first name of the contact.
        /// </summary>
        public string FirstName { get; init; }

        /// <summary>
        /// The last name of the contact.
        /// </summary>
        public string LastName { get; init; }

        /// <summary>
        /// The email address of the contact.
        /// </summary>
        public string ContactEmail { get; init; }

        /// <summary>
        /// The phone number of the contact.
        /// </summary>
        public string Number { get; init; }

        /// <summary>
        /// The area code phone number of the contact.
        /// </summary>
        public string AreaCode { get; init; }
    }
}
