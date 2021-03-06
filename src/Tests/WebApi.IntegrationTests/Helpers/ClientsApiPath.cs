#region

using System;

#endregion

namespace WebApi.IntegrationTests.Helpers
{
    public static class ClientsApiPath
    {
        public const string BaseUrl = "http://localhost:5000";
        public const string ClientsPath = "api/v1/clients";
        
        public static string ClientPath(Guid clientId)
          => $"api/v1/clients/{clientId}";

        public static string ClientSubscribersPath(Guid clientId)
            => $"api/v1/clients/{clientId.ToString()}/subscribers";
    }
}