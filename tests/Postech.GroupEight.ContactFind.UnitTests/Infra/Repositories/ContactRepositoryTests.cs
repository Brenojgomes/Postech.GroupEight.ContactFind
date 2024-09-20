using MongoDB.Driver;
using Moq;
using Postech.GroupEight.ContactFind.Core.Entities;
using Postech.GroupEight.ContactFind.Infra.Repositories;
using Postech.GroupEight.ContactFind.UnitTests.Configuration;

namespace Postech.GroupEight.ContactFind.UnitTests.Infra.Repositories
{
    public class ContactRepositoryTests
    {
        private readonly Mock<IMongoClient> _mockMongoClient;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly Mock<IMongoCollection<ContactEntity>> _mockCollection;
        private readonly ContactRepository _repository;

        public ContactRepositoryTests()
        {
            _mockMongoClient = new Mock<IMongoClient>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _mockCollection = new Mock<IMongoCollection<ContactEntity>>();

            _mockMongoClient.Setup(client => client.GetDatabase(It.IsAny<string>(), null))
                .Returns(_mockDatabase.Object);

            _mockDatabase.Setup(db => db.GetCollection<ContactEntity>(It.IsAny<string>(), null))
                .Returns(_mockCollection.Object);

            _repository = new ContactRepository(_mockMongoClient.Object);
        }

        [Fact]
        public void GetContactsByAreaCode_ReturnsContacts()
        {
            // Arrange
            var areaCode = "31";
            var contacts = new List<ContactEntity>
            {
                new ContactEntity { Id = Guid.NewGuid(), FirstName = "John Doe", AreaCode = areaCode },
                new ContactEntity { Id = Guid.NewGuid(), FirstName = "Jane Doe", AreaCode = areaCode }
            };


            var fakeFindFluent = new FakeFindFluent<ContactEntity>(contacts);

            // Configurando o mock para retornar o fakeFindFluent
            _mockCollection.Setup(collection => collection.Find(It.IsAny<FilterDefinition<ContactEntity>>(), null))
                .Returns(fakeFindFluent);

            // Act
            var result = _repository.GetContactsByAreaCode(areaCode);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, contact => contact.FirstName == "John Doe");
            Assert.Contains(result, contact => contact.FirstName == "Jane Doe");
        }
    }
}
