#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Domain;
using Domain.Contracts;
using Domain.ValueObjects;
using FluentAssertions;
using NSubstitute;
using WebApi.IntegrationTests.Extensions;
using WebApi.IntegrationTests.Helpers;
using WebApi.Models;
using Xunit;

#endregion

namespace WebApi.IntegrationTests.Tests
{
    public class ClientApiTests : IClassFixture<ClientsWebApplicationFactory>
    {
        private readonly ClientsWebApplicationFactory _webApiFactory;

        public ClientApiTests(ClientsWebApplicationFactory webApiFactory)
        {
            _webApiFactory = webApiFactory;
        }

        [Fact]
        public async Task Can_create_new_client()
        {
            // Arrange
            HttpClient clientApi = _webApiFactory.CreateDefaultClient(new Uri(ClientsApiPath.BaseUrl));

            var clientName = "Gordon Lightfoot";

            // Act
            var httpResponse = await clientApi.CreateClientAsync(clientName);

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var clientDto = await httpResponse.Content.ReadFromJsonAsync<ClientResponse>();

            clientDto!.Name.Should().Be(clientName);
        }
        
        [Fact]
        public async Task Can_add_subscribers_to_client()
        {
            // Arrange
            var client = await CreateClientAsync("Eric Clapton");
            var subscriber1 = await CreateClientAsync("Fleetwood mac");
            var subscriber2 = await CreateClientAsync("Jim Croce");

            var clientSeeder = new ClientSeeder(client, subscriber1, subscriber2);

            
            HttpClient clientApi = _webApiFactory
                .WithPredefinedData(clientSeeder)
                .CreateDefaultClient(new Uri(ClientsApiPath.BaseUrl));

            // Act
            var addFirstSubscriber = await clientApi.AddClientSubscriberAsync(client.Id, subscriber1.Id);
            var addSecondSubscriber = await clientApi.AddClientSubscriberAsync(client.Id, subscriber2.Id);

            // Assert
            addFirstSubscriber.StatusCode.Should()
                .Be(HttpStatusCode.OK);
            
            addSecondSubscriber.StatusCode.Should()
                .Be(HttpStatusCode.OK);
            
            var response = await clientApi.GetClientSubscriberAsync(client.Id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var subscribersResponse = await response.Content.ReadFromJsonAsync<ClientsResponse>();

            var clientSubscribers = subscribersResponse.Items;

            clientSubscribers
                .Should()
                .HaveCount(2);

            clientSubscribers.Should()
                .Satisfy
                (
                    firstSubscriber => firstSubscriber.Name == "Fleetwood mac",
                    secondSubscriber => secondSubscriber.Name == "Jim Croce"
                );
        }

        // TODO: Add additional tests to get top popular clients

        private static async Task<Client> CreateClientAsync(string name)
        {
            var clientCounter = Substitute.For<IClientCounter>();

            var clientName = new ClientName(name);

            clientCounter
                .CountByNameAsync(clientName)
                .Returns(0);

            var client = await Client.CreateWithNameAsync(clientName, clientCounter);
            return client;
        }
    }
}