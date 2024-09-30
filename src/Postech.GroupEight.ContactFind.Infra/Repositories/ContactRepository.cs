using MongoDB.Driver;
using Postech.GroupEight.ContactFind.Core.Entities;
using Postech.GroupEight.ContactFind.Core.Interfaces.Repositories;

namespace Postech.GroupEight.ContactFind.Infra.Repositories
{
    /// <summary>
    /// Represents a repository for managing contacts.
    /// </summary>
    public class ContactRepository(IMongoClient mongoClient) : IContactRepository
    {
        /// <summary>
        /// The MongoDB client.
        /// </summary>
        private readonly IMongoDatabase _database = mongoClient.GetDatabase("contacts");

        /// <summary>
        /// Gets the contacts by area code.
        /// </summary>
        /// <param name="areaCode">The area code.</param>
        /// <returns>The contacts with the specified area code.</returns>
        public async Task<IEnumerable<ContactEntity>> GetContactsByAreaCode(string areaCode)
        {
            try
            {
                string collectionName = $"contacts_{areaCode}";
                IMongoCollection<ContactEntity> contactCollection = _database.GetCollection<ContactEntity>(collectionName);
                IAsyncCursor<ContactEntity> queryResult = await contactCollection.FindAsync(contact => contact.Active);
                return await queryResult.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error when querying contacts for the area code {areaCode}: {ex.Message}", ex);
            }
        }
    }
}