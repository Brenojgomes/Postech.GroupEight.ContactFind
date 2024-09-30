using Postech.GroupEight.ContactFind.Application.ViewModels;
using Postech.GroupEight.ContactFind.Core.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Application.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ContactEntityExtensions
    {
        /// <summary>
        /// Converts a collection of ContactEntity objects to a collection of FindContactByAreaCodeViewModel objects.
        /// </summary>
        /// <param name="contacts">The collection of ContactEntity objects.</param>
        /// <returns>A collection of FindContactByAreaCodeViewModel objects.</returns>
        public static IEnumerable<FindContactByAreaCodeViewModel> AsFindContactByAreaCodeViewModel(this IEnumerable<ContactEntity> contacts)
        {
            return contacts.Select(contact => new FindContactByAreaCodeViewModel()
            {
                Id = contact.Id,
                ContactEmail = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                AreaCode = contact.AreaCode,
                Number = contact.Number,
                Active = contact.Active,
                CreatedAt = contact.CreatedAt,
                ModifiedAt = contact.ModifiedAt
            });
        }
    }
}