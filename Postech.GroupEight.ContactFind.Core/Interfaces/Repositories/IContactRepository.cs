using Postech.GroupEight.ContactFind.Core.Entities;

namespace Postech.GroupEight.ContactFind.Core.Interfaces.Repositories
{
    public interface IContactRepository 
    {
        IEnumerable<ContactEntity> GetContactsByAreaCode(string areaCode);
    }
}
