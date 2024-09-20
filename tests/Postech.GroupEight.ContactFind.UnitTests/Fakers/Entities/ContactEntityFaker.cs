using Bogus;
using Postech.GroupEight.ContactFind.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.GroupEight.ContactFind.UnitTests.Fakers.Entities
{
    internal class ContactEntityFaker : Faker<ContactEntity>
    {
        public ContactEntityFaker(string areaCode = "11")
        {
            Locale = "pt_BR";
            CustomInstantiator(c => new ContactEntity
            {
                AreaCode = areaCode,
                FirstName = c.Name.FirstName(),
                LastName = c.Name.LastName(),
                Email = c.Internet.Email(),
                Number = c.Phone.PhoneNumber("9########"),
                Id = Guid.NewGuid()               
            });
        }
    }
}
