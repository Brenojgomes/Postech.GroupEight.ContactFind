using MongoDB.Driver;
using Postech.GroupEight.ContactFind.Core.Entities;
using Postech.GroupEight.ContactFind.Core.Interfaces.Repositories;

namespace Postech.GroupEight.ContactFind.Infra.Repositories
{
    /// <summary>
    /// Represents a repository for managing contacts.
    /// </summary>
    public class ContactRepository : IContactRepository
    {
        /// <summary>
        /// The MongoDB client.
        /// </summary>
        private readonly IMongoDatabase _database;

        public ContactRepository(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("contacts");
        }

        /// <summary>
        /// Gets the contacts by area code.
        /// </summary>
        /// <param name="areaCode">The area code.</param>
        /// <returns>The contacts with the specified area code.</returns>
        public IEnumerable<ContactEntity> GetContactsByAreaCode(string areaCode)
        {
            string collectionName = $"contacts_{areaCode}";
            var contactCollection = _database.GetCollection<ContactEntity>(collectionName);
            return contactCollection.Find(_ => true).ToEnumerable();
        }
    }
}
