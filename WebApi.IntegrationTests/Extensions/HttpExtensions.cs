#region

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApi.IntegrationTests.Helpers;

#endregion

namespace WebApi.IntegrationTests.Extensions
{
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
    }
}