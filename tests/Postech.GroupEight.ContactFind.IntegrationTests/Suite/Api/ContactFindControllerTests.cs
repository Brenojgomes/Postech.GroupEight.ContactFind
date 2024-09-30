using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Postech.GroupEight.ContactFind.Application.Commands.Outputs;
using Postech.GroupEight.ContactFind.Application.ViewModels;
using Postech.GroupEight.ContactFind.Core.Entities;
using Postech.GroupEight.ContactFind.Core.Interfaces.Repositories;
using System.Net;
using System.Text.Json;

namespace Postech.GroupEight.ContactFind.IntegrationTests.Suite.Api
{
    public class ContactFindControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<Program> _factory = factory;
        private readonly Faker _faker = new();
        private HttpClient _client;

        public Task InitializeAsync()
        {
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var areaCode = _faker.Address.ZipCode("31");
                    var contactEntity = new ContactEntity
                    {
                        Id = _faker.Random.Guid(),
                        CreatedAt = _faker.Date.Past(),
                        ModifiedAt = _faker.Date.Recent(),
                        Active = _faker.Random.Bool(),
                        AreaCode = areaCode,
                        Number = _faker.Phone.PhoneNumber("9########"),
                        FirstName = _faker.Name.FirstName(),
                        LastName = _faker.Name.LastName(),
                        Email = _faker.Internet.Email()
                    };

                    var contacts = new List<ContactEntity> { contactEntity };
                    var mockContactRepository = new Mock<IContactRepository>();
                    mockContactRepository.Setup(repo => repo.GetContactsByAreaCode(areaCode)).ReturnsAsync(contacts);

                    services.AddSingleton(mockContactRepository.Object);
                });
            }).CreateClient();
            return Task.CompletedTask;
        }

        [Fact]
        [Trait("Action", "Contacts")]
        public async Task FindContactEndpoint_FetchAnNonExistingContact_ShouldNotFindContact()
        {
            // Arrange
            var areaCode = _faker.Address.ZipCode("##");

            // Act
            using HttpResponseMessage responseMessage = await _client.GetAsync($"/contacts?areaCodeValue={areaCode}");

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var responseMessageContent = JsonSerializer.Deserialize<DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>>(await responseMessage.Content.ReadAsStringAsync());
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Success.Should().BeFalse();
            responseMessageContent?.Message.Equals("No contacts found for the area code provided");
        }

        [Fact]
        [Trait("Action", "Contacts")]
        public async Task FindContactEndpoint_FetchAnExistingContact_ShouldFindContact()
        {
            // Arrange
            var areaCode = _faker.Address.ZipCode("31");

            // Act
            using HttpResponseMessage responseMessage = await _client.GetAsync($"/contacts?areaCodeValue={areaCode}");

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseMessageContent = JsonSerializer.Deserialize<DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>>(await responseMessage.Content.ReadAsStringAsync());
            responseMessageContent.Should().NotBeNull();
            responseMessageContent?.Success.Should().BeTrue();
            responseMessageContent?.Data.Should().HaveCountGreaterThan(0);
        }

        public Task DisposeAsync()
        {
            _client?.Dispose();
            return Task.CompletedTask;
        }
    }
}