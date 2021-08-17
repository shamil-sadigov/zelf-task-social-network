#region

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.IntegrationTests.Helpers;

#endregion

namespace WebApi.IntegrationTests.Extensions
{

    public static class TestExtensions
    {
        public static void ShouldBe200(this HttpResponseMessage responseMessage)
        {
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        public static void ShouldBe201(this HttpResponseMessage responseMessage)
        {
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }

    public static class HttpExtensions
    {
        public static async Task<HttpResponseMessage> CreateClientAsync(this HttpClient httpClient, string clientName)
        {
            var response = await httpClient.PostAsJsonAsync(ClientsApiPath.ClientsPath, new
            {
                ClientName = clientName
            });

            return response;
        }

        public static async Task<HttpResponseMessage> AddClientSubscriberAsync(
            this HttpClient httpClient,
            Guid clientId,
            Guid subscriberId)
        {
            var response = await httpClient.PostAsJsonAsync(
                ClientsApiPath.ClientSubscribersPath(clientId),
                new
                {
                    SubscriberId = subscriberId
                });

            return response;
        }
        
        public static async Task<HttpResponseMessage> GetClientSubscriberAsync(
            this HttpClient httpClient,
            Guid clientId)
        {
            return await httpClient.GetAsync(ClientsApiPath.ClientSubscribersPath(clientId));
        }
        
        public static async Task<HttpResponseMessage> GetClientAsync(
            this HttpClient httpClient,
            Guid clientId)
        {
            return await httpClient.GetAsync(ClientsApiPath.ClientPath(clientId));
        }
    }
}