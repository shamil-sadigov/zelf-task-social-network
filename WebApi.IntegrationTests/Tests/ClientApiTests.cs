#region

using System;
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
        public async Task POST_Creates_new_client()
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
        public async Task POST_Add_subscriber_to_client()
        {
            var client = await CreateClientAsync("Gordon Lightfoot");
            var subscriber = await CreateClientAsync("Fleetwood mac");

            var clientSeeder = new ClientSeeder(client, subscriber);

            // Arrange
            HttpClient clientApi = _webApiFactory
                .WithPredefinedData(clientSeeder)
                .CreateDefaultClient(new Uri(ClientsApiPath.BaseUrl));

            // Act
            HttpResponseMessage httpResponse = await clientApi.AddClientSubscriberAsync(client.Id, subscriber.Id);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
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