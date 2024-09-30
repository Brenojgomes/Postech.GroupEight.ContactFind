using Postech.GroupEight.ContactFind.Core.Entities;

namespace Postech.GroupEight.ContactFind.Core.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository for managing contacts.
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// Retrieves contacts based on the specified area code.
        /// </summary>
        /// <param name="areaCode">The area code to filter contacts.</param>
        /// <returns>An enumerable of contacts.</returns>
        Task<IEnumerable<ContactEntity>> GetContactsByAreaCode(string areaCode);
    }
}