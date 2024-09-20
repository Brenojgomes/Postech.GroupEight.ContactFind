using FluentAssertions;
using Moq;
using Postech.GroupEight.ContactFind.Application.Commands.Inputs;
using Postech.GroupEight.ContactFind.Application.Commands.Outputs;
using Postech.GroupEight.ContactFind.Application.Handlers.Contacts;
using Postech.GroupEight.ContactFind.Application.ViewModels;
using Postech.GroupEight.ContactFind.Core.Entities;
using Postech.GroupEight.ContactFind.Core.Exceptions.Common;
using Postech.GroupEight.ContactFind.Core.Interfaces.Repositories;
using Postech.GroupEight.ContactFind.UnitTests.Fakers.Entities;

namespace Postech.GroupEight.ContactFind.UnitTests.Application.Handlers
{
    public class FindContactByAreaCodeHandlerTests
    {
        [Theory(DisplayName = "Fetching list of contacts by area code.")]
        [InlineData("11")]
        [InlineData("31")]
        [InlineData("81")]
        [Trait("Action", "Handle")]
        public async Task Handle_FetchingExistentContactsByAreaCode_ShouldReturnContactsByAreaCode(string areaCode)
        {
            // Arrange
            List<ContactEntity> contacts = new ContactEntityFaker(areaCode).Generate(10);
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetContactsByAreaCode(areaCode)).Returns(contacts);
            FindContactByAreaCodeHandler handler = new(contactRepository.Object);
            FindContactInput input = new() { AreaCodeValue = areaCode };

            // Act
            DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>> output = await handler.Handle(input, CancellationToken.None);

            // Assert
            output.Success.Should().Be(true);
            output.Data.Should().NotBeNull();
            output.Data.As<IEnumerable<FindContactByAreaCodeViewModel>>().Should().HaveSameCount(contacts);
        }

        [Theory(DisplayName = "Fetching list of contacts by non-existent area code.")]
        [InlineData("99")]
        [InlineData("00")]
        [InlineData("123")]
        [Trait("Action", "Handle")]
        public async Task Handle_NoContactsRegisteredForTheRequestedAreaCode_ShouldThrowNotFoundException(string areaCode)
        {
            // Arrange
            Mock<IContactRepository> contactRepository = new();
            contactRepository.Setup(c => c.GetContactsByAreaCode(areaCode)).Returns(Enumerable.Empty<ContactEntity>());
            FindContactByAreaCodeHandler handler = new(contactRepository.Object);
            FindContactInput input = new() { AreaCodeValue = areaCode };

            // Act & Assert
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(input, CancellationToken.None));
            exception.Message.Should().NotBeNullOrEmpty();
        }

    }
}
