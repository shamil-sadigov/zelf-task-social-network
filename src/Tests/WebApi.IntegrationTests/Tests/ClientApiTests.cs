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
            httpResponse.ShouldBe201();

            var clientDto = await httpResponse.Content.ReadFromJsonAsync<ClientResponse>();

            clientDto!.Name.Should().Be(clientName);
        }
        
        [Fact]
        public async Task Can_add_subscribers_to_client()
        {
            // Arrange
            var client = CreateClient("Eric Clapton");
            var subscriber1 = CreateClient("Fleetwood mac");
            var subscriber2 = CreateClient("Jim Croce");

            var clientSeeder = new ClientSeeder(client, subscriber1, subscriber2);
            
            HttpClient clientApi = _webApiFactory
                .WithSeededData(clientSeeder)
                .CreateDefaultClient(new Uri(ClientsApiPath.BaseUrl));

            // Act
            var addFirstSubscriberResponse = await clientApi.AddClientSubscriberAsync(client.Id, subscriber1.Id);
            var addSecondSubscriberResponse = await clientApi.AddClientSubscriberAsync(client.Id, subscriber2.Id);

            // Assert
            addFirstSubscriberResponse.ShouldBe200();
            addSecondSubscriberResponse.ShouldBe200();
            
            var response = await clientApi.GetClientSubscribersAsync(client.Id);

            response.ShouldBe200();

            var subscribersResponse = await response.Content.ReadFromJsonAsync<ClientsResponse>();

            var clientSubscribers = subscribersResponse.Items;

            clientSubscribers
                .Should()
                .HaveCount(2)
                .And
                .Satisfy
                (
                    firstSubscriber => firstSubscriber.Name == "Fleetwood mac",
                    secondSubscriber => secondSubscriber.Name == "Jim Croce"
                );
        }

        // TODO: Add additional tests to get top popular clients

        private static Client CreateClient(string name)
        {
            var clientName = new ClientName(name);

            var client = Client.CreateWithName(clientName);
            return client;
        }
    }
}