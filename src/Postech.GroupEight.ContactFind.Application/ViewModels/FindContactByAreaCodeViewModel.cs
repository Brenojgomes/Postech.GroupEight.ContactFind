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

        /// <summary>
        /// The creation date and time of the entity.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The last modification date and time of the entity.
        /// </summary>
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// A value indicating whether the entity is active.
        /// </summary>
        public bool Active { get; set; }
    }
}
