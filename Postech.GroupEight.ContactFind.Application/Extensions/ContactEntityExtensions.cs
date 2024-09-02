using Postech.GroupEight.ContactFind.Application.ViewModels;
using Postech.GroupEight.ContactFind.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.GroupEight.ContactFind.Application.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ContactEntityExtensions
    {
        public static IEnumerable<FindContactByAreaCodeViewModel> AsFindContactByAreaCodeViewModel(this IEnumerable<ContactEntity> contacts)
        {
            return contacts.Select(contact => new FindContactByAreaCodeViewModel()
            {
                ContactId = contact.Id,
                ContactEmail = contact.Email,
                ContactFirstName = contact.FirstName,
                ContactLastName = contact.LastName,
                ContactPhoneAreaCode = contact.AreaCode,
                ContactPhoneNumber = contact.Number
            });
        }
    }
}
