﻿#region

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
    public class ClientSubscriptionTests
    {
        [Fact]
        public async Task Can_subscribe_to_new_client()
        {
            // Arrange
            var client = await CreateClientAsync();
            var subscriber = await CreateClientAsync();

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
        public async Task Can_subscribe_to_client_who_already_has_several_subscribers(
            int numberOfSubscribers,
            uint expectedPopularityInt)
        {
            // Arrange
            var client = await CreateClientWithSubscribers(numberOfSubscribers);

            var subscriber = await CreateClientAsync();

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
        public async Task Cannot_subscribe_to_client_when_subscriber_has_been_already_subscribed()
        {
            // Arrange
            var client = await CreateClientAsync();

            var subscriber = await CreateClientAsync();

            client.AddSubscriber(subscriber);

            // Act
            Action addingTheSameSubscriber = () => client.AddSubscriber(subscriber);

            // Assert
            addingTheSameSubscriber.Should()
                .Throw<DuplicateSubscriberException>();
        }
        
        
        [Fact]
        public async Task Cannot_subscribe_client_to_itself()
        {
            // Arrange
            var client = await CreateClientAsync();
            
            // Act
            Action addingTheSameSubscriber = () => client.AddSubscriber(client);

            // Assert
            addingTheSameSubscriber.Should()
                .Throw<InvalidSubscriberException>();
        }


        #region Helpers

        private static async Task<Client> CreateClientWithSubscribers(int subscriberCount)
        {
            var client = await CreateClientAsync();

            for (var i = 0; i < subscriberCount; i++)
            {
                var subscriber = await CreateClientAsync();

                client.AddSubscriber(subscriber);
            }

            client.ClearDomainEvents();

            return client;
        }

        private static async Task<Client> CreateClientAsync()
        {
            var clientName = new ClientName("Firstname Lastname");

            var clientCounter = Substitute.For<IClientCounter>();
            clientCounter.CountByNameAsync(clientName).Returns(0);

            var client = await Client.CreateWithNameAsync(clientName, clientCounter);

            client.ClearDomainEvents();

            return client;
        }

        #endregion
    }
}