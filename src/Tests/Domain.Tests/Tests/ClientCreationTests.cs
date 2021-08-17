#region

using System;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.DomainEvents;
using Domain.Exceptions;
using Domain.Tests.Helpers;
using Domain.ValueObjects;
using FluentAssertions;
using NSubstitute;
using Xunit;

#endregion

namespace Domain.Tests.Tests
{
    public class ClientCreationTests
    {
        [Fact]
        public void Cannot_create_client_when_clientName_is_not_unique()
        {
            // Arrange
            var clientName = new ClientName("Firstname Lastname");

            var clientCounter = Substitute.For<IClientCounter>();
            clientCounter.CountByNameAsync(clientName).Returns(1);

            // Act
            Func<Task> clientCreation = async () =>
            {
                var client = await Client.CreateWithNameAsync(clientName, clientCounter);
            };

            // Assert
            clientCreation.Should()
                .ThrowAsync<DuplicateClientNameException>();
        }

        [Fact]
        public async Task Can_create_client_when_clientName_is_unique()
        {
            // Arrange
            var clientName = new ClientName("Firstname Lastname");

            var clientCounter = Substitute.For<IClientCounter>();
            clientCounter.CountByNameAsync(clientName).Returns(0);

            // Act
            var client = await Client.CreateWithNameAsync(clientName, clientCounter);

            // Assert
            var domainEvent = client.ShouldHavePublishedDomainEvent<ClientCreatedDomainEvent>();

            domainEvent.ClientName.Should()
                .Be(clientName);

            domainEvent.ClientId.Should()
                .Be(client.Id);
        }
    }
}