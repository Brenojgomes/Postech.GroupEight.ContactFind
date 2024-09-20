using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Postech.GroupEight.ContactFind.Application.Commands.Inputs;
using Postech.GroupEight.ContactFind.Application.Commands.Outputs;
using Postech.GroupEight.ContactFind.Application.ViewModels;
using System.Net;
using System.Net.Http.Json;

namespace Postech.GroupEight.ContactFind.UnitTests.ContactFindApi.Contacts
{
    public class ContactsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IMediator> _mediatorMock;

        public ContactsTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task GetContacts_ReturnsContacts_WhenValidRequest()
        {
            // Arrange
            var request = new FindContactInput { AreaCodeValue = "31" };
            var contacts = new List<FindContactByAreaCodeViewModel>
            {
                new FindContactByAreaCodeViewModel { Id = Guid.NewGuid(), FirstName = "John Doe", AreaCode = "31" },
                new FindContactByAreaCodeViewModel { Id = Guid.NewGuid(), FirstName = "Jane Doe", AreaCode = "31" }
            };
            var expectedResponse = new DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>(true, "Contacts found", contacts);

            _mediatorMock.Setup(m => m.Send(It.IsAny<FindContactInput>(), default))
                .ReturnsAsync(expectedResponse);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(_mediatorMock.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync($"/contacts?AreaCodeValue={request.AreaCodeValue}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var actualResponse = await response.Content.ReadFromJsonAsync<DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>>();
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetContacts_ReturnsInternalServerError_WhenAreaCodeValueEmpty()
        {
            // Arrange
            var request = new FindContactInput { AreaCodeValue = "" }; // Invalid request

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/contacts?AreaCodeValue={request.AreaCodeValue}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetContacts_ReturnsNotFound_WhenNoContactsFound()
        {
            // Arrange
            var request = new FindContactInput { AreaCodeValue = "00" };
            var expectedResponse = new DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>(false, "No contacts found", null);

            _mediatorMock.Setup(m => m.Send(It.IsAny<FindContactInput>(), default))
                .ReturnsAsync(expectedResponse);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(_mediatorMock.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync($"/contacts?AreaCodeValue={request.AreaCodeValue}");

            // Assert
            var actualResponse = await response.Content.ReadFromJsonAsync<DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>>();
            actualResponse.Should().NotBeNull();
            actualResponse.Success.Should().Be(expectedResponse.Success);
            actualResponse.Message.Should().Be(expectedResponse.Message);
            actualResponse.Data.Should().BeNull();
        }
    }
}
