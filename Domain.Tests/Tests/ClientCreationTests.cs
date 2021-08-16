#region

using System;
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
            clientCounter.CountByName(clientName).Returns(1);

            // Act
            Action clientCreation = () =>
            {
                var client = Client.WithName(clientName, clientCounter);
            };

            // Assert
            clientCreation.Should()
                .Throw<DuplicateClientNameException>();
        }

        [Fact]
        public void Can_create_client_when_clientName_is_unique()
        {
            // Arrange
            var clientName = new ClientName("Firstname Lastname");

            var clientCounter = Substitute.For<IClientCounter>();
            clientCounter.CountByName(clientName).Returns(0);

            // Act
            var client = Client.WithName(clientName, clientCounter);

            // Assert
            var domainEvent = client.ShouldHavePublishedDomainEvent<ClientCreatedDomainEvent>();

            domainEvent.ClientName.Should()
                .Be(clientName);

            domainEvent.ClientId.Should()
                .Be(client.Id);
        }
    }
}