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
        public void Can_create_client()
        {
            // Arrange
            var clientName = new ClientName("Firstname Lastname");
            
            // Act
            var client = Client.CreateWithName(clientName);

            // Assert
            var domainEvent = client.ShouldHavePublishedDomainEvent<ClientCreatedDomainEvent>();

            domainEvent.ClientName.Should()
                .Be(clientName);

            domainEvent.ClientId.Should()
                .Be(client.Id);
        }
    }
}