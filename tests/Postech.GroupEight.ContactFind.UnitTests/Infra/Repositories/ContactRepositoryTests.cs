using MongoDB.Driver;
using Moq;
using Postech.GroupEight.ContactFind.Core.Entities;
using Postech.GroupEight.ContactFind.Infra.Repositories;

namespace Postech.GroupEight.ContactFind.UnitTests.Infra.Repositories
{
    public class ContactRepositoryTests
    {
        private readonly Mock<IMongoClient> _mongoClientMock;
        private readonly Mock<IMongoDatabase> _mongoDatabaseMock;
        private readonly Mock<IMongoCollection<ContactEntity>> _mongoCollectionMock;
        private readonly ContactRepository _contactRepository;

        public ContactRepositoryTests()
        {
            _mongoClientMock = new Mock<IMongoClient>();
            _mongoDatabaseMock = new Mock<IMongoDatabase>();
            _mongoCollectionMock = new Mock<IMongoCollection<ContactEntity>>();

            // Configurar o mock do MongoDB para retornar a instância de _mongoDatabaseMock ao chamar GetDatabase
            _mongoClientMock.Setup(client => client.GetDatabase("contacts", null))
                            .Returns(_mongoDatabaseMock.Object);

            // Configurar o mock do MongoDatabase para retornar a instância de _mongoCollectionMock ao chamar GetCollection
            _mongoDatabaseMock.Setup(db => db.GetCollection<ContactEntity>(It.IsAny<string>(), null))
                              .Returns(_mongoCollectionMock.Object);

            // Criar instância do repositório usando o mock do IMongoClient
            _contactRepository = new ContactRepository(_mongoClientMock.Object);
        }

        [Fact]
        public void GetContactsByAreaCode_ShouldReturnEmptyList_WhenNoContactsExist()
        {
            // Arrange
            var areaCode = "99";
            var collectionName = $"contacts_{areaCode}";

            // Criar um cursor de mock que não retorna contatos
            var mockCursor = new Mock<IAsyncCursor<ContactEntity>>();
            mockCursor.SetupSequence(cursor => cursor.MoveNext(It.IsAny<System.Threading.CancellationToken>()))
                      .Returns(false);
            mockCursor.Setup(cursor => cursor.Current).Returns(new List<ContactEntity>());

            // Configurar o método Find do mock da coleção para retornar o cursor mock
            _mongoCollectionMock.Setup(collection => collection.FindSync(It.IsAny<FilterDefinition<ContactEntity>>(),
                                                                         It.IsAny<FindOptions<ContactEntity, ContactEntity>>(),
                                                                         default))
                                .Returns(mockCursor.Object);

            // Act
            var result = _contactRepository.GetContactsByAreaCode(areaCode);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}