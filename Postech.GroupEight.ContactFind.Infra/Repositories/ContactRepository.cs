using MongoDB.Driver;
using Postech.GroupEight.ContactFind.Core.Entities;
using Postech.GroupEight.ContactFind.Core.Interfaces.Repositories;

namespace Postech.GroupEight.ContactFind.Infra.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IMongoDatabase _database;

        public ContactRepository(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("contacts");
        }

        public IEnumerable<ContactEntity> GetContactsByAreaCode(string areaCode)
        {
            string collectionName = $"contacts_{areaCode}";
            var contactCollection = _database.GetCollection<ContactEntity>(collectionName);
            return contactCollection.Find(_ => true).ToEnumerable();
        }
    }
}
