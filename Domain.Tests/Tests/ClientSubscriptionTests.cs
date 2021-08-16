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
    public class ClientSubscriptionTests
    {
        [Fact]
        public void Can_subscribe_to_new_client()
        {
            // Arrange
            var client = CreateClient();
            var subscriber = CreateClient();
            
            var expectedClientPopularity = new ClientPopularity(1);
                
            // Act
            client.AddSubscriber(subscriber);

            // Assert
            var domainEvent = client.ShouldHavePublishedDomainEvent<ClientSubscribedDomainEvent>();

            domainEvent.ClientId.Should()
                .Be(client.Id);
            
            domainEvent.SubscriberId.Should()
                .Be(subscriber.Id);
            
            domainEvent.ClientPopularity.Should()
                .Be(expectedClientPopularity);
        }
        
        
        [Theory]
        [InlineData(5, 6)]
        [InlineData(10, 11)]
        [InlineData(150, 151)]
        public void Can_subscribe_to_client_who_already_has_several_subscribers(
            int numberOfSubscribers,
            uint expectedPopularityInt)
        {
            // Arrange
            var client = CreateClientWithSubscribers(numberOfSubscribers);
            
            var subscriber = CreateClient();

            var expectedClientPopularity = new ClientPopularity(expectedPopularityInt);
            
            // Act
            client.AddSubscriber(subscriber);

            // Assert
            var domainEvent = client.ShouldHavePublishedDomainEvent<ClientSubscribedDomainEvent>();

            domainEvent.ClientId.Should()
                .Be(client.Id);
            
            domainEvent.SubscriberId.Should()
                .Be(subscriber.Id);
            
            domainEvent.ClientPopularity.Should()
                .Be(expectedClientPopularity);
        }
        
        [Fact]
        public void Cannot_subscribe_to_client_when_subscriber_has_been_already_subscribed()
        {
            // Arrange
            var client = CreateClient();
            
            var subscriber = CreateClient();
            
            client.AddSubscriber(subscriber);
            
            // Act
            Action addingTheSameSubscriber = () => client.AddSubscriber(subscriber);

            addingTheSameSubscriber.Should()
                .Throw<DuplicateSubscriberException>();
        }

        #region Helpers

        private static Client CreateClientWithSubscribers(int subscriberCount)
        {
            var client = CreateClient();

            for (int i = 0; i < subscriberCount; i++)
            {
                var subscriber = CreateClient();
                
                client.AddSubscriber(subscriber);
            }
            
            client.ClearDomainEvents();

            return client;
        }
        
        private static Client CreateClient()
        {
            // Arrange
            var clientName = new ClientName("Firstname Lastname");

            var clientCounter = Substitute.For<IClientCounter>();
            clientCounter.CountByName(clientName).Returns(0);

            var client = Client.WithName(clientName, clientCounter);
            
            client.ClearDomainEvents();
            
            return client;
        }

        #endregion
    }
}